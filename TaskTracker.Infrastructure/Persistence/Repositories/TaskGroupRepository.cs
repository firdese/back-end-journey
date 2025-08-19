using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskGroupRepository(WebAPIDbContext context) : ITaskGroupRepository
{
    public async Task<IEnumerable<TaskGroup>> GetTaskGroups()
    {
        return await context.TaskGroups.Include(tg => tg.Tasks).ToListAsync();
    }

    public async Task PostTaskGroups(TaskGroup[] taskGroups)
    {
        await context.TaskGroups.AddRangeAsync(taskGroups);
        await context.SaveChangesAsync();
    }

    public async Task PutTaskGroups(TaskGroup[] taskGroups)
    {
        await context.TaskGroups.AddRangeAsync(taskGroups);
        foreach (var taskGroup in taskGroups)
        {
            context.Entry(taskGroup).State = EntityState.Modified;
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteTaskGroups(TaskGroup[] taskGroups)
    {
        await context.TaskGroups.AddRangeAsync(taskGroups);
        foreach (var taskGroup in taskGroups)
        {
            context.Entry(taskGroup).State = EntityState.Deleted;
        }
        await context.SaveChangesAsync();
    }
}