using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Services;

public class TaskService(
    ITaskRepository taskRepository,
    IMapper mapper) : ITaskService
{
    public async Task<IEnumerable<TaskResponseDto>> GetTasksByTaskGroup(int taskGroupId)
    {
        var tasks = await taskRepository.GetTasksByTaskGroup(taskGroupId);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        return response;
    }

    public async Task<IEnumerable<TaskResponseDto>> CreateTasks(TaskRequestDto[] tasks)
    {
        var domain = mapper.Map<Domain.Models.Task[]>(tasks);

        foreach (var t in domain)
        {
            t.TaskCreatedAtUtc = DateTime.UtcNow;
        }

        var createdTasks = await taskRepository.CreateTasks(domain);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(createdTasks);
        return response;
    }

    public async Task<IEnumerable<TaskResponseDto>> PutTasks(TaskRequestDto[] tasks)
    {
        var domain = mapper.Map<Domain.Models.Task[]>(tasks);

        foreach (var t in domain)
        {
            t.TaskUpdatedAtUtc = DateTime.UtcNow;
        }

        var updatedTasks = await taskRepository.PutTasks(domain);
        var response = mapper.Map<IEnumerable<TaskResponseDto>>(updatedTasks);
        return response;
    }

    public async Task<IEnumerable<int>> DeleteTasks(int[] tasksToDelete)
    {
        return await taskRepository.DeleteTasks(tasksToDelete);
    }
}
