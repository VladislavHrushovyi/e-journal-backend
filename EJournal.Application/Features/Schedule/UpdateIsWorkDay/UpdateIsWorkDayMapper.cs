using AutoMapper;

namespace EJournal.Application.Features.Schedule.UpdateIsWorkDay;

public sealed class UpdateIsWorkDayMapper : Profile
{
    public UpdateIsWorkDayMapper()
    {
        // CreateMap<UpdateWorkDayConfigRequest, WorkDay>()
        //     .ForMember(dest => dest.Id, opt
        //         => opt.MapFrom(src => src.WorkDay.Id))
        //     .ForMember(dest => dest.Date, opt
        //         => opt.MapFrom(src => src.WorkDay.Date))
        //     .ForMember(dest => dest.Times, opt
        //         => opt.MapFrom(src => src.WorkDay.Times))
        //     .ForMember(dest => dest.DayOfWeek, opt
        //         => opt.MapFrom(src => src.WorkDay.DayOfWeek))
        //     .ForMember(dest => dest.IsWorkDay, opt
        //         => opt.MapFrom(src => src.WorkDay.IsWorkDay))
        //     .ForMember(dest => dest.CreatedAt, opt
        //         => opt.MapFrom(src => src.WorkDay.CreatedAt))
        //     .ForMember(dest => dest.UpdateAt, opt
        //         => opt.MapFrom(src => src.WorkDay.UpdateAt));
        // CreateMap<WorkDay, UpdateWorkDayConfigResponse>()
        //     .ForMember(dest => dest.WorkDay.Id, opt
        //         => opt.MapFrom(src => src.Id))
        //     .ForMember(dest => dest.WorkDay.IsWorkDay, opt
        //         => opt.MapFrom(src => src.IsWorkDay))
        //     .ForMember(dest => dest.WorkDay.DayOfWeek, opt
        //         => opt.MapFrom(src => src.DayOfWeek))
        //     .ForMember(dest => dest.WorkDay.Date, opt
        //         => opt.MapFrom(src => src.Date))
        //     .ForMember(dest => dest.WorkDay.Times, opt
        //         => opt.MapFrom(src => src.Times))
        //     .ForMember(dest => dest.WorkDay.CreatedAt, opt
        //         => opt.MapFrom(src => src.CreatedAt))
        //     .ForMember(dest => dest.WorkDay.UpdateAt, opt
        //         => opt.MapFrom(src => src.UpdateAt));
    }
}