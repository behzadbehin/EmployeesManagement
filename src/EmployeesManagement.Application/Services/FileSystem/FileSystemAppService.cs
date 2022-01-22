using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring;
using System.Linq;
using Volo.Abp.Json;
using Volo.Abp.Domain.Repositories;
using System.Collections.Generic;
using EmployeesManagement.Employees;

namespace EmployeeManagement.FileSystems
{
    public class FileSystemAppService : ApplicationService, IFileSystemAppService
    {
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
            var resultContainer = string.IsNullOrEmpty(input.Name) ? _blobContainerRepository.GetListAsync()
                                                            .Result
                                                            .FirstOrDefault(x => x.Name == input.Name)
                                                            : null;

            if (resultContainer == null) //insert
            {
                var newGuid = Guid.NewGuid();

                var blobContainer = new DatabaseBlobContainer(newGuid, newGuid.ToString());
                var containerResult = await _blobContainerRepository.InsertAsync(blobContainer);
             //   var dbl = ObjectMapper.Map<List<FileObject>, List<DatabaseBlob>>(input.Files);
                var dbl= new List<DatabaseBlob>();
                foreach (var item in input.Files)
                {
                    var blob = new DatabaseBlob(
                        id: Guid.NewGuid(),
                        containerId: newGuid,
                        content: item.FileContent,
                        name: item.FileName
                    );
                    dbl.Add(blob);
                }

                //blob.ExtraProperties = JsonSerializer.Serialize(input.Files[0]);
                await _blobRepository.InsertManyAsync(dbl);
            }
            else //update
            {
                var testblob = _blobRepository.GetListAsync().Result.Where(x => x.ContainerId == resultContainer.Id).ToList();
                await _blobRepository.DeleteManyAsync(testblob.Select(x => x.Id).ToList());
                var dbl = ObjectMapper.Map<List<FileObject>, List<DatabaseBlob>>(input.Files);
                await _blobRepository.InsertManyAsync(dbl);

            }
            return null;
        }
    }
}