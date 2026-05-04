using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Interfaces.Repositories;
namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskRepository(WebAPIDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<Domain.Models.Task>> GetTasksByTaskGroup(int taskGroupId, string ownerId)
    {
        return await context.Tasks
            .Where(t => t.TaskGroupId == taskGroupId && t.TaskGroup != null && t.TaskGroup.OwnerUserId == ownerId)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<Domain.Models.Task>> CreateTasks(Domain.Models.Task[] tasks, string ownerId) {
        var taskGroupIds = tasks.Select(t => t.TaskGroupId).Distinct().ToArray();
        var ownedTaskGroupIds = await context.TaskGroups
            .Where(tg => taskGroupIds.Contains(tg.TaskGroupId) && tg.OwnerUserId == ownerId)
            .Select(tg => tg.TaskGroupId)
            .ToArrayAsync();

        if (ownedTaskGroupIds.Length != taskGroupIds.Length) {
            throw new ForbiddenResourceAccessException("One or more task groups are not accessible.");
        }

        foreach (var t in tasks) {
            context.Tasks.Add(t);
        }
        await context.SaveChangesAsync();

        return tasks;
    }

    public async Task<IEnumerable<Domain.Models.Task>> PutTasks(Domain.Models.Task[] tasks, string ownerId) {
        var taskIds = tasks.Select(t => t.TaskId).Distinct().ToArray();
        var taskGroupIds = tasks.Select(t => t.TaskGroupId).Distinct().ToArray();

        var ownedTaskGroupIds = await context.TaskGroups
            .Where(tg => taskGroupIds.Contains(tg.TaskGroupId) && tg.OwnerUserId == ownerId)
            .Select(tg => tg.TaskGroupId)
            .ToArrayAsync();

        if (ownedTaskGroupIds.Length != taskGroupIds.Length) {
            throw new ForbiddenResourceAccessException("One or more task groups are not accessible.");
        }

        var existingTasks = await context.Tasks
            .Where(t => taskIds.Contains(t.TaskId) && t.TaskGroup != null && t.TaskGroup.OwnerUserId == ownerId)
            .ToArrayAsync();

        if (existingTasks.Length != taskIds.Length) {
            throw new ForbiddenResourceAccessException("One or more tasks are not accessible.");
        }

        foreach (var t in tasks) {
            var existing = existingTasks.Single(x => x.TaskId == t.TaskId);
            existing.TaskDescription = t.TaskDescription;
            existing.TaskCompletedAtUtc = t.TaskCompletedAtUtc;
            existing.TaskStartAtUtc = t.TaskStartAtUtc;
            existing.TaskEndAtUtc = t.TaskEndAtUtc;
            existing.TaskProgress = t.TaskProgress;
            existing.TaskDueAtUtc = t.TaskDueAtUtc;
            existing.TaskDeletedAtUtc = t.TaskDeletedAtUtc;
            existing.TaskSortOrder = t.TaskSortOrder;
            existing.TaskPriority = t.TaskPriority;
            existing.TaskGroupId = t.TaskGroupId;
            existing.TaskUpdatedAtUtc = t.TaskUpdatedAtUtc;
        }
        await context.SaveChangesAsync();

        return existingTasks;
    }

    public async Task<IEnumerable<int>> DeleteTasks(int[] taskIds, string ownerId) {
        var toDelete = await context.Tasks
            .Where(t => taskIds.Contains(t.TaskId) && t.TaskGroup != null && t.TaskGroup.OwnerUserId == ownerId)
            .ToArrayAsync();

        if (toDelete.Length != taskIds.Distinct().Count()) {
            throw new ForbiddenResourceAccessException("One or more tasks are not accessible.");
        }

        context.Tasks.RemoveRange(toDelete);
        await context.SaveChangesAsync();

        return toDelete.Select(t => t.TaskId).ToArray();
    }

}
