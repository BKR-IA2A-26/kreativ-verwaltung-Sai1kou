using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Waffe
{
    public int WaffeId { get; set; }

    public int? MunitionId { get; set; }

    public string Name { get; set; } = null!;

    public string? WaffenTyp { get; set; }

    public int? BasisSchaden { get; set; }

    public virtual Munitionstyp? Munition { get; set; }

    public virtual ICollection<Aufsatz> Aufsatzs { get; set; } = new List<Aufsatz>();

    public virtual ICollection<LootKategorie> Kategories { get; set; } = new List<LootKategorie>();
}
