using System;
namespace FileManager.Core.DTOs.FolderDTO
{
	public class FolderResponseDTO
	{
        public string FolderName { get; set; } = string.Empty;

        public string FolderPath { get; set; } = string.Empty;

        public string DirectoryPath { get; set; } = string.Empty;

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

