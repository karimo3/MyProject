﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyProject
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // this is called Dependency Injection. it is neccessary for the project to run properly

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) //there is IsDevelopment, IsStaging, IsProduction
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            //app.UseDefaultFiles(); get rid of this
            app.UseStaticFiles(); //only serves files in wwwroot directory
            app.UseMvc( routes =>
            {
                routes.MapRoute(
                    "Default", 
                    "{controller}/{action}/{id}", 
                    new { controller = "App", action = "Index", id = "" }
                );
            });
                                  
        }
    }
}
