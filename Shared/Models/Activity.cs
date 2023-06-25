using System;
using System.Collections.Generic;

namespace Events_WebAPP.Server;

public partial class Activity
{
    public int ActivityId { get; set; }

    public int? EventId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string? Description { get; set; }

    public virtual Event? Event { get; set; }
}
