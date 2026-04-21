using System;

namespace TaskTracker.Application.Dtos.TaskGroup;

public sealed class TaskGroupResponseDto
{
    public int TaskGroupId { get; set; }

    public string TaskGroupDescription { get; set; } = string.Empty;

    public DateTime TaskGroupCreatedAtUtc { get; set; }

    public DateTime TaskGroupUpdatedAtUtc { get; set; }

    public DateTime? TaskGroupArchivedAtUtc { get; set; }

    public string? TaskGroupColor { get; set; }

    public int TaskGroupSortOrder { get; set; }
}
