using EJournal.Application.Repositories;
using MediatR;

namespace EJournal.Application.Features.Schedule.GetActualWeeklySchedule;

public sealed class GetActualWeeklyScheduleHandler : IRequestHandler<GetActualWeeklyScheduleRequest, GetActualWeeklyScheduleResponse>
{
    private readonly BaseUnitOfWork _baseUnitOfWork;

    public GetActualWeeklyScheduleHandler(BaseUnitOfWork baseUnitOfWork)
    {
        _baseUnitOfWork = baseUnitOfWork;
    }

    public async Task<GetActualWeeklyScheduleResponse> Handle(GetActualWeeklyScheduleRequest request, CancellationToken cancellationToken)
    {
        var weeklySchedulers = await _baseUnitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken);
        return new GetActualWeeklyScheduleResponse()
        {
            ActualWeeklySchedule = weeklySchedulers.Last()
        };
    }
}