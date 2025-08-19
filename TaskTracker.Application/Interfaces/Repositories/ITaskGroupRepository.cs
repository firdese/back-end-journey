using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskGroupRepository
{
    public Task<IEnumerable<TaskGroup>> GetTaskGroups();

    public Task PostTaskGroups(TaskGroup[] taskGroups);

    public Task PutTaskGroups(TaskGroup[] taskGroups);

    public Task DeleteTaskGroups(TaskGroup[] taskGroups);

}