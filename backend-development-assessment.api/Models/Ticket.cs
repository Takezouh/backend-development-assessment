
using backend_development_assessment.api.Common;

namespace backend_development_assessment.api.Models;

public class Ticket
{
    public int Id { get; set; }
    public string requester_name { get; set; } = string.Empty;
    public string requester_email { get; set; } = string.Empty;
    public string department { get; set; } = string.Empty;
    public string subject { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public Priority priority { get; set; } = Priority.Low;
    public Status status { get; set; } = Status.Open;

    public DateTime? resolved_at { get; set; }
    public DateTime updated_at { get; set; } = DateTime.UtcNow;
}