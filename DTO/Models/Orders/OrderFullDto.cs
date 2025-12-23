namespace DTO.Models.Orders
{
    public class OrderFullDto
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateOnly OrderDate { get; set; }

        public DateOnly DeliveryDate { get; set; }

        public int ReceiveCode { get; set; }

        public string Status { get; set; } = null!;
    }
}
