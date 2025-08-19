using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelTask = TaskTracker.Domain.Models.Task; 
    
namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskRepository
{
    public Task<IEnumerable<ModelTask>> GetTasks();

    public Task CreateTasks(ModelTask[] tasks);

    public Task PutTasks(ModelTask[] tasks);

    public Task DeleteTasks(ModelTask[] tasksToDelete);
}