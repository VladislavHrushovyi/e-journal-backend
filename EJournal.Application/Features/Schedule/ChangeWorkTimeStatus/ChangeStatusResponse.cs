using System.Net;

namespace EJournal.Application.Features.Schedule.ChangeWorkTimeStatus;

public sealed class ChangeStatusResponse
{
    public HttpStatusCode StatusCode { get; set; }
}