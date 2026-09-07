using System.ComponentModel.DataAnnotations;

namespace SupportTicketApp.Api.DTOs;

public class CreateTicketDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required]
    [RegularExpression(
        "^(Low|Medium|High|Critical)$",
        ErrorMessage = "Priority must be Low, Medium, High, or Critical."
    )]
    public string Priority { get; set; } = "Medium";

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int TicketStatusId { get; set; }

    public int? TechnicianId { get; set; }
}
