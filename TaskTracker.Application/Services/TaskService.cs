using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.Application.Services;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public async Task<IEnumerable<Domain.Models.Task>> GetTasks()
    {
        return await taskRepository.GetTasks();
    }

    public async Task CreateTasks(Domain.Models.Task[] tasks)
    {
        await taskRepository.CreateTasks(tasks);
    }

    public async Task PutTasks(Domain.Models.Task[] tasks)
    {
        await taskRepository.PutTasks(tasks);
    }

    public async Task DeleteTasks(Domain.Models.Task[] tasksToDelete)
    {
        await taskRepository.DeleteTasks(tasksToDelete);
    }
}