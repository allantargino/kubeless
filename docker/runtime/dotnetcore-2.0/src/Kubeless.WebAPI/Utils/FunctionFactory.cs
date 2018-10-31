using Kubeless.Core.Interfaces;
using Kubeless.Core.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Kubeless.WebAPI.Utils
{
    public class FunctionFactory
    {
        public static IFunction GetFunction(IConfiguration configuration)
        {
            var moduleName = configuration.GetNotNullConfiguration("MOD_NAME");
            var functionHandler = configuration.GetNotNullConfiguration("FUNC_HANDLER");
            var referencesPath = configuration.GetNotNullConfiguration("DOTNETCORE_HOME");

            return new CompiledFunction(moduleName, functionHandler, referencesPath);
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
