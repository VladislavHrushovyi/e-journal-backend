using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EJournal.Domain.Common;
using MediatR;

namespace EJournal.Application.Features.User.RecordingToTimeWork;

public sealed class RecordingToTimeWorkRequest : IRequest<RecordingToTimeWorkResponse>
{
    public CustomDayOfWeek DayOfWeek { get; set; }
    public Guid TimeId { get; set; }
    
    [JsonIgnore]
    public Guid UserId { get; set; }
}