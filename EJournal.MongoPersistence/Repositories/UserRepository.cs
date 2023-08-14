using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;
using MongoDB.Driver;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<User> FindOneByProperties(User entity, CancellationToken ct)
    {
        var singleUser = await _collection.Find(x =>
                x.PhoneNumber == entity.PhoneNumber &&
                x.Password == entity.Password)
            .FirstOrDefaultAsync(ct);

        return singleUser;
    }
}