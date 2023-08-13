namespace EJournal.Application.Features.Schedule.AddWorkDay;

public sealed class AddWorkDayResponse
{
    public string Id { get; set; }
    public string DayOfWeek { get; set; }
    public string DayOfWeekName { get; set; }
    public string CreatedAt { get; set; }
}