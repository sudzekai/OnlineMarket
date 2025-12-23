namespace DTO.Models.Products
{
    public class ProductUpdateDto
    {
        public int CategoryId { get; set; }

        public int ProducerId { get; set; }

        public int SupplierId { get; set; }

        public string Name { get; set; } = null!;

        public string Measurement { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public short Discount { get; set; }

        public string Description { get; set; } = null!;

        public string? ImageName { get; set; }
    }
}
