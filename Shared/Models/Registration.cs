using System;
using System.Collections.Generic;

namespace Events_WebAPP.Server;

public partial class Registration
{
    public int RegistrationId { get; set; }

    public int? EvtId { get; set; }

    public int? ParticipantId { get; set; }

    public virtual Event? Evt { get; set; }

    public virtual User? Participant { get; set; }
}
