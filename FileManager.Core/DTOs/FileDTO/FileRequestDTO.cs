using System;
namespace FileManager.Core.DTOs.FileDTO
{
	public class FileRequestDTO
	{
        public string FileName { get; set; } = string.Empty;

        public string FolderPath { get; set; } = string.Empty;

        public string NewFileName { get; set; } = string.Empty;

    }
}

