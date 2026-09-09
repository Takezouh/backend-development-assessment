

using backend_development_assessment.api.Common;

namespace backend_development_assessment.api.DTOs;

public record CreateTicketDTO(
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    Priority Priority,
    Status Status
);

public record TicketDetailsDTO(
    int Id,
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    Priority? Priority,
    Status? Status,
    DateTime? ResolvedAt
);

public record UpdateTicketDTO(
    int Id,
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    Priority? Priority,
    Status? Status,
    DateTime? ResolvedAt
);

