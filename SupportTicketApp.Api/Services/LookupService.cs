using Microsoft.EntityFrameworkCore;
using SupportTicketApp.Api.Data;
using SupportTicketApp.Api.DTOs;

namespace SupportTicketApp.Api.Services;

public class LookupService : ILookupService
{
    private readonly ApplicationDbContext _context;

    public LookupService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LookupDto>> GetCustomersAsync()
    {
        return await _context
            .Customers.AsNoTracking()
            .OrderBy(customer => customer.CompanyName)
            .Select(customer => new LookupDto
            {
                Id = customer.CustomerId,
                Name = customer.CompanyName,
            })
            .ToListAsync();
    }

    public async Task<List<LookupDto>> GetStatusesAsync()
    {
        return await _context
            .TicketStatuses.AsNoTracking()
            .OrderBy(status => status.TicketStatusId)
            .Select(status => new LookupDto
            {
                Id = status.TicketStatusId,
                Name = status.StatusName,
            })
            .ToListAsync();
    }

    public async Task<List<LookupDto>> GetTechniciansAsync()
    {
        return await _context
            .Technicians.AsNoTracking()
            .OrderBy(technician => technician.FirstName)
            .ThenBy(technician => technician.LastName)
            .Select(technician => new LookupDto
            {
                Id = technician.TechnicianId,
                Name = technician.FirstName + " " + technician.LastName,
            })
            .ToListAsync();
    }
}
