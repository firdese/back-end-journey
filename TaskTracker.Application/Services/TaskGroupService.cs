using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskTracker.Application.Dtos.TaskGroup;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Services;

public class TaskGroupService(
    ITaskGroupRepository taskGroupRepository,
    ICurrentUserService currentUserService,
    IMapper mapper) : ITaskGroupService
{
    public async Task<IEnumerable<TaskGroupResponseDto>> GetTaskGroups()
    {
        var ownerId = currentUserService.UserId;
        var groups = await taskGroupRepository.GetTaskGroupsByOwner(ownerId);
        var response = mapper.Map<IEnumerable<TaskGroupResponseDto>>(groups);
        return response;
    }

    public async Task<IEnumerable<TaskGroupResponseDto>> PostTaskGroups(TaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = mapper.Map<TaskGroup[]>(taskGroups);

        // set created timestamp and owner explicitly
        foreach (var g in domain)
        {
            g.TaskGroupCreatedAtUtc = DateTime.UtcNow;
            g.OwnerUserId = ownerId;
        }

        var created = await taskGroupRepository.PostTaskGroups(domain);
        var response = mapper.Map<IEnumerable<TaskGroupResponseDto>>(created);
        return response;
    }

    public async Task<IEnumerable<TaskGroupResponseDto>> PutTaskGroups(TaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = mapper.Map<TaskGroup[]>(taskGroups);

        // set updated timestamp and owner explicitly
        foreach (var g in domain)
        {
            g.TaskGroupUpdatedAtUtc = DateTime.UtcNow;
            g.OwnerUserId = ownerId;
        }

        var updated = await taskGroupRepository.PutTaskGroups(domain);
        var response = mapper.Map<IEnumerable<TaskGroupResponseDto>>(updated);
        return response;
    }

    public async Task<int[]> DeleteTaskGroups(int[] taskGroupIds)
    {
        // TODO soft delete via archived at utc
        var deleted = await taskGroupRepository.DeleteTaskGroups(taskGroupIds);
        return deleted.ToArray();
    }
}
