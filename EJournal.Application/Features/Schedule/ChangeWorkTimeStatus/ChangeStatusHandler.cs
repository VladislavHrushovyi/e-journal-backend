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
        Domain.Entities.User? user = null;
        if (workTimeById.UserId != default)
        {
            user = await _unitOfWork._userRepository.GetById(workTimeById.UserId, cancellationToken);
        }

        workTimeById.Status = request.Status;
        if (request.Status is ReservationStatus.Free)
        {
            workTimeById.Status = ReservationStatus.Free;
            workTimeById.UserId = new Guid();
            await ChangeStatus(currentSchedule, workTimeById, request.DayOfWeek, cancellationToken);
        }
        else
        {
            await ChangeStatus(currentSchedule, workTimeById, request.DayOfWeek, cancellationToken);
        }

        if (user != null)
        {
            await ChangeRecordStatusInUser(
            user, workTimeById.Id, 
            request.Status == ReservationStatus.Free ? ReservationStatus.Canceled : request.Status,
            cancellationToken);
        }
        
        return new ChangeStatusResponse()
        {
            StatusCode = HttpStatusCode.OK
        };
    }

    private Task ChangeRecordStatusInUser(
        Domain.Entities.User user,
        Guid workTimeId, 
        ReservationStatus status, 
        CancellationToken ct
        )
    {
        user.RecordHistoryItems = user.RecordHistoryItems.Select(x =>
        {
            if (x.WorkTimeId == workTimeId)
            {
                x.Status = status;
            }

            return x;
        });
        
        return _unitOfWork._userRepository.Update(user, ct);
    }

    private Task ChangeStatus(
        WeeklySchedule currentWeekly, 
        WorkTime workTimeById,
        CustomDayOfWeek dayOfWeek,
        CancellationToken ct
        ) 
    {
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
        return _unitOfWork.WeeklyScheduleRepository.Update(currentWeekly, ct);
    }
}