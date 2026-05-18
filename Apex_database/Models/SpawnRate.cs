using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class SpawnRate
{
    public int PoiId { get; set; }

    public int KategorieId { get; set; }

    public int? WahrscheinlichkeitProzent { get; set; }

    public virtual LootKategorie Kategorie { get; set; } = null!;

    public virtual ZonePoi Poi { get; set; } = null!;
}
