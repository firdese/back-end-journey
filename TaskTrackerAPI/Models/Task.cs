using System.Text.Json.Serialization;

namespace TaskTrackerAPI.Models
{
    public class Task
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public bool TaskCompleted { get; set; } 

        public int TaskGroupId { get; set; }
        [JsonIgnore]
        public TaskGroup? TaskGroup { get; set; }
    }
}
