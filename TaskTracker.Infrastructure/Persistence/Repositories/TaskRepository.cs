using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskRepository(WebAPIDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<Domain.Models.Task>> GetTasksByTaskGroup(int taskGroupId)
    {
        return await context.Tasks
            .Where(t => t.TaskGroupId == taskGroupId)
            .ToArrayAsync();
    }

    public async Task<IEnumerable<Domain.Models.Task>> CreateTasks(Domain.Models.Task[] tasks) {
        foreach (var t in tasks) {
            context.Tasks.Add(t);
        }
        await context.SaveChangesAsync();

        return tasks;
    }

    public async Task<IEnumerable<Domain.Models.Task>> PutTasks(Domain.Models.Task[] tasks) {
        foreach (var t in tasks) {
            context.Tasks.Update(t);
        }
        await context.SaveChangesAsync();

        return tasks;
    }

    public async Task<IEnumerable<int>> DeleteTasks(int[] taskIds) {
        var toDelete = context.Tasks
            .Where(t => taskIds.Contains(t.TaskId))
            .ToArray();

        context.Tasks.RemoveRange(toDelete);
        await context.SaveChangesAsync();

        return toDelete.Select(t => t.TaskId).ToArray();
    }

}
