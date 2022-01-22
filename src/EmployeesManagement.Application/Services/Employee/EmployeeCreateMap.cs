

using System;
using AutoMapper;
using Volo.Abp.Guids;

namespace EmployeesManagement.Employees
{
    public class EmployeeCreateMap : Profile
    {
        public EmployeeCreateMap()
        {
            CreateMap<Employee, SaveEmployeeDto>();
            CreateMap<SaveEmployeeDto, Employee>();
            CreateMap<Employee, EmployeeInput>();
            CreateMap<EmployeeInput, Employee>();
        }
    }

}