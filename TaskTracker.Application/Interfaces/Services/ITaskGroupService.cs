using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskGroupService
{
    public Task<IEnumerable<TaskGroup>> GetTaskGroups();

    public Task<IEnumerable<TaskGroup>> PostTaskGroups(TaskGroup[] taskGroups);

    public Task<IEnumerable<TaskGroup>> PutTaskGroups(TaskGroup[] taskGroups);

    public Task<IEnumerable<int>> DeleteTaskGroups(int[] taskGroupIds);

}
