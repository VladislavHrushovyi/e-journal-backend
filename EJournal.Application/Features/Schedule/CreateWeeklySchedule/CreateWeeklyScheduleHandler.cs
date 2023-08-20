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

    public async Task<CreateWeeklyScheduleResponse> Handle(CreateWeeklyScheduleRequest request,
        CancellationToken cancellationToken)
    {
        var dateNow = DateTime.Now;
        var weeklySchedules = await _unitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken);
        var dateFirstDayOfWeek = DateOnly.FromDateTime(DateTime.Now)
            .AddDays(-((int)DateTime.Today.DayOfWeek - 1));

        var schedulesList = weeklySchedules.ToList();
        if ((
                dateNow.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday or DayOfWeek.Sunday
                && dateNow.Hour > 15
                )
            ||
            !schedulesList.Any())
        {
            if (schedulesList.Count > 1)
            {
                await _unitOfWork.WeeklyScheduleRepository.Delete(schedulesList.Last(), cancellationToken);
            }

            Calendar cal = new CultureInfo("uk-UA").Calendar;
            int week = cal.GetWeekOfYear(dateNow, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            var allWorkingDays = await _unitOfWork._WorkDayRepository.GetAll(cancellationToken);
            allWorkingDays = allWorkingDays.Select(workDay => new WorkDay()
            {
                Id = workDay.Id,
                Date = dateFirstDayOfWeek.AddDays((int)workDay.DayOfWeek),
                CreatedAt = workDay.CreatedAt,
                DayOfWeek = workDay.DayOfWeek,
                Times = workDay.Times,
                UpdateAt = workDay.UpdateAt,
                IsWorkDay = workDay.IsWorkDay
            });
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