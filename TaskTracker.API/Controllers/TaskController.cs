using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Interfaces.Services;

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
        public async Task<IActionResult> PostTasks([FromBody] CreateTaskRequestDto[] tasks) {
            var createdTasks = await _taskService.CreateTasks(tasks);
            return Ok(createdTasks);
        }

        [HttpPut]
        public async Task<IActionResult> PutTasks([FromBody] UpdateTaskRequestDto[] tasks) {
            var updatedTasks = await _taskService.PutTasks(tasks);
            return Ok(updatedTasks);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTasks([FromBody] int[] taskIds) {
            var deletedIds = await _taskService.DeleteTasks(taskIds);
            return Ok(deletedIds);
        }
    }
}
