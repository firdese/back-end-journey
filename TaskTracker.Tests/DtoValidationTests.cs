using System.ComponentModel.DataAnnotations;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Dtos.TaskGroup;
using Xunit;

namespace TaskTracker.Tests;

public class DtoValidationTests
{
    [Fact]
    public void CreateTaskRequest_requires_a_valid_task_group()
    {
        var request = new CreateTaskRequestDto
        {
            TaskDescription = "Invalid group",
            TaskGroupId = 0
        };

        var results = Validate(request);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(CreateTaskRequestDto.TaskGroupId)));
    }

    [Fact]
    public void UpdateTaskRequest_requires_an_id()
    {
        var request = new UpdateTaskRequestDto
        {
            TaskDescription = "Missing id",
            TaskGroupId = 1
        };

        var results = Validate(request);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(UpdateTaskRequestDto.TaskId)));
    }

    [Fact]
    public void TaskGroupColor_must_be_hex_when_provided()
    {
        var request = new CreateTaskGroupRequestDto
        {
            TaskGroupDescription = "Inbox",
            TaskGroupColor = "blue"
        };

        var results = Validate(request);

        Assert.Contains(results, result => result.MemberNames.Contains(nameof(CreateTaskGroupRequestDto.TaskGroupColor)));
    }

    private static List<ValidationResult> Validate(object value)
    {
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(value, new ValidationContext(value), results, validateAllProperties: true);
        return results;
    }
}
