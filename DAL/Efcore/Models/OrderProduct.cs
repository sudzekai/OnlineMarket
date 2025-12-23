using System;
using System.Collections.Generic;

namespace DAL.Efcore.Models;

public partial class OrderProduct
{
    public int OrderId { get; set; }

    public string ProductArticle { get; set; } = null!;

    public int Amount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product ProductArticleNavigation { get; set; } = null!;
}
