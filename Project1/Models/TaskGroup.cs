namespace Project1.Models
{
    public class TaskGroup
    {
        public int TaskGroupId { get; set; }
        public string TaskGroupDescription { get; set; }
        public ICollection<Task>? Tasks { get; set; }
    }
}
