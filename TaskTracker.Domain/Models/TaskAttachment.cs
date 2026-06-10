using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskTracker.Domain.Models;

public class TaskAttachment
{
    [Column("taskattachmentid")]
    public int TaskAttachmentId { get; set; }

    [Column("taskid")]
    public int TaskId { get; set; }

    [Column("objectkey")]
    [Required]
    [MaxLength(512)]
    public string ObjectKey { get; set; } = string.Empty;

    [Column("filename")]
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Column("contenttype")]
    [Required]
    [MaxLength(255)]
    public string ContentType { get; set; } = "application/octet-stream";

    [Column("sizebytes")]
    public long SizeBytes { get; set; }

    [Column("createdatutc")]
    public DateTime CreatedAtUtc { get; set; }

    public Task? Task { get; set; }
}
