using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Application.Dtos.TaskGroup;

namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskGroupService
{
    Task<IEnumerable<TaskGroupResponseDto>> GetTaskGroups();

    Task<IEnumerable<TaskGroupResponseDto>> PostTaskGroups(CreateTaskGroupRequestDto[] taskGroups);

    Task<IEnumerable<TaskGroupResponseDto>> PutTaskGroups(UpdateTaskGroupRequestDto[] taskGroups);

    Task<int[]> DeleteTaskGroups(int[] taskGroupIds);
}
