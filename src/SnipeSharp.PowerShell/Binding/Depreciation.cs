namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.DepreciationFilter)),
        GenerateNewCmdlet(typeof(Models.DepreciationProperty)),
        GenerateSetCmdlet(typeof(Models.DepreciationProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Depreciations))]
    public sealed partial class Depreciation : IBinding<Models.Depreciation>
    {
    }
}
