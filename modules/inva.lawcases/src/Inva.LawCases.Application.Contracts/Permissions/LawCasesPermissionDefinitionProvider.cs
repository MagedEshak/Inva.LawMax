using Inva.LawCases.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Inva.LawCases.Permissions;

public class LawCasesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LawCasesPermissions.GroupName, L("Permission:LawCases"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LawCasesResource>(name);
    }
}
