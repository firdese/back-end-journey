using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Application.Dtos.TaskGroup;

public sealed class CreateTaskGroupRequestDto
{
    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string TaskGroupDescription { get; set; } = string.Empty;

    [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
    public string? TaskGroupColor { get; set; }

    [Range(0, int.MaxValue)]
    public int TaskGroupSortOrder { get; set; }
}

public sealed class UpdateTaskGroupRequestDto
{
    [Range(1, int.MaxValue)]
    public int TaskGroupId { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string TaskGroupDescription { get; set; } = string.Empty;

    [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$")]
    public string? TaskGroupColor { get; set; }

    [Range(0, int.MaxValue)]
    public int TaskGroupSortOrder { get; set; }
}
