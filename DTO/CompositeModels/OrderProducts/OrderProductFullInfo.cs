using DTO.Models.Products;
using System.ComponentModel;

namespace DTO.CompositeModels.OrderProducts
{
    public class OrderProductFullInfo
    {
        public ProductSimpleDto Product { get; set; } = new();

        public int Amount { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountedPrice { get; set; }    
    }
}
