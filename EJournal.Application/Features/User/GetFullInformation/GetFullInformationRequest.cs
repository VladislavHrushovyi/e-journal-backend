using System.Text.Json.Serialization;
using MediatR;

namespace EJournal.Application.Features.User.GetFullInformation;

public sealed class GetFullInformationRequest : IRequest<GetFullInformationResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}