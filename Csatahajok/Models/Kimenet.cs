using System;
using System.Collections.Generic;

namespace Csatahajok.Models;

public partial class Kimenet
{
    public string Hajo { get; set; } = null!;

    public string Csata { get; set; } = null!;

    public string? Eredmeny { get; set; }

    public virtual Csatum? CsataNavigation { get; set; } = null!;

    public virtual Hajo? HajoNavigation { get; set; } = null!;
}
