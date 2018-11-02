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
        private readonly FunctionCompiler compiler;

        public FunctionFactory(string basePath)
        {
            this.basePath = basePath;
            this.compiler = new FunctionCompiler();
        }

        public IFunction CompileFunction(string language, string functionFileName, string moduleName, string functionHandler)
        {
            var environmentPath = CreateEnvironmentPath(basePath, language, functionFileName);
            var binaryPath = compiler.Compile(environmentPath);

            return new CompiledFunction(moduleName, functionHandler, binaryPath);
        }

        public static string CreateEnvironmentPath(string basePath, string language, string functionFileName)
        {
            var environmentPath = Path.Combine(basePath, language, functionFileName);
            CreateDirectory(environmentPath);

            var functionFiles = Directory.EnumerateFiles(".", $"{functionFileName}.{language}*");
            CopyFunctionsFiles(functionFiles, environmentPath);

            return environmentPath;
        }

        private static void CreateDirectory(string directory)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, recursive: true);
            Directory.CreateDirectory(directory);
        }

        public static void CopyFunctionsFiles(IEnumerable<string> files, string destination)
        {
            foreach (var file in files)
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var extension = Path.GetExtension(file);
                var newName = "project" + extension;
                File.Copy(file, Path.Combine(destination, newName));
            }
        }
    }
}
