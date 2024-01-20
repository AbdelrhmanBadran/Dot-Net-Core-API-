using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private Hashtable _respositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
            => await _context.SaveChangesAsync();

        public void Dispose()
            => _context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_respositories == null)
                _respositories = new Hashtable();
        
            var type = typeof(TEntity).Name;

            if(!_respositories.ContainsKey(type))
            {
                var repositoryType =typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)) , _context);
                
                _respositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_respositories[type]; 

        }
    }
}
