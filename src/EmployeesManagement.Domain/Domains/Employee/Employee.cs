using System;
using Volo.Abp.Domain.Entities;

namespace EmployeesManagement.Employees
{
    public class Employee : Entity<Guid>
    {
        public string Name { get; set; }
    }
}