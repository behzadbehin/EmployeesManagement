using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EmployeesManagement.Web;

[Dependency(ReplaceServices = true)]
public class EmployeesManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "EmployeesManagement";
}
