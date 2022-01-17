using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring;

namespace EmployeeManagement.FileSystems
{
    public class FileSystemAppService : ApplicationService, IFileSystemAppService
    {
        private IBlobContainer _blobContainer;
        private readonly IBlobContainerFactory _blobFactory;
        public FileSystemAppService(IBlobContainer blobContainer, IBlobContainerFactory blobFactory)
        {
            _blobFactory = blobFactory;
            _blobContainer = blobContainer;
        }

        public async Task<SaveFilesInputDto> Upsert(SaveFilesInputDto saveFilesInputDto)
        {
            
            _blobContainer = _blobFactory.Create(saveFilesInputDto.Name);
            
            await _blobContainer.SaveAsync(saveFilesInputDto.Files[0].Name,saveFilesInputDto.Files[0].Content,true);

            //throw new NotImplementedException();
            return null;
        }
    }
}