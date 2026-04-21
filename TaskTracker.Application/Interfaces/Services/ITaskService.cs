namespace TaskTracker.Application.Interfaces.Services;

using TaskTracker.Application.Dtos.Task;
using ModelTask = Domain.Models.Task; 
public interface ITaskService
{
    public Task<IEnumerable<TaskResponseDto>> GetTasksByTaskGroup(int taskGroupId);

    public Task<IEnumerable<TaskResponseDto>> CreateTasks(TaskRequestDto[] tasks);

    public Task<IEnumerable<TaskResponseDto>> PutTasks(TaskRequestDto[] tasks);

    public Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete);
}
