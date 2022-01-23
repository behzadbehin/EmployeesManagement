using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.FileSystems;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.Domain.Repositories;

namespace EmployeesManagement.Employees
{
    public class EmployeeAppService : CrudAppService<Employee, EmployeeInput,
    Guid>, IEmployeeAppService
    {
        private readonly IFileSystemAppService _fileSystem;
        private readonly IRepository<Employee, Guid> _repository;
        public EmployeeAppService(IRepository<Employee, Guid> repository, IFileSystemAppService fileSystem) : base(repository)
        {
            _repository = repository;
            _fileSystem = fileSystem;
        }
        public override async Task<EmployeeInput> CreateAsync(EmployeeInput input)
        {
            
            var employee = await base.CreateAsync(input);
            if (employee == null)
                return null;
            
            var saveFileInput = new SaveFilesInputDto { Name = employee.Id.ToString() };
            if(input.Files!=null)
            {
                saveFileInput.Files.AddRange(input.Files);
            }

            await _fileSystem.Upsert(saveFileInput);

            return employee;
        }

        public async Task<DatabaseBlobContainer> CreatingAsync(EmployeeInput input)
        {
            var saveFileInput = new SaveFilesInputDto();
            if(!String.IsNullOrEmpty(input.Name))
            {
                var employee = await base.CreateAsync(input);
                if (employee == null)
                    return null;
                saveFileInput = new SaveFilesInputDto { Name = employee.Id.ToString() };
            }
            
            if(input.Files!=null)
            {
                saveFileInput.Files.AddRange(input.Files);
            }

            var blobContainer = await _fileSystem.Upsert(saveFileInput);

            return blobContainer;
        }

    }
}