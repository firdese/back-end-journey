namespace TaskTracker.Application.Dtos.Storage;

public class StoredObjectDto
{
    public required string ObjectKey { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public long SizeBytes { get; init; }
}
