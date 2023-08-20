using System.Net;
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.ChangeWorkTimeStatus;

public sealed class ChangeStatusHandler : IRequestHandler<ChangeStatusRequest, ChangeStatusResponse>
{
    private BaseUnitOfWork _unitOfWork { get; set; }
    
    public ChangeStatusHandler(BaseUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ChangeStatusResponse> Handle(ChangeStatusRequest request, CancellationToken cancellationToken)
    {
        var currentSchedule = (await _unitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken)).Last();
        var workTimeById = currentSchedule
            .WorkDays.First(x => x.DayOfWeek == request.DayOfWeek)
            .Times.First(x => x.Id == request.WorkTimeId);
        Domain.Entities.User user = await _unitOfWork._userRepository.GetById(workTimeById.UserId, cancellationToken);

        switch (request.Status)
        {
            case ReservationStatus.Free:
                workTimeById.Status = ReservationStatus.Free;
                workTimeById.UserId = new Guid();
                ChangeStatus(currentSchedule, workTimeById, user, ReservationStatus.Canceled, request.DayOfWeek);
                break;
            case ReservationStatus.Done:
                workTimeById.Status = ReservationStatus.Done;
                ChangeStatus(currentSchedule, workTimeById, user, ReservationStatus.Done, request.DayOfWeek);
                break;
            case ReservationStatus.Canceled:
                workTimeById.Status = ReservationStatus.Canceled;
                ChangeStatus(currentSchedule, workTimeById, user, ReservationStatus.Canceled, request.DayOfWeek);
                break;
            case ReservationStatus.Reserved:
                workTimeById.Status = ReservationStatus.Reserved;
                ChangeStatus(currentSchedule, workTimeById, user, ReservationStatus.Reserved, request.DayOfWeek);
                break;
            case ReservationStatus.TemporaryHold:
                workTimeById.Status = ReservationStatus.TemporaryHold;
                ChangeStatus(currentSchedule, workTimeById, user, ReservationStatus.TemporaryHold, request.DayOfWeek);
                break;
        }
        
        var userUpdateTask = _unitOfWork._userRepository.Update(user, cancellationToken);
        var scheduleUpdateTsk = _unitOfWork.WeeklyScheduleRepository.Update(currentSchedule, cancellationToken);
        await Task.WhenAll(userUpdateTask, scheduleUpdateTsk);
        return new ChangeStatusResponse()
        {
            StatusCode = HttpStatusCode.OK
        };
    }

    private void ChangeStatus(
        WeeklySchedule currentWeekly, 
        WorkTime workTimeById, 
        Domain.Entities.User user, 
        ReservationStatus status,
        CustomDayOfWeek dayOfWeek
        ) 
    {
        user.RecordHistoryItems = user.RecordHistoryItems.Select(x =>
        {
            if (x.WorkTimeId == workTimeById.Id)
            {
                x.Status = status;
            }

            return x;
        });

        currentWeekly.WorkDays = currentWeekly.WorkDays.Select(x =>
        {
            if (x.DayOfWeek == dayOfWeek)
            {
                x.Times = x.Times.Select(w =>
                {
                    if (w.Id == workTimeById.Id)
                    {
                        return workTimeById;
                    }

                    return w;
                });
            }

            return x;
        });
    }
}