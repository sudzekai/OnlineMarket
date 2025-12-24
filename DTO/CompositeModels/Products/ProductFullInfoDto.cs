using DTO.Models.Products;

namespace DTO.CompositeModels.Products
{
    /// <summary>
    /// Полная информация о товаре, включающая в себя цену с учетом скидки 
    /// названия категории, производителя и поставщика
    /// </summary>
    public class ProductFullInfoDto
    {
        public ProductFullDto Product { get; set; } = new();

        public string CategoryName { get; set; } = null!;

        public string ProducerName { get; set; } = null!;

        public string SupplierName { get; set; } = null!;

        public decimal DiscountedPrice { get; set; }
    }
}
