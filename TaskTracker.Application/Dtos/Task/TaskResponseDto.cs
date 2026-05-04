using System;

namespace TaskTracker.Application.Dtos.Task;

public class TaskResponseDto
{
    public int TaskId { get; set; }

    public string TaskDescription { get; set; } = string.Empty;

    public DateTime? TaskCompletedAtUtc { get; set; }

    public DateTime? TaskStartAtUtc { get; set; }

    public DateTime? TaskEndAtUtc { get; set; }

    public int? TaskProgress { get; set; }

    public DateTime? TaskDueAtUtc { get; set; }

    public DateTime? TaskDeletedAtUtc { get; set; }

    public int TaskSortOrder { get; set; }

    public short TaskPriority { get; set; }

    public int TaskGroupId { get; set; }

    public DateTime TaskCreatedAtUtc { get; set; }

    public DateTime TaskUpdatedAtUtc { get; set; }
}
