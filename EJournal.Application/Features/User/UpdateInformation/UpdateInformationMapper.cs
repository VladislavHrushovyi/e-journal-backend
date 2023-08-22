using AutoMapper;

namespace EJournal.Application.Features.User.UpdateInformation;

public sealed class UpdateInformationMapper : Profile
{
    public UpdateInformationMapper()
    {
        CreateMap<UpdateInformationRequest, Domain.Entities.User>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.FirstName, opt
                => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt
                => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.PhoneNumber, opt
                => opt.MapFrom(src => src.PhoneNumber));

        CreateMap<Domain.Entities.User, UpdateInformationResponse>()
            .ForMember(dest => dest.FullName, opt
                => opt.MapFrom((src, _) => src.FirstName + " " + src.LastName))
            .ForMember(dest => dest.PhoneNumber, opt
                => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.PhoneNumber, opt
                => opt.MapFrom(src => src.UpdateAt.ToString()));
    }
}