using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SupportTicketApp.Api.Models;

[Index("Email", Name = "UQ_Customers_Email", IsUnique = true)]
public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [StringLength(150)]
    public string CompanyName { get; set; } = null!;

    [StringLength(100)]
    public string ContactName { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    [StringLength(25)]
    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    [Precision(0)]
    public DateTime CreatedAtUtc { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
