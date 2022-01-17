using EmployeesManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EmployeesManagement.Permissions;

public class EmployeesManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EmployeesManagementPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EmployeesManagementPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EmployeesManagementResource>(name);
    }
}
