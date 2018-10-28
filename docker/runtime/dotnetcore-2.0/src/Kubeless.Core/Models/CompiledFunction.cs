using Kubeless.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kubeless.Core.Models
{
    public class CompiledFunction : IFunction
    {
        public string ModuleName { get; }
        public string FunctionHandler { get; }
        public string FunctionFile { get; }

        public CompiledFunction(string moduleName, string functionHandler, string basePath)
        {
            ModuleName = moduleName;
            FunctionHandler = functionHandler;
            FunctionFile = Path.Combine(basePath, "project.dll");

            if (!File.Exists(FunctionFile))
                throw new FileNotFoundException("Function DLL not found.", FunctionFile);
        }
    }
}
