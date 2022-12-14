using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileManager.Core.DTOs.FolderDTO;
using FileManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService _folderService;

        public FolderController(IFolderService folderService)
        {
            _folderService = folderService;
        }

        [HttpPost("create-folder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateFolderAsync([FromQuery] CreateFolderRequestDTO folderRequestDTO)
        {
            var response = _folderService.AddFolder(folderRequestDTO);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPut("rename-folder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RenameFolder([FromQuery] RenameFolderRequestDTO folderRequestDTO)
        {
            var result = _folderService.UpdateFolder(folderRequestDTO);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete-folder")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteFolder([FromQuery] FolderRequestDTO folderRequestDTO)
        {
            var result = _folderService.DeleteFolder(folderRequestDTO);
            return StatusCode(result.StatusCode, result);
        }


        [HttpGet("get-all-files")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllFilesAsync([FromQuery] string path)
        {
            var result = _folderService.GetAllFilesAsync(path);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-all-subFolders")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSubFoldersAsync([FromQuery] string path)
        {
            var result = _folderService.GetFolders(path);
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("get-all-subFolders-and-file")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllSubFoldersAndFiles([FromQuery] string path)
        {
            var result = _folderService.GetAllSubFoldersAndFilesAsync(path);
            return StatusCode(result.StatusCode, result);
        }
    }
}

