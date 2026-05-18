using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class ZonePoi
{
    public int PoiId { get; set; }

    public int? KarteId { get; set; }

    public string Name { get; set; } = null!;

    public string? LootTier { get; set; }

    public virtual Karte? Karte { get; set; }

    public virtual ICollection<SpawnRate> SpawnRates { get; set; } = new List<SpawnRate>();
}
