using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EmployeesManagement.Data;
using Volo.Abp.DependencyInjection;

namespace EmployeesManagement.EntityFrameworkCore;

public class EntityFrameworkCoreEmployeesManagementDbSchemaMigrator
    : IEmployeesManagementDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreEmployeesManagementDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the EmployeesManagementDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<EmployeesManagementDbContext>()
            .Database
            .MigrateAsync();
    }
}
