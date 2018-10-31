using Kubeless.Core.Interfaces;
using Kubeless.Core.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Kubeless.WebAPI.Utils
{
    public class FunctionFactory
    {
        private const string PUBLISH_PATH = "publish/";
        private const string PACKAGES_PATH = "packages/";

        public static IFunction GetFunction(IConfiguration configuration)
        {
            var moduleName = configuration.GetNotNullConfiguration("MOD_NAME");
            var functionHandler = configuration.GetNotNullConfiguration("FUNC_HANDLER");
            var basePath = "/kubeless/";
            var publishPath = Path.Combine(basePath, PUBLISH_PATH);

            return new CompiledFunction(moduleName, functionHandler, publishPath);
        }

        public static int GetFunctionTimeout(IConfiguration configuration)
        {
            var timeoutSeconds = configuration.GetNotNullConfiguration("FUNC_TIMEOUT");
            var milisecondsInSecond = 1000;

            return int.Parse(timeoutSeconds) * milisecondsInSecond;
        }

        public static string GetFunctionReferencesPath(IConfiguration configuration)
        {
            var referencesPath = configuration.GetNotNullConfiguration("DOTNETCORE_HOME");

            return referencesPath;
        }
    }
}
