using AutoMapper;

namespace EJournal.Application.Features.User.Register;

public sealed class UserRegisterMapper : Profile
{
    public UserRegisterMapper()
    {
        CreateMap<UserRegisterRequest, Domain.Entities.User>();
        CreateMap<Domain.Entities.User, UserRegisterResponse>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CreatedAt, opt =>
                opt.MapFrom(src => src.CreatedAt.ToString()))
            .ForMember(dest => dest.UpdatedAt, opt =>
                opt.MapFrom(src => src.UpdateAt.ToString()));
    }
}