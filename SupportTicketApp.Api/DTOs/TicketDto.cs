namespace SupportTicketApp.Api.DTOs;

public class TicketDto
{
    public int TicketId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string Priority { get; set; } = string.Empty;

    public int CustomerId { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public int TicketStatusId { get; set; }

    public string Status { get; set; } = string.Empty;

    public int? TechnicianId { get; set; }

    public string TechnicianName { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; }
}
