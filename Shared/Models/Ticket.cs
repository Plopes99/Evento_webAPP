using System;
using System.Collections.Generic;

namespace Events_WebAPP.Server;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? EventId { get; set; }

    public string TicketType { get; set; } = null!;

    public int QuantityAvailable { get; set; }

    public virtual Event? Event { get; set; }
}
