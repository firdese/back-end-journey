using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;

[ApiController]
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
    public async Task<IActionResult> PostTaskGroups(TaskGroup[] taskGroups) {
        var created = await _taskGroupService.PostTaskGroups(taskGroups);
        return Ok(created);
    }

    [HttpPut]
    public async Task<IActionResult> PutTaskGroups(TaskGroup[] taskGroups) {
        var updated = await _taskGroupService.PutTaskGroups(taskGroups);
        return Ok(updated);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTaskGroups(int[] taskGroupIds) {
        var deletedIds = await _taskGroupService.DeleteTaskGroups(taskGroupIds);
        return Ok(deletedIds);
    }
}
