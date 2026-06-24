using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Exceptions;
using TaskTracker.Application.Interfaces.Repositories;
using TaskTracker.Domain.Models;

namespace TaskTracker.Infrastructure.Persistence.Repositories;

public class TaskAttachmentRepository(WebAPIDbContext context) : ITaskAttachmentRepository
{
    public async Task<IEnumerable<TaskAttachment>> GetAttachmentsByTaskId(
        int taskId,
        string ownerId)
    {
        await EnsureTaskIsOwned(taskId, ownerId);

        return await context.TaskAttachments
            .Where(attachment => attachment.TaskId == taskId)
            .OrderByDescending(attachment => attachment.CreatedAtUtc)
            .ToArrayAsync();
    }

    public async Task<TaskAttachment> CreateAttachment(
        TaskAttachment attachment,
        string ownerId)
    {
        await EnsureTaskIsOwned(attachment.TaskId, ownerId);

        context.TaskAttachments.Add(attachment);
        await context.SaveChangesAsync();

        return attachment;
    }

    public async Task<TaskAttachment> DeleteAttachment(
        int taskId,
        int attachmentId,
        string ownerId)
    {
        await EnsureTaskIsOwned(taskId, ownerId);

        var attachment = await context.TaskAttachments
            .SingleOrDefaultAsync(item =>
                item.TaskId == taskId &&
                item.TaskAttachmentId == attachmentId);

        if (attachment is null)
        {
            throw new ForbiddenResourceAccessException("Attachment is not accessible.");
        }

        context.TaskAttachments.Remove(attachment);
        await context.SaveChangesAsync();

        return attachment;
    }

    private async System.Threading.Tasks.Task EnsureTaskIsOwned(int taskId, string ownerId)
    {
        var taskIsOwned = await context.Tasks
            .AnyAsync(task =>
                task.TaskId == taskId &&
                task.TaskGroup != null &&
                task.TaskGroup.OwnerUserId == ownerId);

        if (!taskIsOwned)
        {
            throw new ForbiddenResourceAccessException("Task is not accessible.");
        }
    }
}
