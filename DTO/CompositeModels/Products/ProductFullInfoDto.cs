using DTO.Models.Products;

namespace DTO.CompositeModels.Products
{
    public class ProductFullInfoDto
    {
        public ProductFullDto Product { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string ProducerName { get; set; } = null!;

        public string SupplierName { get; set; } = null!;

        public decimal DiscountedPrice => Product.Price * (1 - Product.Discount / 100m);
    }
}
