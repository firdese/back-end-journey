using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Database;

namespace Project1.Controllers
{
    [ApiController]
    [Route("taskgroup")]
    public class TaskGroupController : ControllerBase
    {
        private readonly WebAPIDbContext _dbContext;
        public TaskGroupController(WebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskGroups()
        {
            return Ok(await _dbContext.TaskGroups.Include(tg => tg.Tasks).ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> PostTaskGroups(Models.TaskGroup[] taskGroups)
        {
            await _dbContext.TaskGroups.AddRangeAsync(taskGroups);
            await _dbContext.SaveChangesAsync();

            return Ok(taskGroups);
        }

        [HttpPut]
        public async Task<IActionResult> PutTaskGroups(Models.TaskGroup[] taskGroups)
        {
            await _dbContext.TaskGroups.AddRangeAsync(taskGroups);
            foreach (var taskGroup in taskGroups)
            {
                _dbContext.Entry(taskGroup).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();

            return Ok(taskGroups);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaskGroups(Models.TaskGroup[] taskGroups)
        {
            await _dbContext.TaskGroups.AddRangeAsync(taskGroups);
            foreach (var taskGroup in taskGroups)
            {
                _dbContext.Entry(taskGroup).State = EntityState.Deleted;
            }
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
