using AutoMapper;
using TaskTracker.Application.Dtos.TaskAttachment;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Application.Interfaces.Services;
using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Services;

public class TaskAttachmentService(
    ITaskAttachmentRepository taskAttachmentRepository,
    ICurrentUserService currentUserService,
    IMapper mapper) : ITaskAttachmentService
{
    public async Task<IEnumerable<TaskAttachmentResponseDto>> GetAttachmentsByTaskId(int taskId)
    {
        var attachments = await taskAttachmentRepository.GetAttachmentsByTaskId(
            taskId,
            currentUserService.UserId);

        return mapper.Map<IEnumerable<TaskAttachmentResponseDto>>(attachments);
    }

    public async Task<TaskAttachmentResponseDto> CreateAttachment(
        int taskId,
        string fileName,
        string contentType,
        long sizeBytes,
        string objectKey)
    {
        var attachment = new TaskAttachment
        {
            TaskId = taskId,
            ObjectKey = objectKey,
            FileName = fileName,
            ContentType = contentType,
            SizeBytes = sizeBytes,
            CreatedAtUtc = DateTime.UtcNow
        };

        var created = await taskAttachmentRepository.CreateAttachment(
            attachment,
            currentUserService.UserId);

        return mapper.Map<TaskAttachmentResponseDto>(created);
    }

    public async Task<string> DeleteAttachment(int taskId, int attachmentId)
    {
        var deleted = await taskAttachmentRepository.DeleteAttachment(
            taskId,
            attachmentId,
            currentUserService.UserId);

        return deleted.ObjectKey;
    }
}
