using Volo.Abp.Settings;

namespace Inva.LawMax.Settings;

public class LawMaxSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LawMaxSettings.MySetting1));
    }
}
