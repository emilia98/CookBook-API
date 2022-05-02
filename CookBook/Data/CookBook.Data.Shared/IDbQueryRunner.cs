using System;
using System.Threading.Tasks;

namespace CookBook.Data.Shared
{
    public interface IDbQueryRunner : IDisposable
    {
        Task RunQueryAsync(string query, params object[] parameters);
    }
}