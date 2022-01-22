using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeesManagement.Employees;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring.Database;

namespace EmployeeManagement.FileSystems
{
    public interface IFileSystemAppService : IApplicationService
    {
        Task<SaveFilesInputDto> Upsert(SaveFilesInputDto saveFilesInputDto);
    }

    public class SaveFilesInputDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        // public List<FileDto> Files { get; set; }
        public List<FileObject> Files { get; set; }
    }
    public class FileDto : EntityDto<Guid> //instead blob
    {
        public string Name { get; set; }
        public byte[] Content{get; set;}
        public string Type{get; set;}
        public string Size { get; set; }
    }
}