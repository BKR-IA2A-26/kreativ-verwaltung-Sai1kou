using System;
using System.Collections.Generic;

namespace Apex_database.Models;

public partial class Faehigkeit
{
    public int FaehigkeitId { get; set; }

    public int? CharakterId { get; set; }

    public string Name { get; set; } = null!;

    public string Typ { get; set; } = null!;

    public int? CooldownSek { get; set; }

    public virtual Charakter? Charakter { get; set; }
}
