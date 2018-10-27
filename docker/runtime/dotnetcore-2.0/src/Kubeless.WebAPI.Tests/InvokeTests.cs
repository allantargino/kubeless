using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;

namespace Kubeless.WebAPI.Tests
{
    public class InvokeTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public InvokeTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
