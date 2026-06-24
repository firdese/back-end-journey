using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Dtos.TaskAttachment;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("tasks/{taskId:int}/attachments")]
public class TaskAttachmentController(
    IObjectStorageService objectStorageService,
    ITaskAttachmentService taskAttachmentService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskAttachmentResponseDto>>> GetAttachments(
        int taskId)
    {
        var attachments = await taskAttachmentService.GetAttachmentsByTaskId(taskId);
        return Ok(attachments);
    }

    [HttpPost]
    [RequestSizeLimit(25 * 1024 * 1024)]
    public async Task<ActionResult<TaskAttachmentResponseDto>> UploadAttachment(
        int taskId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest("File must not be empty.");
        }

        var originalFileName = Path.GetFileName(file.FileName);
        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            return BadRequest("File name must not be empty.");
        }

        var relativeObjectKey = $"tasks/{taskId}/{Guid.NewGuid():N}/{originalFileName}";
        var scopedObjectKey = BuildUserObjectKey(relativeObjectKey);
        var contentType = string.IsNullOrWhiteSpace(file.ContentType)
            ? "application/octet-stream"
            : file.ContentType;

        await using var stream = file.OpenReadStream();
        var storedObject = await objectStorageService.UploadAsync(
            scopedObjectKey,
            originalFileName,
            contentType,
            stream,
            cancellationToken);

        TaskAttachmentResponseDto attachment;
        try
        {
            attachment = await taskAttachmentService.CreateAttachment(
                taskId,
                storedObject.FileName,
                storedObject.ContentType,
                storedObject.SizeBytes,
                relativeObjectKey);
        }
        catch
        {
            await objectStorageService.DeleteAsync(scopedObjectKey, cancellationToken);
            throw;
        }

        return Ok(attachment);
    }

    [HttpDelete("{attachmentId:int}")]
    public async Task<IActionResult> DeleteAttachment(
        int taskId,
        int attachmentId,
        CancellationToken cancellationToken)
    {
        var objectKey = await taskAttachmentService.DeleteAttachment(taskId, attachmentId);
        await objectStorageService.DeleteAsync(
            BuildUserObjectKey(objectKey),
            cancellationToken);

        return NoContent();
    }

    private string BuildUserObjectKey(string relativeObjectKey)
    {
        var normalized = relativeObjectKey.Replace('\\', '/').Trim('/');

        if (string.IsNullOrWhiteSpace(normalized) ||
            normalized.Split('/').Any(segment => segment is "." or ".."))
        {
            throw new ArgumentException("Invalid object key.", nameof(relativeObjectKey));
        }

        var userPrefix = Uri.EscapeDataString(currentUserService.UserId);
        return $"{userPrefix}/{normalized}";
    }
}
