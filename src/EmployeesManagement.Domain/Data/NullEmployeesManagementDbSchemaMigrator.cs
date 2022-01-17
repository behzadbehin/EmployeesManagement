using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EmployeesManagement.Data;

/* This is used if database provider does't define
 * IEmployeesManagementDbSchemaMigrator implementation.
 */
public class NullEmployeesManagementDbSchemaMigrator : IEmployeesManagementDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
