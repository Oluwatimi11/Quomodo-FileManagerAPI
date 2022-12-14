using System;
namespace FileManager.Core.DTOs.FolderDTO
{
	public class CreateFolderRequestDTO
	{
        public string FolderName { get; set; } = string.Empty;

        public string DirectoryPath { get; set; } = string.Empty;
    }
}

