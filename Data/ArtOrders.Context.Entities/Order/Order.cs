﻿using System.Diagnostics.CodeAnalysis;

namespace ArtOrders.Context.Entities;

public class Order : BaseEntity
{
    public string Name { get; set; }

    [AllowNull]
    public Guid? CustomerId { get; set; }
    public virtual User Customer { get; set; }

    [AllowNull]
    public Guid? ArtistId { get; set; }
    public virtual User Artist { get; set; }

    public OrderStatus Status { get; set; }
    public string ChatLink { get; set; }

    [AllowNull]
    public int? CurrentResultId { get; set; }
    public virtual Image CurrentResultImage { get; set; }

    public int EditsNumber { get; set; }

    public virtual Chat Chat { get; set; }
}
