using Inva.LawMax.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Inva.LawMax.Permissions;

public class LawMaxPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LawMaxPermissions.GroupName);

        var booksPermission = myGroup.AddPermission(LawMaxPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(LawMaxPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(LawMaxPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(LawMaxPermissions.Books.Delete, L("Permission:Books.Delete"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LawMaxPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LawMaxResource>(name);
    }
}
