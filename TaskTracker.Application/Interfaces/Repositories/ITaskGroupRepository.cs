using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskGroupRepository
{
    public Task<IEnumerable<TaskGroup>> GetTaskGroupsByOwner(string ownerId);

    public Task<IEnumerable<TaskGroup>> PostTaskGroups(TaskGroup[] taskGroups);

    public Task<IEnumerable<TaskGroup>> PutTaskGroups(TaskGroup[] taskGroups);

    public Task<IEnumerable<int>> DeleteTaskGroups(int[] taskGroupIds);

}
