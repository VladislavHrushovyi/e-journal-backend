
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using EJournal.MongoPersistence.Context;

namespace EJournal.MongoPersistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext _dataContext;

    public BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public Task<T> GetById(Guid id, CancellationToken ct)
    {
        _dataContext.MongoDatabase.GetCollection<T>(nameof(T));
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