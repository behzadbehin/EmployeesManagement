using System;
using System.Collections.Generic;
using System.Text;
using EmployeesManagement.Localization;
using Volo.Abp.Application.Services;

namespace EmployeesManagement;

/* Inherit your application services from this class.
 */
public abstract class EmployeesManagementAppService : ApplicationService
{
    protected EmployeesManagementAppService()
    {
        LocalizationResource = typeof(EmployeesManagementResource);
    }
}
