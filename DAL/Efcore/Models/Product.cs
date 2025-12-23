namespace DAL.Efcore.Models;

public partial class Product
{
    public string Article { get; set; } = null!;

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

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Producer Producer { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
