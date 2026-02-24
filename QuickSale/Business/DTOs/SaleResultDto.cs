using System;

namespace QuickSale.Business.DTOs
{
    public class SaleResultDto
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime SaleDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}