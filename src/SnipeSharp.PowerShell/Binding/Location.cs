namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.LocationFilter)),
        GenerateNewCmdlet(typeof(Models.LocationProperty)),
        GenerateSetCmdlet(typeof(Models.LocationProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Locations))]
    public sealed partial class Location : IBinding<Models.Location>
    {
    }
}
