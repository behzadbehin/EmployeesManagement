using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagement.FileSystems;
using Volo.Abp.Application.Services;
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

            // await _fileSystem.Upsert(
            //     new SaveFilesInputDto
            //     {
            //         Name = input.Name,
            //         Files = new List<FileDto>()
            //                     {
            //                         new FileDto
            //                         {
            //                             Content = input.ProfilePicture,
            //                             Name = "profile picture",
            //                             Size = "1200",
            //                             Type = "jpg"
            //                         }
            //                     }
            //     }
            // );
            //return null;
            var employee = await base.CreateAsync(input);
            var saveFileInput = new SaveFilesInputDto { Name = employee.Id.ToString() };
            if(input.Files!=null)
            {
                saveFileInput.Files.AddRange(input.Files);
            }

            await _fileSystem.Upsert(saveFileInput);

            return employee;
        }
    }
}