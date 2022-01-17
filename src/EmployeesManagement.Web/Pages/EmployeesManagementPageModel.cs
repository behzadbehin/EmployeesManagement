using EmployeesManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EmployeesManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class EmployeesManagementPageModel : AbpPageModel
{
    protected EmployeesManagementPageModel()
    {
        LocalizationResourceType = typeof(EmployeesManagementResource);
    }
}
