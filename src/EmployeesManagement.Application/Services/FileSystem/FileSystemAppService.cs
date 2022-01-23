using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeesManagement.Employees;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Json;

namespace EmployeeManagement.FileSystems
{
    public class
    FileSystemAppService
    : ApplicationService, IFileSystemAppService
    {
        public DatabaseBlobContainer BlobContainer{get; set;}
        public IJsonSerializer JsonSerializer { get; }
        private readonly IDatabaseBlobContainerRepository _blobContainerRepository;

        private readonly IDatabaseBlobRepository _blobRepository;

        public FileSystemAppService(
            IDatabaseBlobRepository blobRepository,
            IDatabaseBlobContainerRepository blobContainerRepository,
            IJsonSerializer jsonSerializer
        )
        {
            _blobContainerRepository = blobContainerRepository;
            _blobRepository = blobRepository;
            JsonSerializer = jsonSerializer;
        }

        public async Task<DatabaseBlobContainer> Upsert(SaveFilesInputDto input)
        {
            if (!input.Files.Any()) return null;

            bool isNew = true;

            var containerName = "";

            //the main object or entity does not exist but the files are going to be saved with new guid
            if (String.IsNullOrEmpty(input.Name))
            {
                //insert => container Name = new Guid
                containerName = Guid.NewGuid().ToString();
            } //the Name has value and it must be the entity id
            else
            {
                //get from database container
                BlobContainer = _blobContainerRepository .GetListAsync() .Result .FirstOrDefault(x => x.Name == input.Name);

                //if has value it must be updated
                if (BlobContainer == null)
                {
                    // there is an entity but it has not any container yet it must be saved with the entity id
                    containerName = input.Name;
                    
                }
                else
                {
                    isNew = false;
                    containerName = input.Name;
                }
            }

            if (isNew)
            {
                //start inserting
                //step 1: insert Container
                BlobContainer = await _blobContainerRepository.InsertAsync(new DatabaseBlobContainer(Guid.NewGuid(), containerName));
                if (BlobContainer != null)
                {
                    //step 2: set Conatainer Id and map file objects to blobs
                    var blobs = MapFiles(fileObjects: input.Files, containerId: BlobContainer.Id);

                    //step 3 : insert
                    await _blobRepository.InsertManyAsync(blobs);

                    return BlobContainer;
                }
                return null;
            }
            else
            {
                //update
                //step 1: delete all old files because I can not recognize which files should be updated and which ones must be delete ...
                var currentBlobs = _blobRepository .GetListAsync() .Result
                        .Where(x => x.ContainerId == BlobContainer.Id)
                        .ToList();
                await _blobRepository.DeleteManyAsync(currentBlobs.Select(x => x.Id).ToList());
                
                //step 2: set Conatainer Id and map file objects to blobs
                var blobs = MapFiles(fileObjects: input.Files, containerId: BlobContainer.Id);

                //step 3 : insert
                await _blobRepository.InsertManyAsync(blobs);
                return BlobContainer;
            }
        }

        public List<DatabaseBlob> MapFiles(List<FileObject> fileObjects, Guid containerId)
        {
            
            // the DatabaseBlob must be create witn constructor so i can not create it witn mapping
            var blobs = fileObjects.Select(f=> new DatabaseBlob(
                id: Guid.NewGuid(),
                containerId: containerId,
                name: f.FileName,
                content: f.FileContent
                )
            ).ToList();
            return blobs;

        }

        public async void DeleteFilesAsync(string containerName)
        {
            if(string.IsNullOrEmpty(containerName))
             return;
            var currentContainer = _blobContainerRepository.GetListAsync().Result.FirstOrDefault(x=>x.Name.ToLower()==containerName.ToLower());
            
            if(currentContainer != null)
            {
                 await _blobContainerRepository.DeleteAsync(currentContainer.Id);
            }
        }

        public Task<List<DatabaseBlob>> GetFilesAsync(string containerName)
        {
            throw new NotImplementedException();
        }

        // public Task<List<DatabaseBlob>> GetFilesAsync(string containerName)
        // {

        //     return _blobRepository .GetListAsync() .Result
        //                 .Where(x => x.ContainerId == BlobContainer.Id)
        //                 .ToList();

        // }
    }
}
