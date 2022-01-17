using EmployeesManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EmployeesManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class EmployeesManagementController : AbpControllerBase
{
    protected EmployeesManagementController()
    {
        LocalizationResource = typeof(EmployeesManagementResource);
    }
}
