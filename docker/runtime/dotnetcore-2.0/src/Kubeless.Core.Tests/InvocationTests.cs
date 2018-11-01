﻿using Kubeless.Core.Handlers;
using Kubeless.Core.Interfaces;
using Kubeless.Core.Invokers;
using Kubeless.Core.Tests.Utils;
using Kubeless.Functions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using Xunit;
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Kubeless.Core.Tests
{
    public class InvocationTests
    {
        private const string BASE_PATH = "./Functions";
        private const string PUBLISH_PATH = "publish/";
        private const string PACKAGES_PATH = "packages/";

        [InlineData("cs", "nodependency", "module", "handler")]
        [Theory]
        public void BuildWithoutDependency(string language, string functionFileName, string moduleName, string functionHandler)
        {
            // Arrange
            int timeout = 180;
            string packagesPath = Path.Combine(BASE_PATH, PACKAGES_PATH);
            HttpRequest request = new DefaultHttpContext().Request;
            FunctionFactory factory = new FunctionFactory(BASE_PATH);
            IFunction function = factory.CompileFunction(language, functionFileName, moduleName, functionHandler);
            IInvoker invoker = new CompiledFunctionInvoker(function, timeout, packagesPath);
            Event @event = new Event();
            Context context = new Context();
            CancellationTokenSource token = new CancellationTokenSource();

            // Act
            object result = invoker.Execute(token, @event, context);

            // Assert
        }

        //[InlineData("dependency-json")] //TODO: Run both tests in parallel
        [InlineData("dependency-yaml")]
        [Theory]
        public void BuildWithDependency(string functionFileName)
        {
            //// Compile
            //var environment = EnvironmentManager.CreateEnvironment(BASE_PATH, functionFileName);

            //var functionFile = environment.FunctionFile;
            //var projectFile = environment.ProjectFile;
            //var assemblyFile = environment.AssemblyFile;

            //var restorer = new FunctionCompiler(environment);
            //restorer.CopyAndRestore();

            //var compiler = new DefaultCompiler(new DefaultParser(), new WithDependencyReferencesManager());

            //compiler.Compile(function);

            // Invoke
            //var invoker = new DefaultInvoker();

            //var args = WebManager.GetHttpRequest();

            //object result = invoker.Execute(function, args);
        }

        #region Timeout

        [InlineData("timeout")]
        [Theory]
        public void RunWithTimeout(string functionFileName)
        {
            // Compile
            IFunction function = GetCompiledFunctionWithDepedencies(functionFileName);

            // Invoke
            var timeoutTriggered = false;
            try
            {
                var timeout = 4 * 1000; // Limits to 4 seconds
                object result = ExecuteCompiledFunction(function, timeout); // Takes 5 seconds
            }
            catch (Exception ex)
            {
                Assert.IsType<OperationCanceledException>(ex);
                timeoutTriggered = true;
            }

            Assert.True(timeoutTriggered);
        }

        [InlineData("timeout")]
        [Theory]
        public void RunWithoutTimeout(string functionFileName)
        {
            // Compile
            IFunction function = GetCompiledFunctionWithDepedencies(functionFileName);

            // Invoke
            var timeout = 6 * 1000; // Limits to 6 seconds
            object result = ExecuteCompiledFunction(function, timeout); // Takes 5 seconds

            Assert.Equal("This is a long run!", result.ToString());
        }

        #endregion

        private static object ExecuteCompiledFunction(IFunction function, int timeout = 180 * 1000)
        {
            //// Invoke
            //var invoker = new CompiledFunctionInvoker(function, timeout, null);

            //var cancellationSource = new CancellationTokenSource();

            //var request = GetHttpRequest();
            //(Event _event, Context _context) = new DefaultParameterHandler(null).GetFunctionParameters(request);

            //return invoker.Execute(cancellationSource, _event, _context);
            return null;
        }

        private static IFunction GetCompiledFunctionWithDepedencies(string functionFileName)
        {
            //// Creates Environment
            //EnvironmentManager environment = null;

            //var functionFile = environment.FunctionFile;
            //var projectFile = environment.ProjectFile;
            //var assemblyFile = environment.AssemblyFile;

            //// Restore Dependencies
            //var restorer = new FunctionCompiler(environment);
            //restorer.CopyAndRestore();

            // Compile
            //var compiler = new DefaultCompiler(new DefaultParser(), new WithDependencyReferencesManager());
            //var function = FunctionCreator.CreateFunction(functionFile, projectFile);
            //compiler.Compile(function);

            return null;
        }
    }
}
