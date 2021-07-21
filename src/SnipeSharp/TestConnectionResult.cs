using System.Net;

namespace SnipeSharp
{
    public struct TestConnectionResult
    {
        public const HttpStatusCode GENERAL_ERROR = (HttpStatusCode)601;
        public readonly HttpStatusCode Result;
        internal TestConnectionResult(HttpStatusCode status)
            => Result = status;

        public static implicit operator bool(TestConnectionResult result)
            => result.Result == HttpStatusCode.OK;
        public static bool operator ==(TestConnectionResult a, TestConnectionResult b)
            => a.Result == b.Result;
        public static bool operator !=(TestConnectionResult a, TestConnectionResult b)
            => a.Result != b.Result;
        public override bool Equals(object? obj)
            => obj is TestConnectionResult a ? a.Result == this.Result : false;
        public override int GetHashCode()
            => this.Result.GetHashCode();

        public static TestConnectionResult OK { get; } = new TestConnectionResult(HttpStatusCode.OK);
        public static TestConnectionResult ERROR { get; } = new TestConnectionResult(TestConnectionResult.GENERAL_ERROR);
    }
}
