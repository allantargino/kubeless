using Kubeless.Core.Interfaces;
using Kubeless.Core.Tests.Utils;
using Kubeless.Functions;
using System;
using System.Threading;
using Xunit;

namespace Kubeless.Core.Tests
{
    public class TimeoutTests
    {
        [InlineData("cs", "timeout", "module", "handler", 1)]
        [InlineData("cs", "timeout", "module", "handler", 2)]
        [InlineData("cs", "timeout", "module", "handler", 3)]
        [InlineData("cs", "timeout", "module", "handler", 4)]
        [InlineData("cs", "timeout", "module", "handler", 5)]
        [Theory]
        public void RunWithTimeout(string language, string functionFileName, string moduleName, string functionHandler, int timeoutSeconds)
        {
            // Arrange
            int timeout = timeoutSeconds * 1000;
            IInvoker invoker = InvokerFactory.GetFunctionInvoker(language, functionFileName, moduleName, functionHandler, timeout);
            Event @event = new Event();
            Context context = new Context();
            CancellationTokenSource token = new CancellationTokenSource();

            // Act
            Action timeoutAction = () =>
            {
                object result = invoker.Execute(token, @event, context);
            };

            //Assert
            Assert.Throws<OperationCanceledException>(timeoutAction);
        }

        [InlineData("cs", "timeout", "module", "handler", 6)]
        [InlineData("cs", "timeout", "module", "handler", 7)]
        [InlineData("cs", "timeout", "module", "handler", 8)]
        [InlineData("cs", "timeout", "module", "handler", 9)]
        [InlineData("cs", "timeout", "module", "handler", 10)]
        [Theory]
        public void RunWithoutTimeout(string language, string functionFileName, string moduleName, string functionHandler, int timeoutSeconds)
        {
            // Arrange
            int timeout = timeoutSeconds * 1000;
            IInvoker invoker = InvokerFactory.GetFunctionInvoker(language, functionFileName, moduleName, functionHandler, timeout);
            Event @event = new Event();
            Context context = new Context();
            CancellationTokenSource token = new CancellationTokenSource();

            // Act
            object result = invoker.Execute(token, @event, context);

            // Assert
            Assert.Equal("This is a long run!", result.ToString());
        }

    }
}
