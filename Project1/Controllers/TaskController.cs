using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Database;

namespace Project1.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TaskController : ControllerBase
    {
        private readonly WebAPIDbContext _dbContext;

        public TaskController(WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await _dbContext.Tasks.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostTask(Models.Task[] tasks)
        {
            await _dbContext.Tasks.AddRangeAsync(tasks);
            await _dbContext.SaveChangesAsync();
            return Ok(tasks);
        }

        [HttpPut]
        public async Task<IActionResult> PutTasks(Models.Task[] tasks)
        {
            _dbContext.Tasks.AttachRange(tasks);
            foreach(var task in tasks)
            {
                _dbContext.Entry(task).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();

            return Ok(tasks);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTasks(Models.Task[] tasksToDelete)
        {
            _dbContext.Tasks.AddRange(tasksToDelete);
            foreach (var taskToDelete in tasksToDelete)
            {
                _dbContext.Entry(taskToDelete).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();

            return Ok(tasksToDelete);
        }
    }
}
