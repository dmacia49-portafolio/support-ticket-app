using Microsoft.AspNetCore.Mvc;
using SupportTicketApp.Api.DTOs;
using SupportTicketApp.Api.Services;

namespace SupportTicketApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController : ControllerBase
{
    private readonly ILookupService _lookupService;

    public LookupsController(ILookupService lookupService)
    {
        _lookupService = lookupService;
    }

    [HttpGet("customers")]
    public async Task<ActionResult<List<LookupDto>>> GetCustomers()
    {
        return Ok(await _lookupService.GetCustomersAsync());
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<List<LookupDto>>> GetStatuses()
    {
        return Ok(await _lookupService.GetStatusesAsync());
    }

    [HttpGet("technicians")]
    public async Task<ActionResult<List<LookupDto>>> GetTechnicians()
    {
        return Ok(await _lookupService.GetTechniciansAsync());
    }
}
