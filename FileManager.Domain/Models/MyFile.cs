using System;
using FileManager.Domain.Common;

namespace FileManager.Domain.Models
{
	public class MyFile : BaseEntity
	{
		public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public string FolderId { get; set; } = string.Empty;

        public string NewFileName { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;

		public Folder? Folder { get; set; }
	}
}

