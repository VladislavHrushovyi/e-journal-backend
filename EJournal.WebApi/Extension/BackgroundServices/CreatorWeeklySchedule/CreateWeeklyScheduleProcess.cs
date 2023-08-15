using EJournal.Application.Features.Schedule.CreateWeeklySchedule;
using MediatR;

namespace EJournal.WebApi.Extension.BackgroundServices.CreatorWeeklySchedule;

public sealed class CreateWeeklyScheduleProcess : ICreateWeeklyScheduleProcess
{
    private readonly IMediator _mediator;

    public CreateWeeklyScheduleProcess(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _mediator.Send(new CreateWeeklyScheduleRequest(), stoppingToken);

            await Task.Delay(1000 * 60 * 60 * 12, stoppingToken);
        }
    }
}