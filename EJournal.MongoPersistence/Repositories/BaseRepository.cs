
using EJournal.Application.Repositories;
using EJournal.Domain.Common;

namespace EJournal.MongoPersistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    public Task<T> GetById(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<T> Create(T entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<T> Update(T entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete(T entity, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}