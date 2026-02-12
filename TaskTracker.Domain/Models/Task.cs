using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskTracker.Domain.Models;

public class Task
{
    [Column("taskid")]
    public int TaskId { get; set; }

    [Column("taskdescription")]
    public string TaskDescription { get; set; } = string.Empty;

    [Column("taskcreatedatutc")]
    public DateTime TaskCreatedAtUtc { get; set; }

    [Column("taskupdatedatutc")]
    public DateTime TaskUpdatedAtUtc { get; set; }

    [Column("taskcompletedatutc")]
    public DateTime? TaskCompletedAtUtc { get; set; }

    [Column("taskdueatutc")]
    public DateTime? TaskDueAtUtc { get; set; }

    [Column("taskdeletedatutc")]
    public DateTime? TaskDeletedAtUtc { get; set; }

    [Column("tasksortorder")]
    public int TaskSortOrder { get; set; }

    [Column("taskpriority")]
    public short TaskPriority { get; set; }

    [Column("taskgroupid")]
    public int TaskGroupId { get; set; }

    [JsonIgnore]
    public TaskGroup? TaskGroup { get; set; }
}