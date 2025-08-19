using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Application.Services;
using TaskTracker.Domain.Models;

namespace TaskTracker.API.Controllers
{
    [ApiController]
    [Route("taskgroup")]
    public class TaskGroupController(ITaskGroupService taskGroupService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTaskGroups()
        {
            return Ok(await taskGroupService.GetTaskGroups());
        }

        [HttpPost]
        public async Task<IActionResult> PostTaskGroups(TaskGroup[] taskGroups)
        {
            await taskGroupService.PostTaskGroups(taskGroups);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> PutTaskGroups(TaskGroup[] taskGroups)
        {
            await  taskGroupService.PutTaskGroups(taskGroups);

            return Ok(taskGroups);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTaskGroups(TaskGroup[] taskGroups)
        {
            await taskGroupService.DeleteTaskGroups(taskGroups);

            return Ok();
        }
    }
}
