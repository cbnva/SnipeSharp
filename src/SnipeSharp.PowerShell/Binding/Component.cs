namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateFindCmdlet(typeof(Models.ComponentFilter)),
        GenerateNewCmdlet(typeof(Models.ComponentProperty)),
        GenerateSetCmdlet(typeof(Models.ComponentProperty)),
        GenerateRemoveCmdlet]
    [AssociatedEndPoint(nameof(SnipeItApi.Components))]
    public sealed partial class Component : IBinding<Models.Component>
    {
    }
}
