namespace SnipeSharp.PowerShell
{
    [GenerateBinding,
        GenerateGetCmdlet,
        GenerateRemoveCmdlet,
        GenerateFindCmdlet(typeof(Models.DepartmentFilter)),
        GenerateNewCmdlet(typeof(Models.DepartmentProperty)),
        GenerateSetCmdlet(typeof(Models.DepartmentProperty))]
    [AssociatedEndPoint(nameof(SnipeItApi.Departments))]
    public sealed partial class Department : IBinding<Models.Department>
    {
    }
}
