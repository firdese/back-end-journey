#nullable enable

using TaskTracker.Application.Dtos.Storage;

namespace TaskTracker.Application.Interfaces.Services;

public interface IObjectStorageService
{
    Task<StoredObjectDto> UploadAsync(
        string objectKey,
        string fileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default);

    Task<StorageObjectDownloadDto?> DownloadAsync(
        string objectKey,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string objectKey,
        CancellationToken cancellationToken = default);
}
