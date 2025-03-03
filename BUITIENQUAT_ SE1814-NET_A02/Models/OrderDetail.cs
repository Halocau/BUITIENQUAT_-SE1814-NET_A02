using System;
using System.Collections.Generic;

namespace BUITIENQUAT__SE1814_NET_A02.Models;

public partial class OrderDetail
{
    public string OrderId { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? Money { get; set; }
}
