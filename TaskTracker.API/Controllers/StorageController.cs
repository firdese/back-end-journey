using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Dtos.Storage;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.API.Controllers;

[ApiController]
[Authorize]
[Route("storage")]
public class StorageController(
    IObjectStorageService objectStorageService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [HttpPost]
    [RequestSizeLimit(25 * 1024 * 1024)]
    public async Task<ActionResult<StoredObjectDto>> Upload(
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

        var relativeObjectKey = $"{Guid.NewGuid():N}/{originalFileName}";
        var objectKey = BuildUserObjectKey(relativeObjectKey);

        await using var stream = file.OpenReadStream();
        var stored = await objectStorageService.UploadAsync(
            objectKey,
            originalFileName,
            string.IsNullOrWhiteSpace(file.ContentType) ? "application/octet-stream" : file.ContentType,
            stream,
            cancellationToken);

        return Ok(new StoredObjectDto
        {
            ObjectKey = relativeObjectKey,
            FileName = stored.FileName,
            ContentType = stored.ContentType,
            SizeBytes = stored.SizeBytes
        });
    }

    [HttpGet("{*objectKey}")]
    public async Task<IActionResult> Download(
        string objectKey,
        CancellationToken cancellationToken)
    {
        if (!TryBuildUserObjectKey(objectKey, out var scopedObjectKey))
        {
            return BadRequest("Invalid object key.");
        }

        var download = await objectStorageService.DownloadAsync(
            scopedObjectKey,
            cancellationToken);

        if (download is null)
        {
            return NotFound();
        }

        return File(download.Content, download.ContentType, download.FileName);
    }

    [HttpDelete("{*objectKey}")]
    public async Task<IActionResult> Delete(
        string objectKey,
        CancellationToken cancellationToken)
    {
        if (!TryBuildUserObjectKey(objectKey, out var scopedObjectKey))
        {
            return BadRequest("Invalid object key.");
        }

        await objectStorageService.DeleteAsync(
            scopedObjectKey,
            cancellationToken);

        return NoContent();
    }

    private string BuildUserObjectKey(string relativeObjectKey)
    {
        if (TryBuildUserObjectKey(relativeObjectKey, out var objectKey))
        {
            return objectKey;
        }

        throw new ArgumentException("Invalid object key.", nameof(relativeObjectKey));
    }

    private bool TryBuildUserObjectKey(string relativeObjectKey, out string objectKey)
    {
        var normalized = relativeObjectKey.Replace('\\', '/').Trim('/');

        if (string.IsNullOrWhiteSpace(normalized) ||
            normalized.Split('/').Any(segment => segment is "." or ".."))
        {
            objectKey = string.Empty;
            return false;
        }

        var userPrefix = Uri.EscapeDataString(currentUserService.UserId);
        objectKey = $"{userPrefix}/{normalized}";
        return true;
    }
}
