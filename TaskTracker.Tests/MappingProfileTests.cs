using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Dtos.TaskGroup;
using TaskTracker.Application.Mapping;
using TaskTracker.Domain.Models;
using Xunit;
using DomainTask = TaskTracker.Domain.Models.Task;

namespace TaskTracker.Tests;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>(), NullLoggerFactory.Instance);
        config.AssertConfigurationIsValid();
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateTaskRequest_maps_only_client_owned_fields()
    {
        var request = new CreateTaskRequestDto
        {
            TaskDescription = "Write DTO tests",
            TaskGroupId = 42,
            TaskProgress = 50,
            TaskPriority = 2,
            TaskSortOrder = 3
        };

        var task = _mapper.Map<DomainTask>(request);

        Assert.Equal("Write DTO tests", task.TaskDescription);
        Assert.Equal(42, task.TaskGroupId);
        Assert.Equal(50, task.TaskProgress);
        Assert.Equal(2, task.TaskPriority);
        Assert.Equal(3, task.TaskSortOrder);
        Assert.Equal(0, task.TaskId);
        Assert.Equal(default, task.TaskCreatedAtUtc);
        Assert.Equal(default, task.TaskUpdatedAtUtc);
    }

    [Fact]
    public void Task_maps_to_response_dto_without_navigation_properties()
    {
        var createdAt = new DateTime(2026, 5, 4, 1, 2, 3, DateTimeKind.Utc);
        var updatedAt = createdAt.AddMinutes(10);
        var task = new DomainTask
        {
            TaskId = 7,
            TaskDescription = "Ship DTO boundary",
            TaskGroupId = 9,
            TaskPriority = 1,
            TaskSortOrder = 2,
            TaskCreatedAtUtc = createdAt,
            TaskUpdatedAtUtc = updatedAt
        };

        var response = _mapper.Map<TaskResponseDto>(task);

        Assert.Equal(7, response.TaskId);
        Assert.Equal("Ship DTO boundary", response.TaskDescription);
        Assert.Equal(9, response.TaskGroupId);
        Assert.Equal(1, response.TaskPriority);
        Assert.Equal(2, response.TaskSortOrder);
        Assert.Equal(createdAt, response.TaskCreatedAtUtc);
        Assert.Equal(updatedAt, response.TaskUpdatedAtUtc);
    }

    [Fact]
    public void CreateTaskGroupRequest_does_not_map_owner_or_timestamps()
    {
        var request = new CreateTaskGroupRequestDto
        {
            TaskGroupDescription = "Today",
            TaskGroupColor = "#336699",
            TaskGroupSortOrder = 1
        };

        var group = _mapper.Map<TaskGroup>(request);

        Assert.Equal("Today", group.TaskGroupDescription);
        Assert.Equal("#336699", group.TaskGroupColor);
        Assert.Equal(1, group.TaskGroupSortOrder);
        Assert.Equal(0, group.TaskGroupId);
        Assert.Null(group.OwnerUserId);
        Assert.Equal(default, group.TaskGroupCreatedAtUtc);
        Assert.Equal(default, group.TaskGroupUpdatedAtUtc);
    }
}
