using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("tasks")]
    public class TaskController(ITaskService taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await taskService.GetTasks());
        }

        [HttpPost]
        public async Task<IActionResult> PostTask(Domain.Models.Task[] tasks)
        {
            await taskService.CreateTasks(tasks);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutTasks(Domain.Models.Task[] tasks)
        {
            await taskService.PutTasks(tasks);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTasks(Domain.Models.Task[] tasksToDelete)
        {
            await taskService.DeleteTasks(tasksToDelete);

            return Ok();
        }
    }
}
