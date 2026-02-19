namespace TaskTracker.Application.Interfaces.Services;
using ModelTask = Domain.Models.Task; 
public interface ITaskService
{
    public Task<IEnumerable<ModelTask>> GetTasksByTaskGroup(int taskGroupId);

    public Task<IEnumerable<ModelTask>> CreateTasks(ModelTask[] tasks);

    public Task<IEnumerable<ModelTask>> PutTasks(ModelTask[] tasks);

    public Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete);
}
