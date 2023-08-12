using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.User.Register;

public sealed class UserRegisterRequest : IRequest<UserRegisterResponse>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; } = Role.User;
}