using System.Globalization;
using System.Net;
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using EJournal.Domain.Entities;
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
        var newRecordUser = new RecordHistoryItem()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            DayOfWeek = request.DayOfWeek,
        };
        var user = await _unitOfWork._userRepository.GetById(request.UserId, cancellationToken);
        if (user.RecordHistoryItems == null)
        {
            user.RecordHistoryItems = new List<RecordHistoryItem>();
        }
        var activeWeek = currentWeek.FirstOrDefault();
        if (activeWeek == null)
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
                    var time = DateTime.Parse(t.Time);
                    if (t.Id != request.TimeId) return t;
                    t.UserId = request.UserId;
                    t.Status = ReservationStatus.TemporaryHold;
                    newRecordUser.Date = new DateTime(x.Date.Year, x.Date.Month, x.Date.Day, time.Hour, time.Minute,
                        0).ToString();
                    newRecordUser.Status = ReservationStatus.TemporaryHold;
                    newRecordUser.WorkTimeId = t.Id;

                    return t;
                });
            }

            return x;
        });
        var updateActiveWeekTask = _unitOfWork.WeeklyScheduleRepository.Update(activeWeek, cancellationToken);

        user.RecordHistoryItems = user.RecordHistoryItems.Append(newRecordUser);
        var updateUserTask = _unitOfWork._userRepository.Update(user, cancellationToken);
        
        await Task.WhenAll(updateUserTask, updateActiveWeekTask);
        return new RecordingToTimeWorkResponse()
        {
            Status = HttpStatusCode.OK
        };
    }
}