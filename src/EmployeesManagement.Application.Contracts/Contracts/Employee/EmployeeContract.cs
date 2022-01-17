using System;
using System.IO;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace EmployeesManagement.Employees
{
    public class SaveEmployeeDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public byte[] ProfilePicture { get; set; }
        
        
    }

    public interface IEmployeeAppService : ICrudAppService<SaveEmployeeDto,Guid>
    {
        
    }
}