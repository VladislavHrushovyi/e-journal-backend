using EJournal.Application.Repositories;
using EJournal.Domain.Entities;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext dataContext) : base(dataContext)
    {
    }
}