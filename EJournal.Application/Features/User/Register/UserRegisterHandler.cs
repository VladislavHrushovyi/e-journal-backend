using AutoMapper;
using EJournal.Application.Repositories;
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
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        var result = await _unitOfWork._userRepository.Create(entity, cancellationToken);

        return _mapper.Map<Domain.Entities.User, UserRegisterResponse>(result);
    }
}