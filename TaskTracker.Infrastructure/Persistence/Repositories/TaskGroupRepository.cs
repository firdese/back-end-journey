using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<TaskGroup>> PutTaskGroups(TaskGroup[] taskGroups) {
        foreach (var tg in taskGroups) {
            context.TaskGroups.Update(tg);
        }
        await context.SaveChangesAsync();

        return taskGroups;
    }

    public async Task<IEnumerable<int>> DeleteTaskGroups(int[] taskGroupIds) {
        var toDelete = context.TaskGroups
            .Where(tg => taskGroupIds.Contains(tg.TaskGroupId))
            .ToArray();

        context.TaskGroups.RemoveRange(toDelete);
        await context.SaveChangesAsync();

        return toDelete.Select(t => t.TaskGroupId).ToArray();
    }
}
