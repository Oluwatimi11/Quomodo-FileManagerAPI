using System.Transactions;
using AutoMapper;
using FileManager.Core.DTOs.FileDTO;
using FileManager.Core.DTOs.FolderDTO;
using FileManager.Domain.Models;

namespace FileManager.Core.Utilities.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MyFile, FileRequestDTO>().ReverseMap();
            CreateMap<MyFile, PathRequestDTO>().ReverseMap();
            CreateMap<MyFile, DeleteFileRequestDTO>().ReverseMap();
            CreateMap<MyFile, FileResponseDTO>().ReverseMap();
            CreateMap<Folder, FolderRequestDTO>().ReverseMap();
            CreateMap<Folder, FolderResponseDTO>().ReverseMap();
            CreateMap<Folder, CreateFolderRequestDTO>().ReverseMap();
            CreateMap<Folder, RenameFolderRequestDTO>().ReverseMap();
        }
    }
}
