using System;

namespace QuickSale.Infrastructure.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}