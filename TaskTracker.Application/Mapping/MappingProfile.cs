using AutoMapper;
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
        CreateMap<CreateTaskRequestDto, Domain.Models.Task>()
            .ForMember(dest => dest.TaskId, opt => opt.Ignore())
            .ForMember(dest => dest.TaskCreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskUpdatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskDeletedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.Dependencies, opt => opt.Ignore())
            .ForMember(dest => dest.DependentOnMe, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroup, opt => opt.Ignore());
        CreateMap<UpdateTaskRequestDto, Domain.Models.Task>()
            .ForMember(dest => dest.TaskCreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskUpdatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.Dependencies, opt => opt.Ignore())
            .ForMember(dest => dest.DependentOnMe, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroup, opt => opt.Ignore());

        // TaskGroup mappings
        CreateMap<TaskGroup, TaskGroupResponseDto>();
        CreateMap<CreateTaskGroupRequestDto, TaskGroup>()
            .ForMember(dest => dest.TaskGroupId, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroupCreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroupUpdatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroupArchivedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());
        CreateMap<UpdateTaskGroupRequestDto, TaskGroup>()
            .ForMember(dest => dest.TaskGroupCreatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroupUpdatedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.TaskGroupArchivedAtUtc, opt => opt.Ignore())
            .ForMember(dest => dest.OwnerUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());
    }
}
