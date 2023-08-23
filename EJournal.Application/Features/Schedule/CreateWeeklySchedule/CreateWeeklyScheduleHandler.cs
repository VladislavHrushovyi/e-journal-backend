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
        Calendar cal = new CultureInfo("uk-UA").Calendar;
        int week = cal.GetWeekOfYear(dateNow, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

        var schedulesList = weeklySchedules.ToList();
        if (IsOldWeekSchedule(dateNow, schedulesList) || IsNotCurrWeek(week, schedulesList.Last()))
        {
            if (schedulesList.Count >= 1)
            {
                schedulesList.ForEach(sc => _unitOfWork.WeeklyScheduleRepository.Delete(sc, cancellationToken));
            }

            await CreateNewWeeklySchedule(week, cancellationToken);
        }

        return new CreateWeeklyScheduleResponse();
    }

    private bool IsNotCurrWeek(int week, WeeklySchedule last)
    {
        var lastNumberScheduleWeekString = last.WeekId.Split("-")[0];

        return week.ToString() != lastNumberScheduleWeekString;
    }

    private bool IsOldWeekSchedule(DateTime now, IEnumerable<WeeklySchedule> currWeeklySchedule)
    {
        return now.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday
               ||
               now.DayOfWeek is DayOfWeek.Friday && now.Hour >= 15
               ||
               !currWeeklySchedule.Any();
    }

    private async Task CreateNewWeeklySchedule(int currNumberWeek, CancellationToken cancellationToken)
    {
        var dateFirstDayOfWeek = DateOnly.FromDateTime(DateTime.Now)
            .AddDays(-((int)DateTime.Today.DayOfWeek - 1));
        
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
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            WeekId = currNumberWeek + "-" + dateFirstDayOfWeek.Year,
            WorkDays = allWorkingDays
        }, cancellationToken);
    }
}