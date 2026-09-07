using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportTicketApp.Api.Models;

[Index("Email", Name = "UQ_Technicians_Email", IsUnique = true)]
public partial class Technician
{
    [Key]
    public int TechnicianId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    [Precision(0)]
    public DateTime CreatedAtUtc { get; set; }

    [InverseProperty("Technician")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
