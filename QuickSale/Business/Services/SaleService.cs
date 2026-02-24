using QuickSale.Business.DTOs;
using QuickSale.Infrastructure.Entities;
using QuickSale.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickSale.Business.Services
{
    public class SaleService
    {
        private readonly SaleRepository _saleRepo = new SaleRepository();
        private readonly SalesDbContext _db = new SalesDbContext();

        public int ProcessSale(SaleRequestDto request)
        {

            var customer = _db.Customers.Find(request.CustomerId);
            if (customer == null || !customer.IsActive)
                throw new Exception("El cliente no existe o está inactivo.");

            foreach (var item in request.Items)
            {
                var prod = _db.Products.Find(item.ProductId);
                if (prod == null || !prod.IsActive)
                    throw new Exception($"El producto {item.ProductId} no está disponible.");
            }


            decimal total = request.Items.Sum(i => i.Quantity * i.UnitPrice);

            return _saleRepo.CreateSale(request.CustomerId, total, request.Items);
        }

        public SaleResponseDto GetSaleById(int id)
        {
            var sale = _db.Database.SqlQuery<SaleResponseDto>(
                @"SELECT s.SaleId, c.FullName as CustomerName, s.Total, s.CreatedAt as SaleDate 
          FROM Sales s 
          JOIN Customers c ON s.CustomerId = c.CustomerId 
          WHERE s.SaleId = @id",
                new System.Data.SqlClient.SqlParameter("@id", id)
            ).FirstOrDefault();

            if (sale != null)
            {
                sale.Items = _db.Database.SqlQuery<SaleItemDto>(
                    @"SELECT i.ProductId, p.Name as ProductName, i.Quantity, i.UnitPrice 
              FROM SaleItems i
              JOIN Products p ON i.ProductId = p.ProductId
              WHERE i.SaleId = @id",
                    new System.Data.SqlClient.SqlParameter("@id", id)
                ).ToList();
            }

            return sale;
        }

        public List<SaleResponseDto> GetSalesByCustomerId(int customerId)
        {
            var rawData = _db.Database.SqlQuery<SaleResultDto>(@"
        SELECT 
            s.SaleId, 
            c.FullName as CustomerName, 
            s.Total, 
            s.CreatedAt as SaleDate,
            i.ProductId,
            p.Name as ProductName, 
            i.Quantity, 
            i.UnitPrice
        FROM Sales s
        JOIN Customers c ON s.CustomerId = c.CustomerId
        JOIN SaleItems i ON s.SaleId = i.SaleId
        JOIN Products p ON i.ProductId = p.ProductId
        WHERE s.CustomerId = @customerId
        ORDER BY s.CreatedAt ASC",
                new System.Data.SqlClient.SqlParameter("@customerId", customerId)
            ).ToList();

            var sales = rawData
                .GroupBy(r => r.SaleId)
                .Select(g => new SaleResponseDto
                {
                    SaleId = g.Key,
                    CustomerName = g.First().CustomerName,
                    Total = g.First().Total,
                    SaleDate = g.First().SaleDate,
                    Items = g.Select(i => new SaleItemDto
                    {
                        ProductId = i.ProductId,
                        ProductName = i.ProductName,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                }).ToList();

            return sales;
        }
    }
}