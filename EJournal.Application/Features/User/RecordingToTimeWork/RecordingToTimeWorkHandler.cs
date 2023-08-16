using System.Net;
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
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
        var currentWeek = await _unitOfWork.WeeklyScheduleRepository.GetAll(cancellationToken);
        var activeWeek = currentWeek.FirstOrDefault();
        if (activeWeek == default)
        {
            return new RecordingToTimeWorkResponse()
            {
                Status = HttpStatusCode.Conflict
            };
        }
        activeWeek.WorkDays = activeWeek.WorkDays.Select(x =>
        {
            if (x.DayOfWeek == request.DayOfWeek)
            {
                x.Times = x.Times.Select(t =>
                {
                    if (t.Id != request.TimeId) return t;
                    t.UserId = request.UserId;
                    t.Status = ReservationStatus.TemporaryHold;

                    return t;
                });
            }

            return x;
        });

        await _unitOfWork.WeeklyScheduleRepository.Update(activeWeek, cancellationToken);
        return new RecordingToTimeWorkResponse()
        {
            Status = HttpStatusCode.OK
        };
    }
}