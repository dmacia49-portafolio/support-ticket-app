using SupportTicketApp.Client.Models;

namespace SupportTicketApp.Client.Services;

public interface ITicketApiService
{
    Task<List<TicketDto>> GetAllAsync();

    Task<TicketDto?> GetByIdAsync(int ticketId);

    Task<TicketDto> CreateAsync(CreateTicketDto ticket);

    Task<TicketDto> UpdateAsync(int ticketId, UpdateTicketDto ticket);

    Task<List<LookupDto>> GetCustomersAsync();

    Task<List<LookupDto>> GetStatusesAsync();

    Task<List<LookupDto>> GetTechniciansAsync();

    Task<bool> DeleteAsync(int ticketId);
}
