using EJournal.Application.Repositories;
using EJournal.Domain.Entities;

namespace EJournal.MongoPersistence.Repositories;

public sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    
}