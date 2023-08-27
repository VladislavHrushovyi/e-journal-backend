using AutoMapper;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginMapper : Profile
{
    public UserLoginMapper()
    {
        CreateMap<UserLoginRequest, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, UserLoginResponse>()
            .ForMember(dest => dest.Name, opt
                => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Surname, opt
                => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt
                => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Role, opt
                => opt.MapFrom(src => src.Role));
    }
}