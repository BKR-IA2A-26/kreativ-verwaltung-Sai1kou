using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class HeilItem
{
    public int HeilId { get; set; }

    public string Name { get; set; } = null!;

    public int? HeilungHp { get; set; }

    public int? HeilungSchild { get; set; }

    public decimal? AnwendungsdauerSek { get; set; }

    public virtual ICollection<LootKategorie> Kategories { get; set; } = new List<LootKategorie>();
}
