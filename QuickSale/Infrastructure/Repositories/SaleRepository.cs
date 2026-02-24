using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using QuickSale.Business.DTOs;

namespace QuickSale.Infrastructure.Repositories
{
    public class SaleRepository
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["SalesConnectionString"].ConnectionString;

        public int CreateSale(int customerId, decimal total, List<SaleItemDto> items)
        {
            var xmlItems = new XElement("Items",
                items.Select(i => new XElement("Item",
                    new XElement("ProductId", i.ProductId),
                    new XElement("Quantity", i.Quantity),
                    new XElement("UnitPrice", i.UnitPrice)
                ))
            );

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_CreateSale", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@ItemsXML", xmlItems.ToString());

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
    }
}