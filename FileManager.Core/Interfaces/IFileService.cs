using System;
using FileManager.Core.DTOs.FileDTO;
using Microsoft.AspNetCore.Http;
using static FileManager.Core.DTOs.ResponseDTO;

namespace FileManager.Core.Interfaces
{
	public interface IFileService
	{

        ResponseDto<FileResponseDTO> UploadFile(IFormFile file, PathRequestDTO pathRequestDto);

        ResponseDto<FileResponseDTO> RenameFile(FileRequestDTO fileRequestDto);

        ResponseDto<FileResponseDTO> DeleteFile(DeleteFileRequestDTO pathRequestDto);

    }
}


