using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Services;

public class TaskGroupService(ITaskGroupRepository repository) : ITaskGroupService
{
    public async Task<IEnumerable<TaskGroup>> GetTaskGroups()
    {
        return await repository.GetTaskGroups();
    }

    public async Task PostTaskGroups(TaskGroup[] taskGroups)
    { 
        await repository.PostTaskGroups(taskGroups);
    }

    public async Task PutTaskGroups(TaskGroup[] taskGroups)
    {
        await repository.PutTaskGroups(taskGroups);
    }

    public async Task DeleteTaskGroups(TaskGroup[] taskGroups)
    {
        await repository.DeleteTaskGroups(taskGroups);
    }
}