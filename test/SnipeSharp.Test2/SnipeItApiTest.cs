using SnipeSharp.Models;
using static SnipeSharp.Test.Utility;
using Xunit;
using System;

namespace SnipeSharp.Test
{
    public sealed class SnipeItApiTest
    {
        [Fact]
        public async void TestConnection_Successful()
        {
            var snipe = new SnipeItApi(TEST_URI, TEST_TOKEN);
            Assert.True(await snipe.TestConnection());
        }

        [Fact]
        public async void TestConnection_BadUri_Fails()
        {
            var snipe = new SnipeItApi(new Uri("http://fake.localhost"), TEST_TOKEN);
            Assert.False(await snipe.TestConnection());
        }

        [Fact]
        public async void TestConnection_BadToken_Fails()
        {
            var snipe = new SnipeItApi(TEST_URI, string.Empty);
            Assert.False(await snipe.TestConnection());
        }
    }
}
