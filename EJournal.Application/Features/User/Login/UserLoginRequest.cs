using MediatR;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginRequest : IRequest<UserLoginResponse>
{
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
}