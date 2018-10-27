using Kubeless.Core.Interfaces;
using Kubeless.Functions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;

namespace Kubeless.WebAPI.Controllers
{
    [Route("/")]
    public class RuntimeController : Controller
    {
        private readonly IFunction function;
        private readonly IParameterHandler parameterManager;
        private readonly IInvoker invoker;
        private readonly ILogger logger;

        public RuntimeController(IFunction function, IParameterHandler parameterManager, IInvoker invoker, ILogger<RuntimeController> logger)
        {
            this.function = function;
            this.invoker = invoker;
            this.parameterManager = parameterManager;
            this.logger = logger;
        }

        [AcceptVerbs("GET", "POST", "PUT", "PATCH", "DELETE")]
        public object Execute()
        {
            logger.LogInformation("{0}: Function Started. HTTP Method: {1}, Path: {2}.", DateTime.Now.ToString(), Request.Method, Request.Path);

            try
            {
                (Event @event, Context context) = parameterManager.GetFunctionParameters(Request);

                var cancellationSource = new CancellationTokenSource();

                var output = invoker.Execute(function, cancellationSource, @event, context);

                logger.LogInformation("{0}: Function Executed. HTTP response: {1}.", DateTime.Now.ToString(), 200);
                return output;
            }
            catch (OperationCanceledException exception)
            {
                logger.LogError(exception, "{0}: Function Cancelled. HTTP Response: {1}. Reason: {2}.", DateTime.Now.ToString(), 408, "Timeout");
                return new StatusCodeResult(408);
            }
            catch (Exception exception)
            {
                logger.LogCritical(exception, "{0}: Function Corrupted. HTTP Response: {1}. Reason: {2}.", DateTime.Now.ToString(), 500, exception.Message);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet("/healthz")]
        public IActionResult Health() => Ok();

    }
}