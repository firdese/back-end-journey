using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.Application.Dtos;
using TaskTracker.Application.Dtos.TaskGroup;

namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskGroupService
{
    Task<IEnumerable<TaskGroupResponseDto>> GetTaskGroups();

    Task<IEnumerable<TaskGroupResponseDto>> PostTaskGroups(TaskGroupRequestDto[] taskGroups);

    Task<IEnumerable<TaskGroupResponseDto>> PutTaskGroups(TaskGroupRequestDto[] taskGroups);

    Task<int[]> DeleteTaskGroups(int[] taskGroupIds);
}
