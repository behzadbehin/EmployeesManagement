using System.Threading.Tasks;

namespace EmployeesManagement.Data;

public interface IEmployeesManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
