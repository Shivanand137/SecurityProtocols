using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using Todo.Entities;
using Todo.Services;
using Todo.Services.Implementation;

namespace Todo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register CORS
            services.AddCors();

            // Register HTTPClient
            services.AddHttpClient(HttpClientSettings.HttpClientName, config =>
            {
                config.BaseAddress = new Uri(HttpClientSettings.BaseUrl);
                config.DefaultRequestHeaders.Add("Accept", HttpClientSettings.MediaType);
            });

            // Register Dependencies
            services.AddTransient<ITodoService, TodoService>();

            // Register Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info()
                {
                    Version = "v1",
                    Title = "Todo API",
                    Description = "A Simple ASP.NET Core API with JWT Implementation!"
                });
            });

            // Register MVC Services
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Enable Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/swagger/v1/swagger.json", "Todo API - V1");
            });


            // Bad practice, but okay for this dummy app.
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
