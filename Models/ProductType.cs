using System;
using System.Collections.Generic;

namespace Module2.Models;

public partial class ProductType
{
    public int IdProductType { get; set; }

    public string NameType { get; set; } = null!;

    public decimal CoefficentType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
