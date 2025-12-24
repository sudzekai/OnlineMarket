using DTO.CompositeModels.OrderProducts;
using DTO.CompositeModels.Products;
using DTO.Models.Orders;

namespace DTO.CompositeModels.Orders
{
    /// <summary>
    /// Полная информация о заказе, включающая в себя ФИО клиента и список товаров
    /// </summary>
    public class OrderFullInfoDto
    {
        public OrderFullDto Order { get; set; } = null!;

        public List<OrderProductFullInfo> Products { get; set; } = null!;

        public string ClientFullName { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscountedPrice { get; set; }
    }
}
