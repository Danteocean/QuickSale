using QuickSale.Business.DTOs;
using QuickSale.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;

namespace QuickSale.Business.Services
{
    public class CatalogService
    {
        private readonly SalesDbContext _db = new SalesDbContext();

        public List<ProductDto> GetActiveProducts()
        {
            return _db.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();
        }

        public List<CustomerDto> GetActiveCustomers()
        {
            return _db.Customers
                .Where(c => c.IsActive)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FullName
                }).ToList();
        }
    }
}