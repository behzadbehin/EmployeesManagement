using EmployeesManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace EmployeesManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EmployeesManagementEntityFrameworkCoreModule),
    typeof(EmployeesManagementApplicationContractsModule)
    )]
public class EmployeesManagementDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
