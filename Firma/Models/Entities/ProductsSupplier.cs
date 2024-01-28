using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Firma.Models.Entities;

[Table("Products_Suppliers")]
public partial class ProductsSupplier
{
    [Key]
    [Column("ProductSupplierID")]
    public int ProductSupplierId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    [Column("SupplierID")]
    public int SupplierId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductsSuppliers")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("SupplierId")]
    [InverseProperty("ProductsSuppliers")]
    public virtual Supplier Supplier { get; set; } = null!;
}
