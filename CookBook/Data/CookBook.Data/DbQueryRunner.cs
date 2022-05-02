using System;
using System.Threading.Tasks;
using CookBook.Data.Shared;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Data
{
    public class DbQueryRunner : IDbQueryRunner
    {
        public DbQueryRunner(ApplicationDbContext dbContext)
        {
            this.DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public ApplicationDbContext DbContext { get; set; }

        public Task RunQueryAsync(string query, params object[] parameters)
        {
            return this.DbContext.Database.ExecuteSqlRawAsync(query, parameters);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
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