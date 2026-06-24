using TaskTracker.Application.Dtos.TaskAttachment;

namespace TaskTracker.Application.Interfaces.Services;

public interface ITaskAttachmentService
{
    Task<IEnumerable<TaskAttachmentResponseDto>> GetAttachmentsByTaskId(int taskId);

    Task<TaskAttachmentResponseDto> CreateAttachment(
        int taskId,
        string fileName,
        string contentType,
        long sizeBytes,
        string objectKey);

    Task<string> DeleteAttachment(int taskId, int attachmentId);
}
