using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Application.Dtos.Task;

public class CreateTaskRequestDto
{
    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string TaskDescription { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int TaskGroupId { get; set; }

    public DateTime? TaskCompletedAtUtc { get; set; }

    public DateTime? TaskStartAtUtc { get; set; }

    public DateTime? TaskEndAtUtc { get; set; }

    [Range(0, 100)]
    public int? TaskProgress { get; set; }

    public DateTime? TaskDueAtUtc { get; set; }

    [Range(0, int.MaxValue)]
    public int TaskSortOrder { get; set; }

    [Range(0, short.MaxValue)]
    public short TaskPriority { get; set; }
}

public class UpdateTaskRequestDto
{
    [Range(1, int.MaxValue)]
    public int TaskId { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public string TaskDescription { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int TaskGroupId { get; set; }

    public DateTime? TaskCompletedAtUtc { get; set; }

    public DateTime? TaskStartAtUtc { get; set; }

    public DateTime? TaskEndAtUtc { get; set; }

    [Range(0, 100)]
    public int? TaskProgress { get; set; }

    public DateTime? TaskDueAtUtc { get; set; }

    public DateTime? TaskDeletedAtUtc { get; set; }

    [Range(0, int.MaxValue)]
    public int TaskSortOrder { get; set; }

    [Range(0, short.MaxValue)]
    public short TaskPriority { get; set; }
}
