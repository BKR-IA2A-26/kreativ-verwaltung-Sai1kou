using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Munitionstyp
{
    public int MunitionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Waffe> Waffes { get; set; } = new List<Waffe>();
}
