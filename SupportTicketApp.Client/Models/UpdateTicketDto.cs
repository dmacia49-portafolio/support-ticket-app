using System.ComponentModel.DataAnnotations;

namespace SupportTicketApp.Client.Models;

public class UpdateTicketDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    public string Priority { get; set; } = "Medium";

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Select a customer.")]
    public int CustomerId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Select a status.")]
    public int TicketStatusId { get; set; }

    public int? TechnicianId { get; set; }
}
