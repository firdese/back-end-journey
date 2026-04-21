using System;

namespace TaskTracker.Application.Dtos.Task;

public class TaskRequestDto
{
    // Id is optional for creates; clients may send 0 or omit
    public int TaskId { get; set; }

    public string? TaskTitle { get; set; }

    public string? TaskDescription { get; set; }

    // Foreign key to task group
    public int? TaskGroupId { get; set; }

    // Scheduling / lifecycle
    public DateTime? TaskDueAtUtc { get; set; }

    public DateTime? TaskCompletedAtUtc { get; set; }

    // Domain-level priority (1..N)
    public int? TaskPriority { get; set; }

    // Any other client-supplied flags (example)
    public bool? TaskIsArchived { get; set; }
}
