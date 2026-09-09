

using backend_development_assessment.api.Data;
using backend_development_assessment.api.DTOs;
using backend_development_assessment.api.Models;
using backend_development_assessment.api.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_development_assessment.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IValidator<CreateTicketDTO> _createValidator;
    private readonly IValidator<UpdateTicketDTO> _updateValidator;

    public TicketController(
        AppDbContext context,
        IValidator<CreateTicketDTO> createValidator,
        IValidator<UpdateTicketDTO> updateValidator
    )
    {
        _context = context;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    [HttpGet]
    public async Task<ActionResult<List<TicketDetailsDTO>>> GetAll()
    {
        var tickets = await _context.Tickets
            .Select(t => new TicketDetailsDTO(
                t.Id,
                t.requester_name,
                t.requester_email,
                t.department,
                t.subject,
                t.description,
                t.priority,
                t.status,
                t.resolved_at,
                t.updated_at
            )
            ).ToListAsync();

        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDetailsDTO>> GetById(int id)
    {
        var response = await _context.Tickets
            .Where(t => t.Id == id)
            .Select(t => new TicketDetailsDTO(
                t.Id,
                t.requester_name,
                t.requester_email,
                t.department,
                t.subject,
                t.description,
                t.priority,
                t.status,
                t.resolved_at,
                t.updated_at
            )
            ).FirstOrDefaultAsync();

        if (response == null)
            return NotFound(new { message = $"Ticket not found" });

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTicketDTO request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            }));
        }

        var ticket = new Ticket
        {
            requester_name = request.Name,
            requester_email = request.Email,
            department = request.Department,
            subject = request.Subject,
            description = request.Description,
            priority = request.Priority,
            status = request.Status,
            updated_at = DateTime.UtcNow
        };

        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateTicketDTO request)
    {
        var validateResult = await _updateValidator.ValidateAsync(request);

        if (!validateResult.IsValid)
            return BadRequest(validateResult.Errors.Select(e => new
            {
                field = e.PropertyName,
                error = e.ErrorMessage
            }));

        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null)
            return NotFound(new { message = "Ticket not found" });

        // Update only sent fields
        if (request.Name is not null)
            ticket.requester_name = request.Name;
        if (request.Email is not null)
            ticket.requester_email = request.Email;
        if (request.Department is not null)
            ticket.department = request.Department;
        if (request.Subject is not null)
            ticket.subject = request.Subject;
        if (request.Description is not null)
            ticket.description = request.Description;
        if (request.Priority is not null)
            ticket.priority = request.Priority;
        if (request.Status is not null)
        {
            if (request.Status == "open" || request.Status == "in_progress")
            {
                ticket.status = request.Status;
                ticket.updated_at = DateTime.UtcNow;
                ticket.resolved_at = null;
            }
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);

        if (ticket == null)
            return NotFound(new { message = "Ticket not found" });

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}