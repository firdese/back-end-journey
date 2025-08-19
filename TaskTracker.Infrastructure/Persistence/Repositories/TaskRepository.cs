using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Repositories;
namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskRepository(WebAPIDbContext context) : ITaskRepository
{
    public async Task<IEnumerable<Domain.Models.Task>> GetTasks()
    {
        return await context.Tasks.ToListAsync();
    }

    public async Task CreateTasks(Domain.Models.Task[] tasks)
    {
        await context.Tasks.AddRangeAsync(tasks);
        await context.SaveChangesAsync();
    }

    public async Task PutTasks(Domain.Models.Task[] tasks)
    {
        context.Tasks.AttachRange(tasks);
        foreach(var task in tasks)
        {
            context.Entry(task).State = EntityState.Modified;
        }
        await context.SaveChangesAsync();
    }

    public async Task DeleteTasks(Domain.Models.Task[] tasksToDelete)
    {
        context.Tasks.AddRange(tasksToDelete);
        foreach (var taskToDelete in tasksToDelete)
        {
            context.Entry(taskToDelete).State = EntityState.Deleted;
        }
        await context.SaveChangesAsync();
    }
}