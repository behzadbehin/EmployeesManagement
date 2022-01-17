using Volo.Abp.Settings;

namespace EmployeesManagement.Settings;

public class EmployeesManagementSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(EmployeesManagementSettings.MySetting1));
    }
}
