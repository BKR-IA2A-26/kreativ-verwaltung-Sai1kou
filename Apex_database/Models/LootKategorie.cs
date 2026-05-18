using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class LootKategorie
{
    public int KategorieId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<SpawnRate> SpawnRates { get; set; } = new List<SpawnRate>();

    public virtual ICollection<HeilItem> Heils { get; set; } = new List<HeilItem>();

    public virtual ICollection<Waffe> Waffes { get; set; } = new List<Waffe>();
}
