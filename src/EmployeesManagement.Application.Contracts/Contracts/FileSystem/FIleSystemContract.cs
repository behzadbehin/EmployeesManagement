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
        Task<DatabaseBlobContainer> Upsert(SaveFilesInputDto saveFilesInputDto);
        void DeleteFilesAsync(string containerName);
        Task<List<DatabaseBlob>> GetFilesAsync(string containerName);
    }

    public class SaveFilesInputDto : EntityDto<Guid>
    {
        public SaveFilesInputDto()
        {
            Files = new List<FileObject>();
        }
        public string Name { get; set; }
        public List<FileObject> Files { get; set; }
    }

   
}