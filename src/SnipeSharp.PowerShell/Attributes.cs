using System;

namespace SnipeSharp.PowerShell
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    internal sealed class AssociatedEndPointAttribute: Attribute
    {
        public string PropertyName { get; }

        public AssociatedEndPointAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateGetCmdletAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateFindCmdletAttribute: Attribute
    {
        internal GenerateFindCmdletAttribute(Type filterType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateRemoveCmdletAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateBindingAttribute: Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateNewCmdletAttribute: Attribute
    {
        internal GenerateNewCmdletAttribute(Type postType)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class GenerateSetCmdletAttribute: Attribute
    {
        internal GenerateSetCmdletAttribute(Type putPatchType)
        {
        }
        internal GenerateSetCmdletAttribute(Type putType, Type patchType)
        {
        }
    }
}
