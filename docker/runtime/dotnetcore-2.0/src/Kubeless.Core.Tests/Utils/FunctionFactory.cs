using Kubeless.Core.Interfaces;
using Kubeless.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kubeless.Core.Tests.Utils
{
    public class FunctionFactory
    {
        private readonly string basePath;

        public FunctionFactory(string basePath)
        {
            this.basePath = basePath;
        }

        public IFunction CompileFunction(string language, string functionFileName, string moduleName, string functionHandler)
        {
            var env = CreateEnvironment(basePath, language, functionFileName);
            string publishPath = null;

            return new CompiledFunction(moduleName, functionHandler, publishPath);
        }

        public static FunctionEnvironment CreateEnvironment(string basePath, string language, string functionFileName)
        {
            var environmentPath = Path.Combine(basePath, language, functionFileName);

            EnsureDirectoryIsClear(environmentPath);

            var functionFiles = Directory.EnumerateFiles(basePath, $"{functionFileName}.*");

            CopyFunctionsFiles(functionFiles, environmentPath);

            var environment = new FunctionEnvironment(environmentPath, functionFileName, 180);



            return environment;
        }

        private static void EnsureDirectoryIsClear(string directory)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, recursive: true);
            Directory.CreateDirectory(directory);
        }

        public static void CopyFunctionsFiles(IEnumerable<string> files, string destination)
        {
            foreach (var f in files)
            {
                var name = Path.GetFileName(f);
                File.Copy(f, Path.Combine(destination, name));
            }
        }
    }
}
