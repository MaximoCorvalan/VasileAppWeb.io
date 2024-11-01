using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiVasile.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<bool> AddAsync(T generic);
        Task<bool> UpdateAsync(T generic, string query);
        Task<bool> DeleteAsync(string key);
        Task<T> Find(string key);
    }
}
