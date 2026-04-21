using System;

namespace TaskTracker.Application.Dtos.TaskGroup;

public sealed class TaskGroupRequestDto
{
    public int TaskGroupId { get; set; }

    public string TaskGroupDescription { get; set; } = string.Empty;

    public string? TaskGroupColor { get; set; }

    public int TaskGroupSortOrder { get; set; }
}
