using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Firma.Models.Entities;

public partial class Shipment
{
    [Key]
    [Column("ShipmentID")]
    public int ShipmentId { get; set; }

    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "date")]
    public DateTime ShipmentDate { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal ShippingCost { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("Shipments")]
    public virtual Order Order { get; set; } = null!;
}
