using System;
using System.Collections.Generic;

namespace Module2.Models;

public partial class MaterialType
{
    public int IdMaterialType { get; set; }

    public string TypeMaterial { get; set; } = null!;

    public decimal PercentDefect { get; set; }
}
