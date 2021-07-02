namespace SnipeSharp.PowerShell
{
    internal interface IBinding<T>: IApiObject<T> where T: class, IApiObject<T>
    {
        bool HasObject { get; }
        T? Object { get; }
    }
}
