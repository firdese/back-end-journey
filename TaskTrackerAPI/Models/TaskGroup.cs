using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTrackerAPI.Models
{
    public class TaskGroup
    {
        [Column("taskgroupid")]
        public int TaskGroupId { get; set; }
        
        [Column("taskgroupdescription")]
        public string TaskGroupDescription { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
