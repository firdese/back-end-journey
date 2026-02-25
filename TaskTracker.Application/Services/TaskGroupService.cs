using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTracker.Application.Dtos;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Services;

public class TaskGroupService(ITaskGroupRepository taskGroupRepository, ICurrentUserService currentUserService) : ITaskGroupService
{
    public async Task<IEnumerable<TaskGroupResponseDto>> GetTaskGroups()
    {
        var ownerId = currentUserService.UserId;
        var groups = await taskGroupRepository.GetTaskGroupsByOwner(ownerId);

        var response = groups.Select(g => new TaskGroupResponseDto
        {
            TaskGroupId = g.TaskGroupId,
            TaskGroupDescription = g.TaskGroupDescription,
            TaskGroupCreatedAtUtc = g.TaskGroupCreatedAtUtc,
            TaskGroupUpdatedAtUtc = g.TaskGroupUpdatedAtUtc,
            TaskGroupArchivedAtUtc = g.TaskGroupArchivedAtUtc,
            TaskGroupColor = g.TaskGroupColor,
            TaskGroupSortOrder = g.TaskGroupSortOrder
        });

        return response;
    }

    public async Task<IEnumerable<TaskGroupResponseDto>> PostTaskGroups(TaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = taskGroups.Select(dto => new TaskGroup
        {
            TaskGroupId = dto.TaskGroupId,
            TaskGroupDescription = dto.TaskGroupDescription,
            TaskGroupCreatedAtUtc = dto.TaskGroupCreatedAtUtc,
            TaskGroupUpdatedAtUtc = dto.TaskGroupUpdatedAtUtc,
            TaskGroupArchivedAtUtc = dto.TaskGroupArchivedAtUtc,
            TaskGroupColor = dto.TaskGroupColor,
            TaskGroupSortOrder = dto.TaskGroupSortOrder,
            OwnerUserId = ownerId
        }).ToArray();

        var created = await taskGroupRepository.PostTaskGroups(domain);

        var response = created.Select(g => new TaskGroupResponseDto
        {
            TaskGroupId = g.TaskGroupId,
            TaskGroupDescription = g.TaskGroupDescription,
            TaskGroupCreatedAtUtc = g.TaskGroupCreatedAtUtc,
            TaskGroupUpdatedAtUtc = g.TaskGroupUpdatedAtUtc,
            TaskGroupArchivedAtUtc = g.TaskGroupArchivedAtUtc,
            TaskGroupColor = g.TaskGroupColor,
            TaskGroupSortOrder = g.TaskGroupSortOrder
        });

        return response;
    }

    public async Task<IEnumerable<TaskGroupResponseDto>> PutTaskGroups(TaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = taskGroups.Select(dto => new TaskGroup
        {
            TaskGroupId = dto.TaskGroupId,
            TaskGroupDescription = dto.TaskGroupDescription,
            TaskGroupCreatedAtUtc = dto.TaskGroupCreatedAtUtc,
            TaskGroupUpdatedAtUtc = dto.TaskGroupUpdatedAtUtc,
            TaskGroupArchivedAtUtc = dto.TaskGroupArchivedAtUtc,
            TaskGroupColor = dto.TaskGroupColor,
            TaskGroupSortOrder = dto.TaskGroupSortOrder,
            OwnerUserId = ownerId
        }).ToArray();

        var updated = await taskGroupRepository.PutTaskGroups(domain);

        var response = updated.Select(g => new TaskGroupResponseDto
        {
            TaskGroupId = g.TaskGroupId,
            TaskGroupDescription = g.TaskGroupDescription,
            TaskGroupCreatedAtUtc = g.TaskGroupCreatedAtUtc,
            TaskGroupUpdatedAtUtc = g.TaskGroupUpdatedAtUtc,
            TaskGroupArchivedAtUtc = g.TaskGroupArchivedAtUtc,
            TaskGroupColor = g.TaskGroupColor,
            TaskGroupSortOrder = g.TaskGroupSortOrder
        });

        return response;
    }

    public async Task<int[]> DeleteTaskGroups(int[] taskGroupIds)
    {
        var deleted = await taskGroupRepository.DeleteTaskGroups(taskGroupIds);
        return deleted.ToArray();
    }
}
