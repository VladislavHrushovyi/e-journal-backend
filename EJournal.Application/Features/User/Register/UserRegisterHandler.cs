using AutoMapper;
using EJournal.Application.Common.Exception;
using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using MediatR;

namespace EJournal.Application.Features.User.Register;

public sealed class UserRegisterHandler : IRequestHandler<UserRegisterRequest, UserRegisterResponse>
{
    private readonly BaseUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserRegisterHandler(BaseUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserRegisterResponse> Handle(UserRegisterRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Domain.Entities.User>(request);
        var userExisted = await _unitOfWork._userRepository.GetAll(cancellationToken);
        if (userExisted.Any(x => x.PhoneNumber == entity.PhoneNumber))
        {
            throw new BadRequestException("User has been registered with same phone number");
        }
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        entity.RecordHistoryItems = Array.Empty<RecordHistoryItem>();
        var result = await _unitOfWork._userRepository.Create(entity, cancellationToken);

        return _mapper.Map<Domain.Entities.User, UserRegisterResponse>(result);
    }
}