using System.Globalization;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.CreateWeeklySchedule;

public class CreateWeeklyScheduleHandler : IRequestHandler<CreateWeeklyScheduleRequest, CreateWeeklyScheduleResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;

    public CreateWeeklyScheduleHandler(BaseUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateWeeklyScheduleResponse> Handle(CreateWeeklyScheduleRequest request, CancellationToken cancellationToken)
    {
        var dateNow = DateTime.Now;
        var weeklySchedules = await _unitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken);
        
        if ((dateNow.DayOfWeek == DayOfWeek.Friday && dateNow.Hour > 15) || !weeklySchedules.Any())
        {
            Calendar cal = new CultureInfo("uk-UA").Calendar;
            int week = cal.GetWeekOfYear(dateNow, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var allWorkingDays = await _unitOfWork._WorkDayRepository.GetAll(cancellationToken);
            await _unitOfWork.WeeklyScheduleRepository.Create(new WeeklySchedule()
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateOnly.FromDateTime(dateNow),
                WeekId = week + "-" + dateNow.Year,
                WorkDays = allWorkingDays
            }, cancellationToken);
        }

        return new CreateWeeklyScheduleResponse();
    }
}