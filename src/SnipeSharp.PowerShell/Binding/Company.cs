namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateFindCmdlet(typeof(Models.CompanyFilter)),
        GenerateNewCmdlet(typeof(Models.CompanyProperty)),
        GenerateSetCmdlet(typeof(Models.CompanyProperty)),
        GenerateRemoveCmdlet]
    [AssociatedEndPoint(nameof(SnipeItApi.Companies))]
    public sealed partial class Company : IBinding<Models.Company>
    {
    }
}
