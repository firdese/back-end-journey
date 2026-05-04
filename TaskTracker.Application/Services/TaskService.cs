using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.Application.Services;

public class TaskService(
    ITaskRepository taskRepository,
    ICurrentUserService currentUserService,
    IMapper mapper) : ITaskService
{
    public async Task<IEnumerable<TaskResponseDto>> GetTasksByTaskGroup(int taskGroupId)
    {
        var tasks = await taskRepository.GetTasksByTaskGroup(taskGroupId, currentUserService.UserId);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        return response;
    }

    public async Task<IEnumerable<TaskResponseDto>> CreateTasks(CreateTaskRequestDto[] tasks)
    {
        var domain = mapper.Map<Domain.Models.Task[]>(tasks);
        var now = DateTime.UtcNow;

        foreach (var t in domain)
        {
            t.TaskCreatedAtUtc = now;
            t.TaskUpdatedAtUtc = now;
        }

        var createdTasks = await taskRepository.CreateTasks(domain, currentUserService.UserId);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(createdTasks);
        return response;
    }

    public async Task<IEnumerable<TaskResponseDto>> PutTasks(UpdateTaskRequestDto[] tasks)
    {
        var domain = mapper.Map<Domain.Models.Task[]>(tasks);
        var now = DateTime.UtcNow;

        foreach (var t in domain)
        {
            t.TaskUpdatedAtUtc = now;
        }

        var updatedTasks = await taskRepository.PutTasks(domain, currentUserService.UserId);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(updatedTasks);
        return response;
    }

    public async Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete)
    {
        return await taskRepository.DeleteTasks(tasksToDelete, currentUserService.UserId);
    }
}
