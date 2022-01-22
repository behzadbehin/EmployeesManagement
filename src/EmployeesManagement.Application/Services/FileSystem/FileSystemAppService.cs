using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring;
using System.Linq;
using Volo.Abp.Json;
using Volo.Abp.Domain.Repositories;

namespace EmployeeManagement.FileSystems
{
    public class FileSystemAppService : ApplicationService, IFileSystemAppService
    {
         private IRepository<DatabaseBlob> BehzadRepository => LazyServiceProvider.LazyGetService<IRepository<DatabaseBlob>>();

        public IJsonSerializer JsonSerializer { get; }
        private IBlobContainer _blobContainer;
        private readonly IBlobContainerFactory _blobFactory;

        private readonly IDatabaseBlobContainerRepository _blobContainerRepository;
        private readonly IDatabaseBlobRepository _blobRepository;
        public FileSystemAppService(IBlobContainer blobContainer, IBlobContainerFactory blobFactory,
        IDatabaseBlobRepository blobRepository, IDatabaseBlobContainerRepository blobContainerRepository
        , IJsonSerializer jsonSerializer)
        {
            _blobFactory = blobFactory;
            _blobContainer = blobContainer;
            _blobContainerRepository = blobContainerRepository;
            _blobRepository = blobRepository;
            JsonSerializer = jsonSerializer;
        }

        public async Task<SaveFilesInputDto> InsertUpdate(SaveFilesInputDto saveFilesInputDto)
        {

            _blobContainer = _blobFactory.Create(saveFilesInputDto.Name);
            foreach (var item in saveFilesInputDto.Files)
            {
                //    await _blobContainer.SaveAsync(

                //     ); 
            }
            // await _blobContainer.SaveAsync(saveFilesInputDto.Files[0].Name,saveFilesInputDto.Files[0].Content,true);

            //throw new NotImplementedException();
            return null;
        }

        public async Task<SaveFilesInputDto> Upsert(SaveFilesInputDto input)
        {
            var resultContainer = _blobContainerRepository.FindAsync(input.Name);
            if (resultContainer.Result == null) //insert
            {
                var blobContainer = new DatabaseBlobContainer(Guid.NewGuid(), input.Name);
                var blob = new DatabaseBlob(
                    id: Guid.NewGuid(),
                    containerId: blobContainer.Id,
                    content: input.Files[0].FileContent,
                    name: input.Files[0].FileName
                );
                //blob.ExtraProperties = JsonSerializer.Serialize(input.Files[0]);
                var containerResult = await _blobContainerRepository.InsertAsync(blobContainer);
                var blobResult = await _blobRepository.InsertAsync(blob);
            }
            else //update
            {
                
               // BehzadRepository.GetQueryableAsync().Result.Where(x=>x.ContainerId)
            }
            return null;
        }
    }
}