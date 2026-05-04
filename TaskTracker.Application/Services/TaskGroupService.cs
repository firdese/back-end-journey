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

    public async Task<IEnumerable<TaskGroupResponseDto>> PostTaskGroups(CreateTaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = mapper.Map<TaskGroup[]>(taskGroups);
        var now = DateTime.UtcNow;

        foreach (var g in domain)
        {
            g.TaskGroupCreatedAtUtc = now;
            g.TaskGroupUpdatedAtUtc = now;
            g.OwnerUserId = ownerId;
        }

        var created = await taskGroupRepository.PostTaskGroups(domain);
        var response = mapper.Map<IEnumerable<TaskGroupResponseDto>>(created);
        return response;
    }

    public async Task<IEnumerable<TaskGroupResponseDto>> PutTaskGroups(UpdateTaskGroupRequestDto[] taskGroups)
    {
        var ownerId = currentUserService.UserId;
        var domain = mapper.Map<TaskGroup[]>(taskGroups);
        var now = DateTime.UtcNow;

        foreach (var g in domain)
        {
            g.TaskGroupUpdatedAtUtc = now;
            g.OwnerUserId = ownerId;
        }

        var updated = await taskGroupRepository.PutTaskGroups(domain, ownerId);
        var response = mapper.Map<IEnumerable<TaskGroupResponseDto>>(updated);
        return response;
    }

    public async Task<int[]> DeleteTaskGroups(int[] taskGroupIds)
    {
        var ownerId = currentUserService.UserId;
        var deleted = await taskGroupRepository.DeleteTaskGroups(taskGroupIds, ownerId);
        return deleted.ToArray();
    }
}
