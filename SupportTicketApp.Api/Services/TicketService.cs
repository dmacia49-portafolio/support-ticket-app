using Microsoft.EntityFrameworkCore;
using SupportTicketApp.Api.Data;
using SupportTicketApp.Api.DTOs;
using SupportTicketApp.Api.Models;

namespace SupportTicketApp.Api.Services;

public class TicketService : ITicketService
{
    private readonly ApplicationDbContext _context;

    public TicketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TicketDto>> GetAllAsync()
    {
        return await _context
            .Tickets.AsNoTracking()
            .OrderBy(ticket => ticket.TicketId)
            .Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,

                CustomerId = ticket.CustomerId,
                CompanyName = ticket.Customer.CompanyName,
                ContactName = ticket.Customer.ContactName,

                Status = ticket.TicketStatus.StatusName,
                TicketStatusId = ticket.TicketStatusId,

                TechnicianId = ticket.TechnicianId,
                TechnicianName =
                    ticket.Technician == null
                        ? "Unassigned"
                        : ticket.Technician.FirstName + " " + ticket.Technician.LastName,

                CreatedAtUtc = ticket.CreatedAtUtc,
            })
            .ToListAsync();
    }

    public async Task<TicketDto?> GetByIdAsync(int ticketId)
    {
        return await _context
            .Tickets.AsNoTracking()
            .Where(ticket => ticket.TicketId == ticketId)
            .Select(ticket => new TicketDto
            {
                TicketId = ticket.TicketId,
                Title = ticket.Title,
                Description = ticket.Description,
                Priority = ticket.Priority,

                CustomerId = ticket.CustomerId,
                CompanyName = ticket.Customer.CompanyName,
                ContactName = ticket.Customer.ContactName,

                Status = ticket.TicketStatus.StatusName,
                TicketStatusId = ticket.TicketStatusId,

                TechnicianId = ticket.TechnicianId,
                TechnicianName =
                    ticket.Technician == null
                        ? "Unassigned"
                        : ticket.Technician.FirstName + " " + ticket.Technician.LastName,

                CreatedAtUtc = ticket.CreatedAtUtc,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<TicketDto> CreateAsync(CreateTicketDto createTicket)
    {
        bool customerExists = await _context.Customers.AnyAsync(customer =>
            customer.CustomerId == createTicket.CustomerId
        );

        if (!customerExists)
        {
            throw new ArgumentException($"Customer {createTicket.CustomerId} does not exist.");
        }

        bool statusExists = await _context.TicketStatuses.AnyAsync(status =>
            status.TicketStatusId == createTicket.TicketStatusId
        );

        if (!statusExists)
        {
            throw new ArgumentException(
                $"Ticket status {createTicket.TicketStatusId} does not exist."
            );
        }

        if (createTicket.TechnicianId.HasValue)
        {
            bool technicianExists = await _context.Technicians.AnyAsync(technician =>
                technician.TechnicianId == createTicket.TechnicianId.Value
            );

            if (!technicianExists)
            {
                throw new ArgumentException(
                    $"Technician {createTicket.TechnicianId} does not exist."
                );
            }
        }

        int highestTicketId =
            await _context.Tickets.MaxAsync(ticket => (int?)ticket.TicketId) ?? 1000;

        int nextTicketId = highestTicketId + 1;

        var ticket = new Ticket
        {
            TicketId = nextTicketId,
            Title = createTicket.Title.Trim(),
            Description = createTicket.Description?.Trim(),
            Priority = createTicket.Priority,
            CustomerId = createTicket.CustomerId,
            TicketStatusId = createTicket.TicketStatusId,
            TechnicianId = createTicket.TechnicianId,
            CreatedAtUtc = DateTime.UtcNow,
        };

        _context.Tickets.Add(ticket);

        await _context.SaveChangesAsync();

        return await GetByIdAsync(ticket.TicketId)
            ?? throw new InvalidOperationException(
                "The ticket was created but could not be retrieved."
            );
    }

    public async Task<TicketDto?> UpdateAsync(int ticketId, UpdateTicketDto updateTicket)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket =>
            ticket.TicketId == ticketId
        );

        if (ticket is null)
        {
            return null;
        }

        bool customerExists = await _context.Customers.AnyAsync(customer =>
            customer.CustomerId == updateTicket.CustomerId
        );

        if (!customerExists)
        {
            throw new ArgumentException($"Customer {updateTicket.CustomerId} does not exist.");
        }

        bool statusExists = await _context.TicketStatuses.AnyAsync(status =>
            status.TicketStatusId == updateTicket.TicketStatusId
        );

        if (!statusExists)
        {
            throw new ArgumentException(
                $"Ticket status {updateTicket.TicketStatusId} does not exist."
            );
        }

        if (updateTicket.TechnicianId.HasValue)
        {
            bool technicianExists = await _context.Technicians.AnyAsync(technician =>
                technician.TechnicianId == updateTicket.TechnicianId.Value
            );

            if (!technicianExists)
            {
                throw new ArgumentException(
                    $"Technician {updateTicket.TechnicianId} does not exist."
                );
            }
        }

        ticket.Title = updateTicket.Title.Trim();
        ticket.Description = updateTicket.Description?.Trim();
        ticket.Priority = updateTicket.Priority;
        ticket.CustomerId = updateTicket.CustomerId;
        ticket.TicketStatusId = updateTicket.TicketStatusId;
        ticket.TechnicianId = updateTicket.TechnicianId;

        await _context.SaveChangesAsync();

        return await GetByIdAsync(ticketId);
    }

    public async Task<bool> DeleteAsync(int ticketId)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(ticket =>
            ticket.TicketId == ticketId
        );

        if (ticket is null)
        {
            return false;
        }

        _context.Tickets.Remove(ticket);

        await _context.SaveChangesAsync();

        return true;
    }
}
