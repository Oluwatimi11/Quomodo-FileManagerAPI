using System;
using FileManager.Core.DTOs.FolderDTO;
using static FileManager.Core.DTOs.ResponseDTO;

namespace FileManager.Core.Interfaces
{
	public interface IFolderService
	{
        ResponseDto<FolderResponseDTO> AddFolder(CreateFolderRequestDTO folderRequestDto);

        ResponseDto<FolderResponseDTO> DeleteFolder(FolderRequestDTO folderRequestDto);

        ResponseDto<IEnumerable<string>> GetAllFilesAsync(string path);

        ResponseDto<IEnumerable<string>> GetAllSubFoldersAndFilesAsync(string path);

        ResponseDto<IEnumerable<string>> GetFolders(string path);

        ResponseDto<FolderResponseDTO> UpdateFolder(RenameFolderRequestDTO folderRequestDto);
    }
}

