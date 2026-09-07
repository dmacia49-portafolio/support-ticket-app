using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportTicketApp.Api.Models;

[Index("DisplayOrder", Name = "UQ_TicketStatuses_DisplayOrder", IsUnique = true)]
[Index("StatusName", Name = "UQ_TicketStatuses_StatusName", IsUnique = true)]
public partial class TicketStatus
{
    [Key]
    public int TicketStatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    public int DisplayOrder { get; set; }

    [InverseProperty("TicketStatus")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
