namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.AccessoryFilter)),
        GenerateNewCmdlet(typeof(Models.AccessoryProperty)),
        GenerateSetCmdlet(typeof(Models.AccessoryProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Accessories))]
    public sealed partial class Accessory : IBinding<Models.Accessory>
    {
    }
}
