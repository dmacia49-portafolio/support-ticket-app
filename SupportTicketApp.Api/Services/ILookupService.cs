using SupportTicketApp.Api.DTOs;

namespace SupportTicketApp.Api.Services;

public interface ILookupService
{
    Task<List<LookupDto>> GetCustomersAsync();

    Task<List<LookupDto>> GetStatusesAsync();

    Task<List<LookupDto>> GetTechniciansAsync();
}
