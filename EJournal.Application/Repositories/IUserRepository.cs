using EJournal.Domain.Entities;

namespace EJournal.Application.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    public Task<User> FindOneByProperties(User entity, CancellationToken ct);
}