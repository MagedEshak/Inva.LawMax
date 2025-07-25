namespace Inva.LawCases;

public static class LawCasesDbProperties
{
    public static string DbTablePrefix { get; set; } = "LawCases";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "LawCases";
}
