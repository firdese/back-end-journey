using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskTracker.Domain.Models;

public class Task
{
    [Column("taskid")]
    public int TaskId { get; set; }
        
    [Column("taskdescription")]
    public string TaskDescription { get; set; }
        
    [Column("taskcompleted")]
    public bool TaskCompleted { get; set; } 

    [Column("taskgroupid")]
    public int TaskGroupId { get; set; }
        
    [JsonIgnore]
    public TaskGroup? TaskGroup { get; set; }
}