using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.Application.Services;

public class TaskService(ITaskRepository taskRepository) : ITaskService
{
    public async Task<IEnumerable<Domain.Models.Task>> GetTasksByTaskGroup(int taskGroupId)
    {
        return await taskRepository.GetTasksByTaskGroup(taskGroupId);
    }

    public async Task<IEnumerable<Domain.Models.Task>> CreateTasks(Domain.Models.Task[] tasks)
    {
        return await taskRepository.CreateTasks(tasks);
    }

    public async Task<IEnumerable<Domain.Models.Task>> PutTasks(Domain.Models.Task[] tasks)
    {
        return await taskRepository.PutTasks(tasks);
    }

    public async Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete)
    {
        return await taskRepository.DeleteTasks(tasksToDelete);
    }
}
