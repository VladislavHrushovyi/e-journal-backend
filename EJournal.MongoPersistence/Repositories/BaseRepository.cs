
using EJournal.Application.Repositories;
using EJournal.Domain.Common;
using EJournal.MongoPersistence.Context;
using MongoDB.Driver;

namespace EJournal.MongoPersistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DataContext _dataContext;
    protected readonly IMongoCollection<T> _collection;
    public BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
        _collection = _dataContext.MongoDatabase.GetCollection<T>(typeof(T).Name);
    }

    public Task<T> GetById(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAll(CancellationToken ct)
    {
        return (await _collection.FindAsync(_ => true, cancellationToken: ct)).ToList(cancellationToken: ct);
    }

    [Obsolete("Create entity")]
    public async Task<T> Create(T entity, CancellationToken ct)
    {
        await _collection.InsertOneAsync(entity, ct);
        return entity;
    }

    public async Task<T> Update(T entity, CancellationToken ct)
    {
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity, cancellationToken: ct);

        return entity;
    }

    public async Task<T> Delete(T entity, CancellationToken ct)
    {
        await _collection.DeleteOneAsync(x => x.Id == entity.Id, cancellationToken: ct);

        return entity;
    }
}