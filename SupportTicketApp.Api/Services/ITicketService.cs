using SupportTicketApp.Api.DTOs;

namespace SupportTicketApp.Api.Services;

public interface ITicketService
{
    Task<List<TicketDto>> GetAllAsync();

    Task<TicketDto?> GetByIdAsync(int ticketId);
    Task<TicketDto> CreateAsync(CreateTicketDto createTicket);
    Task<TicketDto?> UpdateAsync(int ticketId, UpdateTicketDto updateTicket);
    Task<bool> DeleteAsync(int ticketId);
}
