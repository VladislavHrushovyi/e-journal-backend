using EJournal.Application.Repositories;
using MediatR;

namespace EJournal.Application.Features.User.RecordingToTimeWork;

public sealed class RecordingToTimeWorkHandler : IRequestHandler<RecordingToTimeWorkRequest, RecordingToTimeWorkResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;
    public RecordingToTimeWorkHandler(BaseUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<RecordingToTimeWorkResponse> Handle(RecordingToTimeWorkRequest request, CancellationToken cancellationToken)
    {
        var workDayByNumber = await _unitOfWork._WorkDayRepository.GetByDayOfWeek(request.DayOfWeek);
        // TODO implement after background worker

        return new RecordingToTimeWorkResponse();
    }
}