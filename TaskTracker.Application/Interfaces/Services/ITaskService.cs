namespace TaskTracker.Application.Interfaces.Services;

using TaskTracker.Application.Dtos.Task;

public interface ITaskService
{
    public Task<IEnumerable<TaskResponseDto>> GetTasksByTaskGroup(int taskGroupId);

    public Task<IEnumerable<TaskResponseDto>> CreateTasks(CreateTaskRequestDto[] tasks);

    public Task<IEnumerable<TaskResponseDto>> PutTasks(UpdateTaskRequestDto[] tasks);

    public Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete);
}
