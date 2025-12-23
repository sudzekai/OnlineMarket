namespace DTO.Models.Orders
{
    public class OrderUpdateDto
    {
        public DateOnly DeliveryDate { get; set; }

        public int ReceiveCode { get; set; }

        public string Status { get; set; } = null!;
    }
}
