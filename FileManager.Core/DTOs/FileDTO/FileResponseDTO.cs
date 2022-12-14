using System;
namespace FileManager.Core.DTOs.FileDTO
{
	public class FileResponseDTO
	{
        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FolderPath { get; set; } = string.Empty;

        public string NewFileName { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

