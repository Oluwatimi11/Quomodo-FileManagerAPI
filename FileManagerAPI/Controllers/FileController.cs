using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FileManager.Core.DTOs.FileDTO;
using FileManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FileManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {

        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpPost("upload-file")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UploadFile([FromQuery] PathRequestDTO pathRequestDto, IFormFile file)
        {
            var result = _fileService.UploadFile(file, pathRequestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("delete-file")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteFile([FromQuery] DeleteFileRequestDTO pathRequestDto)
        {
            var result = _fileService.DeleteFile(pathRequestDto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("rename-file")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult RenameFile([FromQuery] FileRequestDTO fileRequestDTO)
        {
            var result = _fileService.RenameFile(fileRequestDTO);
            return StatusCode(result.StatusCode, result);
        }
    }
}

