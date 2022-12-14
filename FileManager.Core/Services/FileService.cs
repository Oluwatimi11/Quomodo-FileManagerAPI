using System;
using System.Net;
using AutoMapper;
using FileManager.Core.DTOs;
using FileManager.Core.DTOs.FileDTO;
using FileManager.Core.Interfaces;
using FileManager.Domain.Models;
//using Microsoft.AspNetCore.Http;
using ILogger = Serilog.ILogger;
using static FileManager.Core.DTOs.ResponseDTO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace FileManager.Core.Services
{
	public class FileService : IFileService
	{
        private readonly ILogger _logger;
        private readonly IMapper _mapper;


        public FileService(IServiceProvider provider, IMapper mapper)
		{
            //_logger = logger;
            _logger = provider.GetRequiredService<ILogger>();
            _mapper = mapper;
		}

        /// <summary>
        /// Uploads new file to a specified folder path
        /// </summary>
        /// <param name="fileRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResponseDto<FileResponseDTO> UploadFile(IFormFile file, PathRequestDTO pathRequestDto)
        {
            MyFile myfile;
            myfile = _mapper.Map<MyFile>(pathRequestDto);
            var path = myfile.FolderPath;

            try
            {
                _logger.Information($"Trying to retrieve folder content from {path}");
                if (!Directory.Exists(path))
                {
                    return ResponseDto<FileResponseDTO>.Fail("Folder Not Found", (int)HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Folder could not be created");
            }

            var filePath = Path.Combine(path, file.FileName);
            using (FileStream fileStream = File.Create(filePath))
            myfile.FileName = file.Name;
            myfile.FilePath = filePath;

            var fileResponse = _mapper.Map<FileResponseDTO>(myfile);
            return ResponseDto<FileResponseDTO>.Success($"Your {fileResponse.FileName}, with path {fileResponse.FilePath} has been created Successfully", fileResponse, (int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Deletes a file from a parent folder
        /// </summary>
        /// <param name="fileRequestDto"></param>
        /// <returns></returns>
        public ResponseDto<FileResponseDTO> DeleteFile(DeleteFileRequestDTO pathRequestDto)
        {
            MyFile myfile;
            myfile = _mapper.Map<MyFile>(pathRequestDto);
            var path = myfile.FilePath;

            try
            {
                if (path == null)
                {
                    _logger.Information($"File path is null");
                    return ResponseDto<FileResponseDTO>.Fail("File path cannot be empty, input a file path", (int)HttpStatusCode.BadRequest);
                }

                if (!File.Exists(path))
                {
                    _logger.Information($"We are verifying {path} existence");
                    return ResponseDto<FileResponseDTO>.Fail("File Not Found, input a correct file path", (int)HttpStatusCode.NotFound);
                }

                File.Delete(path);
                _logger.Information($"File has been deleted successfully");
                myfile.FileName = "";
                myfile.FilePath = "";
                myfile.FolderPath = "";

                var fileResponse = _mapper.Map<FileResponseDTO>(myfile);
                return ResponseDto<FileResponseDTO>.Success($"File has been deleted Successfully", fileResponse, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Unable to delete file with path {path}");
                return ResponseDto<FileResponseDTO>.Fail("File with path {path} could not be deleted", (int)HttpStatusCode.BadRequest);
            }
        }


        /// <summary>
        /// Method renames a file from a given folder path
        /// </summary>
        /// <param name="fileRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ResponseDto<FileResponseDTO> RenameFile(FileRequestDTO fileRequestDto)
        {
            MyFile myfile;
            myfile = _mapper.Map<MyFile>(fileRequestDto);
            var folderpath = myfile.FolderPath;
            var filename = myfile.FileName;
            var newfilename = myfile.NewFileName;

            try
            {
                _logger.Information($"Verifying existence of parent folder with path {folderpath}");
                if (!Directory.Exists(folderpath))
                {
                    _logger.Information($"Parent folder does not exist.");
                    return ResponseDto<FileResponseDTO>.Fail("Parent Folder Not Found, input a correct folder path", (int)HttpStatusCode.NotFound);
                }

                File.Move(Path.Combine(folderpath, filename), Path.Combine(folderpath, newfilename));
                _logger.Information($"File has been renamed successfully");

                myfile.FileName = newfilename;
                myfile.NewFileName = "";

                var fileResponse = _mapper.Map<FileResponseDTO>(myfile);
                return ResponseDto<FileResponseDTO>.Success($"File has been updated Successfully", fileResponse, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"File could not be renamed");
                return ResponseDto<FileResponseDTO>.Fail("File {myfile.FileName} could not be updated", (int)HttpStatusCode.BadRequest);
            }
        }
    }
}

