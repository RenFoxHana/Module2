using System;
using System.Collections.Generic;

namespace Module2.Models;

public partial class PartnerProduct
{
    public int IdPartnerProduct { get; set; }

    public int IdProduct { get; set; }

    public int IdPartner { get; set; }

    public long CountProduct { get; set; }

    public DateOnly DateSale { get; set; }

    public virtual Partner IdPartnerNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
