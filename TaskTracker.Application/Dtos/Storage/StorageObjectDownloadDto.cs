namespace TaskTracker.Application.Dtos.Storage;

public class StorageObjectDownloadDto : IDisposable
{
    public required Stream Content { get; init; }
    public required string ContentType { get; init; }
    public required string FileName { get; init; }
    public long SizeBytes { get; init; }

    public void Dispose()
    {
        Content.Dispose();
    }
}
