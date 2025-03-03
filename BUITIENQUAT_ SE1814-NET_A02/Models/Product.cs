using System;
using System.Collections.Generic;

namespace BUITIENQUAT__SE1814_NET_A02.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? UnitInStock { get; set; }
}
