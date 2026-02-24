using System;
using System.Collections.Generic;

namespace QuickSale.Business.DTOs
{
    public class SaleResponseDto
    {
        public int SaleId { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}