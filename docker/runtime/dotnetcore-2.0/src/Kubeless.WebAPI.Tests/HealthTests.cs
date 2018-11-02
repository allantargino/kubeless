using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kubeless.WebAPI.Tests
{
    public class HealthTests
    {

        [Fact]
        public async Task PerformHealthCheck()
        {
            // Arrange
            Environment.SetEnvironmentVariable("MOD_NAME", "hellowithdata");
            Environment.SetEnvironmentVariable("FUNC_HANDLER", "handler");
            Environment.SetEnvironmentVariable("FUNC_TIMEOUT", "180");
            Environment.SetEnvironmentVariable("FUNC_PORT", "8080");
            Environment.SetEnvironmentVariable("FUNC_RUNTIME", "dotnetcore2.0");
            Environment.SetEnvironmentVariable("FUNC_MEMORY_LIMIT", "0");
            Environment.SetEnvironmentVariable("FunctionAssemblyPath", "../../../../../functions/");

            var _server = new TestServer(Program.CreateWebHostBuilder());



            var client = _server.CreateClient();


            // Act
            var response = await client.GetAsync("/healthz");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

    }
}
