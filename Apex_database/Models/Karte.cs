using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Karte
{
    public int KarteId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ZonePoi> ZonePois { get; set; } = new List<ZonePoi>();
}
