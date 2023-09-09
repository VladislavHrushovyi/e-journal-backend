using AutoMapper;
using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.UpdateIsWorkDay;

public sealed class UpdateIsWorkDayHandler : IRequestHandler<UpdateIsWorkDayRequest, UpdateIsWorkDayResponse>
{
    private readonly BaseUnitOfWork _baseUnitOfWork;
    private readonly IMapper _mapper;

    public UpdateIsWorkDayHandler(BaseUnitOfWork baseUnitOfWork, IMapper mapper)
    {
        _baseUnitOfWork = baseUnitOfWork;
        _mapper = mapper;
    }

    public async Task<UpdateIsWorkDayResponse> Handle(UpdateIsWorkDayRequest request, CancellationToken cancellationToken)
    {
        var result = Guid.TryParse(request.WorkDayId, out Guid workDayId);
        if (!result)
        {
            throw new BadRequestException("Id is not valid");
        }

        WorkDay workDayById = await _baseUnitOfWork._WorkDayRepository.GetById(workDayId, cancellationToken);
        if (workDayById == null)
        {
            throw new BadRequestException("Work day not founded");
        }

        workDayById.IsWorkDay = request.IsWorkDay;
        var updatedResult = await _baseUnitOfWork._WorkDayRepository.Update(workDayById, cancellationToken);
        var response = new UpdateIsWorkDayResponse();
        response.WorkDay = updatedResult;
        return response;
    }
}