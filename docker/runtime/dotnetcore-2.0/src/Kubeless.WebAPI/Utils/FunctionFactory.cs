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
            var assemblyPathConfiguration = configuration.GetNotNullConfiguration("FunctionAssemblyPath");

            return new CompiledFunction(moduleName, functionHandler, assemblyPathConfiguration);
        }

        public static int GetFunctionTimeout(IConfiguration configuration)
        {
            var timeout = configuration.GetNotNullConfiguration("FUNC_TIMEOUT");

            return int.Parse(timeout) * 1000; // seconds
        }

        public static string GetFunctionReferencesPath(IConfiguration configuration)
        {
            var referencesPath = configuration.GetNotNullConfiguration("DOTNETCORE_HOME");

            return referencesPath;
        }
    }
}
