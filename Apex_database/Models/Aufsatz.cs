using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Aufsatz
{
    public int AufsatzId { get; set; }

    public string Name { get; set; } = null!;

    public string Typ { get; set; } = null!;

    public string? Seltenheit { get; set; }

    public virtual ICollection<Waffe> Waffes { get; set; } = new List<Waffe>();
}
