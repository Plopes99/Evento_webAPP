using System;
using System.Collections.Generic;

namespace Events_WebAPP.Server;

public partial class Event
{
    public int EventId { get; set; }

    public int? OrganizerId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Time { get; set; }

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public int MaxCapacity { get; set; }

    public decimal TicketPrice { get; set; }

    public virtual User? Organizer { get; set; }
}
