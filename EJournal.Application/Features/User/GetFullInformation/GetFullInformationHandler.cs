using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using MediatR;

namespace EJournal.Application.Features.User.GetFullInformation;

public sealed class GetFullInformationHandler : IRequestHandler<GetFullInformationRequest, GetFullInformationResponse>
{
    private BaseUnitOfWork _baseUnitOfWork { get; set; }
    
    public GetFullInformationHandler(BaseUnitOfWork baseUnitOfWork)
    {
        _baseUnitOfWork = baseUnitOfWork;
    }
    
    public async Task<GetFullInformationResponse> Handle(GetFullInformationRequest request, CancellationToken cancellationToken)
    {
        var user = await _baseUnitOfWork._userRepository.GetById(request.UserId, cancellationToken);
        if (user == null)
        {
            throw new BadRequestException("User not found");
        }

        return new GetFullInformationResponse()
        {
            AllInformation = user
        };
    }
}