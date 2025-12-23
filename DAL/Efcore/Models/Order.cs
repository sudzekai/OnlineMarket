using System;
using System.Collections.Generic;

namespace DAL.Efcore.Models;

public partial class Order
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly DeliveryDate { get; set; }

    public int ReceiveCode { get; set; }

    public string Status { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
