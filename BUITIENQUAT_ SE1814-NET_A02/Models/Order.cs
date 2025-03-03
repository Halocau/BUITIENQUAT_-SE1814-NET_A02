using System;
using System.Collections.Generic;

namespace BUITIENQUAT__SE1814_NET_A02.Models;

public partial class Order
{
    public string OrderId { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public string? CustomerId { get; set; }

    public decimal? TotalPayment { get; set; }
}
