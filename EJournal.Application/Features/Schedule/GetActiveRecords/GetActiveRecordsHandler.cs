using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.Schedule.GetActiveRecords;

public sealed class GetActiveRecordsHandler : IRequestHandler<GetActiveRecordsRequest, GetActiveRecordsResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;

    public GetActiveRecordsHandler(BaseUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetActiveRecordsResponse> Handle(GetActiveRecordsRequest request, CancellationToken cancellationToken)
    {
        var weekSchedules = await _unitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken);
        var currSchedule = weekSchedules.Last();
        if (currSchedule == null)
        {
            throw new BadRequestException("Current weekly schedule is null");
        }

        var response = new GetActiveRecordsResponse();
        var workDays = currSchedule.WorkDays.Where(day => day.Times.Any());
        foreach (var day in workDays)
        {
            foreach (var time in day.Times)
            {
                if (time.Status == ReservationStatus.TemporaryHold)
                {
                    var user = await _unitOfWork._userRepository.GetById(time.UserId, cancellationToken);
                    response.ActiveRecords.Add(new ActiveRecordInformation()
                    {
                        Date = day.Date.ToString(),
                        DayOfWeek = day.DayOfWeek,
                        Time = time.Time,
                        UserMessage = time.UserMessage,
                        WorkTimeId = time.Id.ToString(),
                        User = new UserInformation(user)
                    });
                }
            }
        }
            
        return response;
    }
}