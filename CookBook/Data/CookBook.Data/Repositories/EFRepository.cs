using System;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Data.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Data.Repositories
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity: class
    {
        public EFRepository(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.DbSet = this.DbContext.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public virtual Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public virtual IQueryable<TEntity> All() => this.DbSet;

        public virtual IQueryable<TEntity> AllAsNoTracking() => this.DbSet.AsNoTracking();

        public virtual void Delete(TEntity entity) => this.DbSet.Remove(entity);

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveChangesAsync() => this.DbContext.SaveChangesAsync();

        public virtual void Update(TEntity entity)
        {
            var entry = this.DbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DbContext?.Dispose();
            }
        }
    }
}