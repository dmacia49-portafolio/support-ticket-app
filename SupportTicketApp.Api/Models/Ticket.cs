using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportTicketApp.Api.Models;

[Index("CustomerId", Name = "IX_Tickets_CustomerId")]
[Index("Priority", Name = "IX_Tickets_Priority")]
[Index("TechnicianId", Name = "IX_Tickets_TechnicianId")]
[Index("TicketStatusId", Name = "IX_Tickets_TicketStatusId")]
public partial class Ticket
{
    [Key]
    public int TicketId { get; set; }

    public int CustomerId { get; set; }

    public int? TechnicianId { get; set; }

    public int TicketStatusId { get; set; }

    [StringLength(200)]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [StringLength(20)]
    public string Priority { get; set; } = null!;

    [Precision(0)]
    public DateTime CreatedAtUtc { get; set; }

    [Precision(0)]
    public DateTime? UpdatedAtUtc { get; set; }

    [Precision(0)]
    public DateTime? ResolvedAtUtc { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Tickets")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("TechnicianId")]
    [InverseProperty("Tickets")]
    public virtual Technician? Technician { get; set; }

    [ForeignKey("TicketStatusId")]
    [InverseProperty("Tickets")]
    public virtual TicketStatus TicketStatus { get; set; } = null!;
}
