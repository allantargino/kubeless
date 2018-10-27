using Kubeless.Core.Interfaces;
using Kubeless.Core.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace Kubeless.WebAPI.Utils
{
    public class FunctionFactory
    {
        public static IFunctionSettings BuildFunctionSettings(IConfiguration configuration)
        {
            var moduleName = configuration["MOD_NAME"];
            if (string.IsNullOrEmpty(moduleName))
                throw new ArgumentNullException("MOD_NAME");

            var functionHandler = configuration["FUNC_HANDLER"];
            if (string.IsNullOrEmpty(moduleName))
                throw new ArgumentNullException("FUNC_HANDLER");

            var assemblyPathConfiguration = configuration["FunctionAssemblyPath"];
            if (string.IsNullOrEmpty(assemblyPathConfiguration))
                throw new ArgumentNullException("FunctionAssemblyPath");
            var assemblyPath = string.Concat(assemblyPathConfiguration, "project", ".dll");
            var assembly = new BinaryContent(assemblyPath);

            return new FunctionSettings(moduleName, functionHandler, assembly);
        }

        public static IFunction BuildFunction(IConfiguration configuration)
        {
            var settings = BuildFunctionSettings(configuration);
            return new Function(settings);
        }
    }
}
