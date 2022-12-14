using System;
using FileManager.Domain.Common;

namespace FileManager.Domain.Models
{
	public class Folder : BaseEntity
	{
        public string FolderName { get; set; } = string.Empty;

        public string DirectoryPath { get; set; } = string.Empty;

        public string NewFolderName { get; set; } = string.Empty;

        public ICollection<MyFile>? Files { get; set; }

        public ICollection<Folder>? Folders { get; set; }

    }
}

