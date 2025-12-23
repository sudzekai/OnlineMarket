using DTO.CompositeModels.Products;
using DTO.Models.Orders;

namespace DTO.CompositeModels.Orders
{
    public class OrderFullInfoDto
    {
        public OrderFullDto Order { get; set; } = null!;

        public List<ProductFullInfoDto> Products { get; set; } = null!;

        public string ClientFullName { get; set; } = null!;
    }
}
