using AutoMapper;
using EJournal.Application.Repositories;
using MediatR;

namespace EJournal.Application.Features.User.UpdateInformation;

public sealed class UpdateInformationHandler : IRequestHandler<UpdateInformationRequest, UpdateInformationResponse>
{
    private readonly IMapper _mapper;
    private readonly BaseUnitOfWork _unitOfWork;

    public UpdateInformationHandler(IMapper mapper, BaseUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateInformationResponse> Handle(UpdateInformationRequest request, CancellationToken cancellationToken)
    {
        var newUserData = _mapper.Map<Domain.Entities.User>(request);
        var oldUserInformation = await _unitOfWork._userRepository.GetById(request.UserId, cancellationToken);

        newUserData.Id = oldUserInformation.Id;
        newUserData.Password = oldUserInformation.Password;
        newUserData.Role = oldUserInformation.Role;
        newUserData.RecordHistoryItems = oldUserInformation.RecordHistoryItems;
        newUserData.CreatedAt = oldUserInformation.CreatedAt;
        newUserData.UpdateAt = DateOnly.FromDateTime(DateTime.Now);

        var result = await _unitOfWork._userRepository.Update(newUserData, cancellationToken);
        var response = _mapper.Map<UpdateInformationResponse>(result);
        return response;
    }
}