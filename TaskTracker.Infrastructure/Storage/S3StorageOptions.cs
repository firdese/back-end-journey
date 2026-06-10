namespace TaskTracker.Infrastructure.Storage;

public class S3StorageOptions
{
    public const string SectionName = "AWS:S3";

    public string BucketName { get; init; } = string.Empty;
}
