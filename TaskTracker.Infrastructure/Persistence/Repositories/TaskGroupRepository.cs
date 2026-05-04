using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskGroupRepository(WebAPIDbContext context) : ITaskGroupRepository
{
    public async Task<IEnumerable<TaskGroup>> GetTaskGroupsByOwner(string ownerId)
    {
        return await context.TaskGroups.Include(tg => tg.Tasks).Where(x => x.OwnerUserId == ownerId).ToListAsync();
    }

    public async Task<IEnumerable<TaskGroup>> PostTaskGroups(TaskGroup[] taskGroups) {
        foreach (var tg in taskGroups) {
            context.TaskGroups.Add(tg);
        }
        await context.SaveChangesAsync();

        return taskGroups;
    }

    public async Task<IEnumerable<TaskGroup>> PutTaskGroups(TaskGroup[] taskGroups, string ownerId) {
        var taskGroupIds = taskGroups.Select(tg => tg.TaskGroupId).Distinct().ToArray();
        var existingTaskGroups = await context.TaskGroups
            .Where(tg => taskGroupIds.Contains(tg.TaskGroupId) && tg.OwnerUserId == ownerId)
            .ToArrayAsync();

        if (existingTaskGroups.Length != taskGroupIds.Length) {
            throw new ForbiddenResourceAccessException("One or more task groups are not accessible.");
        }

        foreach (var tg in taskGroups) {
            var existing = existingTaskGroups.Single(x => x.TaskGroupId == tg.TaskGroupId);
            existing.TaskGroupDescription = tg.TaskGroupDescription;
            existing.TaskGroupColor = tg.TaskGroupColor;
            existing.TaskGroupSortOrder = tg.TaskGroupSortOrder;
            existing.TaskGroupUpdatedAtUtc = tg.TaskGroupUpdatedAtUtc;
        }
        await context.SaveChangesAsync();

        return existingTaskGroups;
    }

    public async Task<IEnumerable<int>> DeleteTaskGroups(int[] taskGroupIds, string ownerId) {
        var toDelete = await context.TaskGroups
            .Where(tg => taskGroupIds.Contains(tg.TaskGroupId) && tg.OwnerUserId == ownerId)
            .ToArrayAsync();

        if (toDelete.Length != taskGroupIds.Distinct().Count()) {
            throw new ForbiddenResourceAccessException("One or more task groups are not accessible.");
        }

        context.TaskGroups.RemoveRange(toDelete);
        await context.SaveChangesAsync();

        return toDelete.Select(t => t.TaskGroupId).ToArray();
    }
}
