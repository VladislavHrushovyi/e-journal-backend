using EJournal.Domain.Common;

namespace EJournal.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task<T> GetById(Guid id, CancellationToken ct);
    public Task<IEnumerable<T>> GetAll(CancellationToken ct);
    public Task<T> Create(T entity, CancellationToken ct);
    public Task<T> Update(T entity, CancellationToken ct);
    public Task<T> Delete(T entity, CancellationToken ct);
}