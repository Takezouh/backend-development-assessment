

namespace backend_development_assessment.api.DTOs;

public record CreateTicketDTO(
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    string Priority,
    string Status,
    DateTime? ResolvedAt,
    DateTime CreatedAt
);
public record TicketDetailsDTO(
    int Id,
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    string Priority,
    string Status,
    DateTime? ResolvedAt,
    DateTime UpdatedAt
);

public record UpdateTicketDTO(
    int Id,
    string Name,
    string Email,
    string Department,
    string Subject,
    string Description,
    string Priority,
    string Status,
    DateTime? ResolvedAt,
    DateTime UpdatedAt
);

