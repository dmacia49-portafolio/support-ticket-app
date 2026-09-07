using Microsoft.AspNetCore.Mvc;
using SupportTicketApp.Api.DTOs;
using SupportTicketApp.Api.Services;

namespace SupportTicketApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TicketDto>>> GetAll()
    {
        var tickets = await _ticketService.GetAllAsync();

        return Ok(tickets);
    }

    [HttpGet("{ticketId:int}")]
    public async Task<ActionResult<TicketDto>> GetById(int ticketId)
    {
        var ticket = await _ticketService.GetByIdAsync(ticketId);

        if (ticket is null)
        {
            return NotFound(new { message = $"Ticket {ticketId} was not found." });
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(CreateTicketDto createTicket)
    {
        try
        {
            var createdTicket = await _ticketService.CreateAsync(createTicket);

            return CreatedAtAction(
                nameof(GetById),
                new { ticketId = createdTicket.TicketId },
                createdTicket
            );
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }

    [HttpPut("{ticketId:int}")]
    public async Task<ActionResult<TicketDto>> Update(int ticketId, UpdateTicketDto updateTicket)
    {
        try
        {
            var updatedTicket = await _ticketService.UpdateAsync(ticketId, updateTicket);

            if (updatedTicket is null)
            {
                return NotFound(new { message = $"Ticket {ticketId} was not found." });
            }

            return Ok(updatedTicket);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpDelete("{ticketId:int}")]
    public async Task<IActionResult> Delete(int ticketId)
    {
        bool deleted = await _ticketService.DeleteAsync(ticketId);

        if (!deleted)
        {
            return NotFound(new { message = $"Ticket {ticketId} was not found." });
        }

        return NoContent();
    }
}
