using System;
using System.Collections.Generic;

namespace Buitienquat_SE1814_NET_A02.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 3);

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public int? DiscountRate { get; set; }
}
