using System;
using System.ComponentModel.DataAnnotations;

namespace FileManager.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string FolderPath { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

