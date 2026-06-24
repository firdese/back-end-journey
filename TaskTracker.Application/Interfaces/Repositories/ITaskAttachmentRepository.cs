using TaskTracker.Domain.Models;

namespace TaskTracker.Application.Interfaces.Repositories;

public interface ITaskAttachmentRepository
{
    Task<IEnumerable<TaskAttachment>> GetAttachmentsByTaskId(int taskId, string ownerId);

    Task<TaskAttachment> CreateAttachment(TaskAttachment attachment, string ownerId);

    Task<TaskAttachment> DeleteAttachment(int taskId, int attachmentId, string ownerId);
}
