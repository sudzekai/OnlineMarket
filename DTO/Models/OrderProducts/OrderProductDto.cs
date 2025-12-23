namespace DTO.Models.OrderProducts
{
    public class OrderProductDto
    {
        public int OrderId { get; set; }

        public string ProductArticle { get; set; } = null!;

        public int Amount { get; set; }
    }
}
