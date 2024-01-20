using Core.Context;
using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;

        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Product> GetProductByIdAsync(int? id)
            => await _context.Set<Product>().FindAsync(id);


        public async Task<IReadOnlyList<Product>> GetProductsAsync()
            => await _context.Set<Product>().ToListAsync();


        public async Task<IReadOnlyList<ProductBrand>> GetProductsBrandAsync()
            => await _context.Set<ProductBrand>().ToListAsync();


        public async Task<IReadOnlyList<ProductType>> GetProductsTypesAsync()
             => await _context.Set<ProductType>().ToListAsync();

    }
}
