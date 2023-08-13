using AutoMapper;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.AddWorkDay;

public sealed class AddWorkDayHandler : IRequestHandler<AddWorkDayRequest, AddWorkDayResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddWorkDayHandler(BaseUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AddWorkDayResponse> Handle(AddWorkDayRequest request, CancellationToken cancellationToken)
    {
        var workDayEntity = _mapper.Map<WorkDay>(request);
        workDayEntity.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        workDayEntity.Id = Guid.NewGuid();
        var result = await _unitOfWork._WorkDayRepository.Create(workDayEntity, cancellationToken);

        return _mapper.Map<AddWorkDayResponse>(result);
    }
}