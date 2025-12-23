namespace DTO.Models.Orders
{
    public class OrderCreateDto
    {
        public DateOnly OrderDate { get; set; }

        public DateOnly DeliveryDate { get; set; }

        public int ReceiveCode { get; set; }

        public string Status { get; set; } = null!;
    }
}
