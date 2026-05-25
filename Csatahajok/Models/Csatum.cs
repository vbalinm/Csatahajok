using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Csatahajok.Models;

public partial class Csatum
{
    public string Nev { get; set; } = null!;

    public DateTime? Kezdes { get; set; }

    public DateTime? Befejezes { get; set; }
    [JsonIgnore]
    public virtual ICollection<Kimenet> Kimenets { get; set; } = new List<Kimenet>();
}
