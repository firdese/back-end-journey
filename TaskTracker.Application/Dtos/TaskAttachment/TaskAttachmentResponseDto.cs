namespace TaskTracker.Application.Dtos.TaskAttachment;

public class TaskAttachmentResponseDto
{
    public int TaskAttachmentId { get; init; }
    public int TaskId { get; init; }
    public required string ObjectKey { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public long SizeBytes { get; init; }
    public DateTime CreatedAtUtc { get; init; }
}
