using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Dtos.TaskGroup;
using TaskTracker.Application.Interfaces.Services;

[ApiController]
[Authorize]
[Route("taskgroup")]
public class TaskGroupController : ControllerBase {
    private readonly ITaskGroupService _taskGroupService;

    public TaskGroupController(ITaskGroupService taskGroupService) {
        _taskGroupService = taskGroupService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskGroups() {
        var result = await _taskGroupService.GetTaskGroups();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostTaskGroups([FromBody] CreateTaskGroupRequestDto[] taskGroups) {
        var created = await _taskGroupService.PostTaskGroups(taskGroups);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> PutTaskGroups([FromBody] UpdateTaskGroupRequestDto[] taskGroups) {
        var updated = await _taskGroupService.PutTaskGroups(taskGroups);
        return Ok(updated);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTaskGroups([FromBody] int[] taskGroupIds) {
        var deletedIds = await _taskGroupService.DeleteTaskGroups(taskGroupIds);
        return Ok(deletedIds);
    }
}
