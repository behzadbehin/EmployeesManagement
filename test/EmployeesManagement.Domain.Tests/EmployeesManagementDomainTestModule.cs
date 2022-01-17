using EmployeesManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EmployeesManagement;

[DependsOn(
    typeof(EmployeesManagementEntityFrameworkCoreTestModule)
    )]
public class EmployeesManagementDomainTestModule : AbpModule
{

}
