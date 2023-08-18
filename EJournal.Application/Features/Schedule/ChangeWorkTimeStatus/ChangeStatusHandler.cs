using EJournal.Application.Repositories;
using EJournal.Domain.Common;
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
        Domain.Entities.User user = null;
        if (workTimeById.UserId != default)
        {
            user = await _unitOfWork._userRepository.GetById(workTimeById.UserId, cancellationToken);
        }

        switch (request.Status)
        {
            case ReservationStatus.Free:
                workTimeById.Status = ReservationStatus.Free;
                if (user != null)
                {
                    user.RecordHistoryItems = user.RecordHistoryItems.Select(x =>
                    {
                        if (x.WorkTimeId == workTimeById.Id)
                        {
                            x.Status = ReservationStatus.Canceled;
                        }

                        return x;
                    });
                }

                currentSchedule.WorkDays = currentSchedule.WorkDays.Select(x =>
                {
                    if (x.DayOfWeek == request.DayOfWeek)
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
                var UserUpdateTask = _unitOfWork._userRepository.Update(user, cancellationToken);
                var ScheduleUpdate = _unitOfWork.WeeklyScheduleRepository.Update(currentSchedule, cancellationToken); //TODO HERE BUG
                break;
        }
        return new ChangeStatusResponse();
    }
}