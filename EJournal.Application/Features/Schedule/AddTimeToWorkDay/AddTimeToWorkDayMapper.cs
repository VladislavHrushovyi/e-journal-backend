using System.Globalization;
using AutoMapper;
using EJournal.Domain.Common;
using EJournal.Domain.Entities;

namespace EJournal.Application.Features.Schedule.AddTimeToWorkDay;

public sealed class AddTimeToWorkDayMapper : Profile
{
    public AddTimeToWorkDayMapper()
    {
        CreateMap<AddTimeToWorkDayRequest, WorkTime>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Time, opt
                => opt.MapFrom((dest, _) => new DateTime(
                    1,
                    1,
                    1,
                    Int32.Parse(dest.Time.Split(':')[0]),
                    Int32.Parse(dest.Time.Split(':')[1]),
                    0,
                    DateTimeKind.Unspecified
                )))
            .ForMember(dest => dest.Status, opt
                => opt.MapFrom(_ => ReservationStatus.Free))
            .ForMember(dest => dest.CreatedAt, opt
                => opt.MapFrom(_ => DateOnly.FromDateTime(DateTime.Now)));
        CreateMap<WorkTime, AddTimeToWorkDayResponse>()
            .ForMember(dest => dest.Id, opt
                => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Time, opt
                => opt.MapFrom(src => src.Time.ToString(CultureInfo.CurrentUICulture)));
    }
}