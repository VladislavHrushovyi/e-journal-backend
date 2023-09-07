using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.Schedule.GetWorkDays;

public sealed class GetWorkDaysHandler : IRequestHandler<GetWorkDaysRequest, GetWorkDaysResponse>
{
    private readonly BaseUnitOfWork _baseUnitOfWork;

    public GetWorkDaysHandler(BaseUnitOfWork baseUnitOfWork)
    {
        _baseUnitOfWork = baseUnitOfWork;
    }

    public async Task<GetWorkDaysResponse> Handle(GetWorkDaysRequest request, CancellationToken cancellationToken)
    {
        var result = await _baseUnitOfWork._WorkDayRepository.GetAll(cancellationToken);
        var workDays = result as WorkDay[] ?? result.ToArray();
        if (!workDays.Any())
        {
            throw new BadRequestException("The WorkDays sequence is empty");
        }

        var response = new GetWorkDaysResponse(workDays);

        return response;
    }
}