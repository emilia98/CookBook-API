using System.Linq;
using CookBook.Data.Shared.Models;

namespace CookBook.Data.Shared.Repositories
{
    public interface IDeletableEntityRepository<TEntity>: IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}