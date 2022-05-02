using System;
using System.Linq;
using CookBook.Data.Shared.Models;
using CookBook.Data.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Data.Repositories
{
    public class EFDeletableEntityRepository<TEntity> : EFRepository<TEntity>, IDeletableEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        public EFDeletableEntityRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => base.AllAsNoTracking().IgnoreQueryFilters();

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}