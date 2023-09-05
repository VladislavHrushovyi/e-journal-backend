using EJournal.Domain.Common;

namespace EJournal.Application.Features.Schedule.GetActiveRecords;

public sealed class GetActiveRecordsResponse
{
    public List<ActiveRecordInformation> ActiveRecords { get; set; }

    public GetActiveRecordsResponse()
    {
        this.ActiveRecords = new List<ActiveRecordInformation>();
    }
}

public sealed class ActiveRecordInformation
{
    public string WorkTimeId { get; set; }
    public string Date { get; set; }
    public CustomDayOfWeek DayOfWeek { get; set; }
    public string Time { get; set; }
    public string UserMessage { get; set; }
    public UserInformation User { get; set; }
}

public sealed class UserInformation
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public UserInformation(Domain.Entities.User User)
    {
        UserId = User.Id.ToString();
        FirstName = User.FirstName;
        LastName = User.LastName;
        PhoneNumber = User.PhoneNumber;
    }
} 