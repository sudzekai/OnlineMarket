namespace DTO.Models.Products
{
    public class ProductSimpleDto
    {
        public string Article { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Measurement { get; set; } = null!;

        public decimal Price { get; set; }

        public short Discount { get; set; }

        public string? ImageName { get; set; }
    }
}
