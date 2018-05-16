using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MockApi.Server
{
    public class Startup
    {
		// This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
				var path = context.Request.Path;				
				var bodyAsText = String.Empty;

				if(context.Request.ContentLength.GetValueOrDefault() > 0)
				{
					var buffer = new byte[(int)context.Request.ContentLength];
					await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
					bodyAsText = ASCIIEncoding.ASCII.GetString(buffer);
				}

				var handler = Handlers.HandlerFactory.GetHandler(path);
				var response = handler.ProcessRequest(path, bodyAsText);				
				await context.Response.WriteAsync(response);
			});
        }
    }

	
}
