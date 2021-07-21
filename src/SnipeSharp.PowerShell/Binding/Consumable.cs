namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.ConsumableFilter)),
        GenerateNewCmdlet(typeof(Models.ConsumableProperty)),
        GenerateSetCmdlet(typeof(Models.ConsumableProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Consumables))]
    public sealed partial class Consumable : IBinding<Models.Consumable>
    {
    }
}
