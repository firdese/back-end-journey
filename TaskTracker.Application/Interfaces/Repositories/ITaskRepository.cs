using ModelTask = TaskTracker.Domain.Models.Task; 
    
namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<ModelTask>> GetTasksByTaskGroup(int taskGroupId, string ownerId);

    public Task<IEnumerable<ModelTask>> CreateTasks(ModelTask[] tasks, string ownerId);

    public Task<IEnumerable<ModelTask>> PutTasks(ModelTask[] tasks, string ownerId);

    public Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete, string ownerId);
}
