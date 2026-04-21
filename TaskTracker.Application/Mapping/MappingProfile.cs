using AutoMapper;
using TaskTracker.Application.Dtos;
using TaskTracker.Application.Dtos.Task;
using TaskTracker.Application.Dtos.TaskGroup;
using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Task mappings
        CreateMap<Domain.Models.Task, TaskResponseDto>();
        CreateMap<TaskRequestDto, Domain.Models.Task>()
            .ForMember(dest => dest.TaskId, opt => opt.Condition(src => src.TaskId != 0));
        CreateMap<Domain.Models.Task, TaskRequestDto>();

        // TaskGroup mappings
        CreateMap<TaskGroup, TaskGroupResponseDto>();
        CreateMap<TaskGroupRequestDto, TaskGroup>()
            .ForMember(dest => dest.TaskGroupId, opt => opt.Condition(src => src.TaskGroupId != 0));
        CreateMap<TaskGroup, TaskGroupRequestDto>();
    }
}
