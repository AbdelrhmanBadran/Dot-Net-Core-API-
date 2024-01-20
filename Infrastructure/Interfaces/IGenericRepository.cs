using Core.Entities;
using Infrastructure.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository<T> where T :BaseEntity
    {
        Task<T> GetByIdAsync(int? id);
        Task<IReadOnlyList<T>> GetByAllAsync();
        Task Add(T entity);
        Task<T> GetEntityWithSpecificationsAsync(ISpecifications<T> specifications);
        Task<int> CountAsync(ISpecifications<T> specifications);
        Task<IReadOnlyList<T>> GetAllyWithSpecificationsAsync(ISpecifications<T> specifications);
        void Update(T entity);
        void Delete(T entity);
    }
}
