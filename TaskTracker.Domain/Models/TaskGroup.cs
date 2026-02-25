using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Domain.Models;

public class TaskGroup
{
    [Column("taskgroupid")]
    public int TaskGroupId { get; set; }

    [Column("taskgroupdescription")]
    public string TaskGroupDescription { get; set; } = string.Empty;

    [Column("taskgroupcreatedatutc")]
    public DateTime TaskGroupCreatedAtUtc { get; set; }

    [Column("taskgroupupdatedatutc")]
    public DateTime TaskGroupUpdatedAtUtc { get; set; }

    [Column("taskgrouparchivedatutc")]
    public DateTime? TaskGroupArchivedAtUtc { get; set; }

    [Column("taskgroupcolor")]
    public string? TaskGroupColor { get; set; }

    [Column("taskgroupsortorder")]
    public int TaskGroupSortOrder { get; set; }

    [Column("userid")]
    [Required]
    [MaxLength(100)]
    public string OwnerUserId { get; set; } = default!;

    public ICollection<Task>? Tasks { get; set; }
}
