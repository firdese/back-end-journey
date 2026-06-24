#nullable enable

using Amazon.S3;
using Amazon.S3.Model;
using TaskTracker.Application.Dtos.Storage;
using TaskTracker.Application.Interfaces.Services;

namespace TaskTracker.Infrastructure.Storage;

public class S3ObjectStorageService(
    IAmazonS3 s3Client,
    S3StorageOptions options) : IObjectStorageService
{
    public async Task<StoredObjectDto> UploadAsync(
        string objectKey,
        string fileName,
        string contentType,
        Stream content,
        CancellationToken cancellationToken = default)
    {
        EnsureBucketConfigured();

        var request = new PutObjectRequest
        {
            BucketName = options.BucketName,
            Key = objectKey,
            InputStream = content,
            ContentType = contentType
        };

        await s3Client.PutObjectAsync(request, cancellationToken);

        return new StoredObjectDto
        {
            ObjectKey = objectKey,
            FileName = fileName,
            ContentType = contentType,
            SizeBytes = content.CanSeek ? content.Length : 0
        };
    }

    public async Task<StorageObjectDownloadDto?> DownloadAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        EnsureBucketConfigured();

        try
        {
            var response = await s3Client.GetObjectAsync(
                options.BucketName,
                objectKey,
                cancellationToken);

            return new StorageObjectDownloadDto
            {
                Content = response.ResponseStream,
                ContentType = response.Headers.ContentType ?? "application/octet-stream",
                FileName = Path.GetFileName(objectKey),
                SizeBytes = response.Headers.ContentLength
            };
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task DeleteAsync(
        string objectKey,
        CancellationToken cancellationToken = default)
    {
        EnsureBucketConfigured();

        await s3Client.DeleteObjectAsync(
            options.BucketName,
            objectKey,
            cancellationToken);
    }

    private void EnsureBucketConfigured()
    {
        if (string.IsNullOrWhiteSpace(options.BucketName))
        {
            throw new InvalidOperationException("AWS:S3:BucketName must be configured.");
        }
    }
}
