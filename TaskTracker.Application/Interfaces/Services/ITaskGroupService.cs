using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskGroupService
{
    public Task<IEnumerable<TaskGroup>> GetTaskGroups();

    public Task PostTaskGroups(TaskGroup[] taskGroups);

    public Task PutTaskGroups(TaskGroup[] taskGroups);

    public Task DeleteTaskGroups(TaskGroup[] taskGroups);

}