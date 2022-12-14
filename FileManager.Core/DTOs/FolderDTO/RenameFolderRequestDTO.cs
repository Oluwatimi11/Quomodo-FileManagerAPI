using System;
namespace FileManager.Core.DTOs.FolderDTO
{
	public class RenameFolderRequestDTO
	{
        public string FolderName { get; set; } = string.Empty;

        public string FolderPath { get; set; } = string.Empty;

        public string NewFolderName { get; set; } = string.Empty;
    }
}

