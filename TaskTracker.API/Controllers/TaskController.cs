using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;
using Task = TaskTracker.Domain.Models.Task;

namespace TaskTracker.API.Controllers {
    [ApiController]
    [Authorize]
    [Route("tasks")]
    public class TaskController : ControllerBase {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService) {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int taskGroupId) {
            var tasks = await _taskService.GetTasksByTaskGroup(taskGroupId);
            return Ok(tasks);
        } 

        [HttpPost]
        public async Task<IActionResult> PostTasks(Task[] tasks) {
            var createdTasks = await _taskService.CreateTasks(tasks);
            return Ok(createdTasks);
        }

        [HttpPut]
        public async Task<IActionResult> PutTasks(Task[] tasks) {
            var updatedTasks = await _taskService.PutTasks(tasks);
            return Ok(updatedTasks);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTasks(int[] taskIds) {
            var deletedIds = await _taskService.DeleteTasks(taskIds);
            return Ok(deletedIds);
        }
    }
}
