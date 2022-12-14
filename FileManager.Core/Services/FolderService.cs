using System;
using System.Net;
using AutoMapper;
using FileManager.Core.DTOs;
using FileManager.Core.DTOs.FileDTO;
using FileManager.Core.DTOs.FolderDTO;
using FileManager.Core.Interfaces;
using FileManager.Domain.Models;
using ILogger = Serilog.ILogger;
using static FileManager.Core.DTOs.ResponseDTO;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.Core.Services
{
	public class FolderService : IFolderService
	{
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

		public FolderService(IServiceProvider provider, IMapper mapper)
		{
            _logger = provider.GetRequiredService<ILogger>();
            _mapper = mapper;
        }


        /// <summary>
        /// Method creates a folder in a given directory path
        /// </summary>
        /// <param name="folderRequestDto"></param>
        /// <returns></returns>
        public ResponseDto<FolderResponseDTO> AddFolder(CreateFolderRequestDTO folderRequestDto)
        {
            Folder folder;
            folder = _mapper.Map<Folder>(folderRequestDto);
            var path = folder.DirectoryPath;
            var name = folder.FolderName;
            var folderpath = string.Empty;
           

            try
            {
                _logger.Information($"Checking for existence of folder path {path}");
                if (!Directory.Exists(Path.Combine(path, name)))
                {
                    _logger.Information($"Path created as it doesn't exist");
                    folderpath = Directory.CreateDirectory(Path.Combine(path, name)).ToString();
                }
                _logger.Information($"Path has been created successfully");

                folder.FolderPath = folderpath;
                var folderResponse = _mapper.Map<FolderResponseDTO>(folder);
                return ResponseDto<FolderResponseDTO>.Success($"Your folder {folderResponse.FolderName}, with path {folderResponse.FolderPath} has been created Successfully", folderResponse, (int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Folder could not be created");
                return ResponseDto<FolderResponseDTO>.Fail("Folder could not be created in directory with path {path}", (int)HttpStatusCode.BadRequest);
            }
        }


        /// <summary>
        /// Method deletes folder in a specified directory path
        /// </summary>
        /// <param name="folderRequestDto"></param>
        /// <returns></returns>
        public ResponseDto<FolderResponseDTO> DeleteFolder(FolderRequestDTO folderRequestDto)
        {

            Folder folder;
            folder = _mapper.Map<Folder>(folderRequestDto);
            var path = folder.FolderPath;
            var name = folder.FolderName;
            var folderpath = string.Empty;

            var check = Path.Combine(path, name);
            try
            {
                _logger.Information($"Checking for existence of directory {check}");
                if (Directory.Exists(check))
                {
                    _logger.Information($"Folder {name} has been deleted successfully");
                    Directory.Delete(check, true);
                }
                folder.FolderPath = "";
                folder.FolderName = "";
                var folderResponse = _mapper.Map<FolderResponseDTO>(folder);
                return ResponseDto<FolderResponseDTO>.Success($"Your folder {folderResponse.FolderName} has been deleted Successfully", folderResponse, (int)HttpStatusCode.OK);

            }
            catch (Exception)
            {
                return ResponseDto<FolderResponseDTO>.Fail("Folder {name} could not be deleted", (int)HttpStatusCode.BadRequest);
            }
        }



        /// <summary>
        /// Method gets all children files in folder
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResponseDto<IEnumerable<string>> GetAllFilesAsync(string path)
        {
            List<string> folders = new List<string>();
            try
            {
                _logger.Information($"Retreiving all files from {path}");
                string[] files = Directory.GetFiles(path);

                foreach (var file in files)
                {
                    var item = (Path.GetFileName(file));
                    folders.Add(item);
                }
                return ResponseDto<IEnumerable<string>>.Success($"Your List of files are: ", folders, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Unable to retrieve all files from {path}");
                throw;
            }
        }



        /// <summary>
        /// Method provides list of all subfolders and files in a specified folder path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResponseDto<IEnumerable<string>> GetAllSubFoldersAndFilesAsync(string path)
        {
            List<string> response = new List<string>();
            try
            {
                _logger.Information($"Retreiving all subfolders and files from {path}");
                string[] children = Directory.GetFiles(path);

                foreach (var folder in children)
                {
                    var item = (Path.GetFileName(folder));
                    response.Add(item);
                }

                string[] items = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
                foreach (var folder in items)
                {
                    var dir = (Path.GetFileName(folder));
                    response.Add(dir);
                }
                return ResponseDto<IEnumerable<string>>.Success($"Your List of subfolders and files are: ", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Unable to retrieve all subfolders and files from {path}");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all subfolders in a specific folder path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ResponseDto<IEnumerable<string>> GetFolders(string path)
        {
            List<string> response = new List<string>();
            try
            {
                _logger.Information($"Retreiving all subfolders from {path}");
                string[] folderpaths = Directory.GetFiles(path);

                string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);

                foreach (var folder in directories)
                {
                    var item = (Path.GetFileName(folder));
                    response.Add(item);
                }
                return ResponseDto<IEnumerable<string>>.Success($"Your List of subfolders are: ", response, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Unable to retrieve all subfolders from {path}");
                throw;
            }
        }

        public ResponseDto<FolderResponseDTO> UpdateFolder(RenameFolderRequestDTO folderRequestDto)
        {
            Folder folder;
            folder = _mapper.Map<Folder>(folderRequestDto);
            var path = folder.FolderPath;
            var name = folder.FolderName;
            var newName = folder.NewFolderName;

            try
            {
                _logger.Information($"Attempting folder rename");
                Directory.Move(Path.Combine(path, name), Path.Combine(path, newName));


                folder.FolderName = newName;
                folder.NewFolderName = "";
                var folderResponse = _mapper.Map<FolderResponseDTO>(folder);
                return ResponseDto<FolderResponseDTO>.Success($"Folder was renames successfully", folderResponse, (int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, $"Unable to rename specified folder");
                throw;
            }
        }
    }
}

