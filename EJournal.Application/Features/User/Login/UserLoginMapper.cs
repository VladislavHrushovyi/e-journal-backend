using AutoMapper;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginMapper : Profile
{
    public UserLoginMapper()
    {
        CreateMap<UserLoginRequest, Domain.Entities.User>();
    }
}