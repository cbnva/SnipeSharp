namespace SnipeSharp.PowerShell.Generator
{
    internal sealed class CmdletTarget
    {
        public string Name { get; }
        public string BindingType { get; }
        public string ModelType { get; }
        public string EndPointPropertyName { get; }

        public CmdletTarget(string name, string bindingType, string modelType, string endpoint)
        {
            Name = name;
            BindingType = bindingType;
            ModelType = modelType;
            EndPointPropertyName = endpoint;
        }
    }
}
