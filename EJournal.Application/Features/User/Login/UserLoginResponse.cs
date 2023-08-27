using EJournal.Domain.Common;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginResponse
{
    public string JwtToken { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
}