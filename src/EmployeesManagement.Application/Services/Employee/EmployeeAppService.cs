using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace EmployeesManagement.Employees
{
    public class EmployeeAppService : CrudAppService<Employee, SaveEmployeeDto, Guid>, IEmployeeAppService
    {
        private readonly IRepository<Employee, Guid> _repository;
        public EmployeeAppService(IRepository<Employee, Guid> repository) : base(repository)
        {
            _repository = repository;
        }

        public Task DeleteAsync(SaveEmployeeDto id)
        {
            throw new NotImplementedException();
        }

        public Task<SaveEmployeeDto> GetAsync(SaveEmployeeDto id)
        {
            throw new NotImplementedException();
        }

        public Task<SaveEmployeeDto> UpdateAsync(SaveEmployeeDto id, SaveEmployeeDto input)
        {
            throw new NotImplementedException();
        }
    }


}