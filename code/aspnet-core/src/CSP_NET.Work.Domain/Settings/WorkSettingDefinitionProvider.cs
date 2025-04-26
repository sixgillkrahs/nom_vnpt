using CTIN.Abp.Settings;

namespace CSP_NET.Work.Settings;

public class WorkSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WorkSettings.MySetting1));
    }
}
