using AutoMapper;
using EJournal.Domain.Entities;
using EJournal.Domain.Extension;

namespace EJournal.Application.Features.Schedule.AddWorkDay;

public sealed class AddWorkDayMapper : Profile
{
    public AddWorkDayMapper()
    {
        CreateMap<AddWorkDayRequest, WorkDay>();
        CreateMap<WorkDay, AddWorkDayResponse>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.DayOfWeek, opt
                => opt.MapFrom(src => ((int)src.DayOfWeek).ToString()))
            .ForMember(dest => dest.DayOfWeekName, opt
                => opt.MapFrom(src => src.DayOfWeek.GetStringValue()))
            .ForMember(dest => dest.CreatedAt, opt
                => opt.MapFrom(src => src.CreatedAt.ToString()));
    }
}