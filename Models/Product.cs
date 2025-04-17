using System;
using System.Collections.Generic;

namespace Module2.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public int IdProductType { get; set; }

    public string NameProduct { get; set; } = null!;

    public int ArticleProduct { get; set; }

    public decimal MinimalCostForAPartner { get; set; }

    public virtual ProductType IdProductTypeNavigation { get; set; } = null!;

    public virtual ICollection<PartnerProduct> PartnerProducts { get; set; } = new List<PartnerProduct>();
}
