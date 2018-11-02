using Kubeless.Core.Interfaces;
using Kubeless.Core.Invokers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kubeless.Core.Tests.Utils
{
    public class InvokerFactory
    {
        private const string BASE_PATH = "./functions-tests";
        private const string PACKAGES_SUBPATH = "packages/";

        public static IInvoker GetFunctionInvoker(
            string language, string functionFileName, string moduleName, string functionHandler,
            int timeout = 180000)
        {
            FunctionFactory factory = new FunctionFactory(BASE_PATH, PACKAGES_SUBPATH);
            string functionPath = factory.CreateEnvironmentPath(BASE_PATH, language, functionFileName);
            string referencesPath = Path.Combine(functionPath, PACKAGES_SUBPATH);
            IFunction function = factory.CompileFunction(functionPath, moduleName, functionHandler);
            IInvoker invoker = new CompiledFunctionInvoker(function, timeout, referencesPath);

            return invoker;
        }
    }
}
