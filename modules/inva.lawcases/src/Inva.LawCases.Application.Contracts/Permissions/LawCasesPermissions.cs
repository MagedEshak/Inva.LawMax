using Volo.Abp.Reflection;

namespace Inva.LawCases.Permissions;

public class LawCasesPermissions
{
    public const string GroupName = "LawCases";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(LawCasesPermissions));
    }
}
