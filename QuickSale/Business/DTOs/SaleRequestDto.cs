using System.Collections.Generic;

namespace QuickSale.Business.DTOs
{
    public class SaleRequestDto
    {
        public int CustomerId { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }
}