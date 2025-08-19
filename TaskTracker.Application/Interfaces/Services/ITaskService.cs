namespace TaskTracker.Application.Interfaces.Services;
using ModelTask = Domain.Models.Task; 
public interface ITaskService
{
    public Task<IEnumerable<ModelTask>> GetTasks();

    public Task CreateTasks(ModelTask[] tasks);

    public Task PutTasks(ModelTask[] tasks);

    public Task DeleteTasks(ModelTask[] tasksToDelete);
}