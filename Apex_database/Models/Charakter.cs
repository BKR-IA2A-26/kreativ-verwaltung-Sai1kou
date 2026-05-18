using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Charakter
{
    public int CharakterId { get; set; }

    public string Name { get; set; } = null!;

    public string Klasse { get; set; } = null!;

    public virtual ICollection<Faehigkeit> Faehigkeits { get; set; } = new List<Faehigkeit>();
}
