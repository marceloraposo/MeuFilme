using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;

namespace MeuFIlmeGateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot(_configuration);
            services.AddSwaggerForOcelot(_configuration);
            services.AddMvc();

            services.AddCors(o => o.AddPolicy("CorsGateway", builder =>
            {
                builder
                    .WithOrigins(new[] { "https://localhost:44397" })
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials().WithExposedHeaders("x-custom-header");
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/gateway");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseStaticFiles();

            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
            });
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsGateway");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("API GATEWAY FUNCIONANDO");
                });
            });

            await app.UseOcelot();
        }
    }
}
