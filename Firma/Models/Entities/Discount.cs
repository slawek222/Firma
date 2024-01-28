using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Firma.Models.Entities;

public partial class Discount
{
    [Key]
    [Column("DiscountID")]
    public int DiscountId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal DiscountAmount { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("Discounts")]
    public virtual Product Product { get; set; } = null!;
}
