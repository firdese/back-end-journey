using System;

namespace TaskTracker.Application.Dtos.Task;

public class TaskResponseDto
{
    public int TaskId { get; set; }

    public string? TaskTitle { get; set; }

    public string? TaskDescription { get; set; }

    public int? TaskGroupId { get; set; }

    public int? TaskPriority { get; set; }

    public bool TaskIsArchived { get; set; }

    // Timestamps
    public DateTime? TaskCreatedAtUtc { get; set; }

    public DateTime? TaskUpdatedAtUtc { get; set; }

    public DateTime? TaskArchivedAtUtc { get; set; }

    public DateTime? TaskDueAtUtc { get; set; }

    public DateTime? TaskCompletedAtUtc { get; set; }

    // Owner info (optional)
    public string? OwnerUserId { get; set; }
}
