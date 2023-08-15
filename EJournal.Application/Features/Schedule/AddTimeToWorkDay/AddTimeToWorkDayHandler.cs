using AutoMapper;
using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.Domain.Extension;
using MediatR;

namespace EJournal.Application.Features.Schedule.AddTimeToWorkDay;

public sealed class AddTimeToWorkDayHandler : IRequestHandler<AddTimeToWorkDayRequest, AddTimeToWorkDayResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTimeToWorkDayHandler(BaseUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddTimeToWorkDayResponse> Handle(AddTimeToWorkDayRequest request, CancellationToken cancellationToken)
    {
        var workTimeEntity = _mapper.Map<WorkTime>(request);
        var workDayByDayOfWeek = await _unitOfWork._WorkDayRepository.GetByDayOfWeek(request.DayOfWeek);
        if (!workDayByDayOfWeek.IsWorkDay || workDayByDayOfWeek == default)
        {
            throw new BadRequestException("This day is not working or does`nt not found!!");
        }
        var times = workDayByDayOfWeek.Times.ToList();
        
        times.Add(workTimeEntity);
        workDayByDayOfWeek.Times = times;
        await _unitOfWork._WorkDayRepository.Update(workDayByDayOfWeek, cancellationToken);
        var response = _mapper.Map<AddTimeToWorkDayResponse>(workTimeEntity);
        response.DayOfWeek = ((int)request.DayOfWeek).ToString();
        response.DayOfWeekName = request.DayOfWeek.GetStringValue();
        
        return response;
    }
}