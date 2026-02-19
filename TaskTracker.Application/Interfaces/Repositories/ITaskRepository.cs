using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelTask = TaskTracker.Domain.Models.Task; 
    
namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<ModelTask>> GetTasksByTaskGroup(int taskGroupId);

    public Task<IEnumerable<ModelTask>> CreateTasks(ModelTask[] tasks);

    public Task<IEnumerable<ModelTask>> PutTasks(ModelTask[] tasks);

    public Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete);
}
