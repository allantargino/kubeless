using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kubeless.Core.Interfaces;
using Kubeless.WebAPI.Utils;
using Kubeless.Core.Invokers;
using System.IO;
using Kubeless.Core.Handlers;

namespace Kubeless.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var function = FunctionFactory.BuildFunction(Configuration);

            if (!function.IsCompiled())
                throw new FileNotFoundException(nameof(function.FunctionSettings.ModuleName));

            services.AddSingleton<IFunction>(function);

            int timeout = int.Parse(Configuration["FUNC_TIMEOUT"]) * 1000; // seconds

            services.AddSingleton<IInvoker>(new CompiledFunctionInvoker(timeout));
            services.AddSingleton<IParameterHandler>(new DefaultParameterHandler());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            app.UseMvc();
        }
    }
}
