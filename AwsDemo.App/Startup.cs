using System;
using Amazon.XRay.Recorder.Handlers.AwsSdk;
using AwsDemo.App.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AwsDemo.App
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
			services.AddControllers();
			services.AddHealthChecks();
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddOptions();
			services.AddServiceOptions(Configuration);
			services.AddServices();

			AWSSDKHandler.RegisterXRayForAllServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseAuthorization();
			app.UseXRay(nameof(AwsDemo), Configuration);
			
			app.UseEndpoints(
				endpoints =>
				{
					endpoints.MapControllers();
					endpoints.MapHealthChecks("/health");
				});
		}
	}
}