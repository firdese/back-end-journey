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

    public async Task<IEnumerable<TaskGroup>> PostTaskGroups(TaskGroup[] taskGroups)
    { 
        return await repository.PostTaskGroups(taskGroups);
    }

    public async Task<IEnumerable<TaskGroup>> PutTaskGroups(TaskGroup[] taskGroups)
    {
        return await repository.PutTaskGroups(taskGroups);
    }

    public async Task<IEnumerable<int>> DeleteTaskGroups(int[] taskGroupIds)
    {
        return await repository.DeleteTaskGroups(taskGroupIds);
    }
}
