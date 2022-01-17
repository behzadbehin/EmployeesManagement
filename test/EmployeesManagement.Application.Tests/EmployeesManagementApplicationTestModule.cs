using Volo.Abp.Modularity;

namespace EmployeesManagement;

[DependsOn(
    typeof(EmployeesManagementApplicationModule),
    typeof(EmployeesManagementDomainTestModule)
    )]
public class EmployeesManagementApplicationTestModule : AbpModule
{

}
