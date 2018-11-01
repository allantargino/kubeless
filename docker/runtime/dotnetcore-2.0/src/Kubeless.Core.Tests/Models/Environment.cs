using Kubeless.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kubeless.Core.Tests.Utils
{
    public class FunctionEnvironment
    {
        public string Path { get; }
        public string FunctionFileName { get; }
        public IFunction function { get; }

        public FunctionEnvironment(string path, string functionFileName, int timeout)
        {
            Path = path;
            FunctionFileName = functionFileName;
        }

        public string PackagesPath
            => $@"{Path}\Packages";

        public string FunctionFile
            => $@"{Path}\{FunctionFileName}.cs";

        public string AssemblyFile
            => $@"{Path}\{FunctionFileName}.dll";

        public string ProjectFile
            => $@"{Path}\{FunctionFileName}.csproj";
    }
}
