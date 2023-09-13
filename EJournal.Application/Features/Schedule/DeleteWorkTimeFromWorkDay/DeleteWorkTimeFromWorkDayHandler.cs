using System.Net;
using EJournal.Application.Repositories;
using MediatR;

namespace EJournal.Application.Features.Schedule.DeleteWorkTimeFromWorkDay;

public sealed class DeleteWorkTimeFromWorkDayHandler 
    : IRequestHandler<DeleteWorkTimeFromWorkDayRequest, DeleteWorkTimeFromWorkDayResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;

    public DeleteWorkTimeFromWorkDayHandler(BaseUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DeleteWorkTimeFromWorkDayResponse> Handle(DeleteWorkTimeFromWorkDayRequest request, CancellationToken cancellationToken)
    {
        var workDay = await _unitOfWork._WorkDayRepository.GetByDayOfWeek(request.DayOfWeek, cancellationToken);
        workDay.Times = workDay.Times.Where(x => x.Id != request.WorkTimeId);

        await _unitOfWork._WorkDayRepository.Update(workDay, cancellationToken);

        return new DeleteWorkTimeFromWorkDayResponse()
        {
            HttpStatusCode = HttpStatusCode.OK
        };
    }
}