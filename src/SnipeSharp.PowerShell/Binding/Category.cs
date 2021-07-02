namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.CategoryFilter)),
        GenerateNewCmdlet(typeof(Models.CategoryProperty)),
        GenerateSetCmdlet(typeof(Models.CategoryProperty), typeof(Models.CategoryProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Categories))]
    public sealed partial class Category : IBinding<Models.Category>
    {
    }
}
