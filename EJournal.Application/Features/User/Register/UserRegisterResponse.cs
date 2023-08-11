namespace EJournal.Application.Features.User.Register;

public sealed class UserRegisterResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
}