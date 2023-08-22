using System.Text.Json.Serialization;
using MediatR;

namespace EJournal.Application.Features.User.UpdateInformation;

public sealed class UpdateInformationRequest : IRequest<UpdateInformationResponse>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
}