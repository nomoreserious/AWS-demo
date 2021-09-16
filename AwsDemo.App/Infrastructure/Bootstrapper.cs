using AwsDemo.Bll.Contracts.Services;
using AwsDemo.Bll.Services;
using AwsDemo.Dal.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AwsDemo.App.Infrastructure
{
	public static class Bootstrapper
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<IProductService, ProductService>();

			//services.AddScoped<IProductStorage, InMemoryProductStorage>();
			services.AddScoped<IProductStorage, DynamoDbProductStorage>();
		}

		public static void AddServiceOptions(this IServiceCollection services, IConfiguration configuration)
		{
#if DEBUG
			services.Configure<Dal.AwsOptions>(o =>
			{
				o.AccessKey = configuration["Aws:AccessKey"];
				o.SecretKey = configuration["Aws:SecretKey"];
				o.RegionName = configuration["Aws:RegionName"];
			});
#else
			services.Configure<Dal.AwsOptions>(o =>
			{
				o.AccessKey = System.Environment.GetEnvironmentVariable("AWS_ACCESS_KEY");
				o.SecretKey = System.Environment.GetEnvironmentVariable("AWS_SECRET_KEY");
				o.RegionName = System.Environment.GetEnvironmentVariable("AWS_REGION_NAME");
			});
#endif
			services.AddSingleton(sp => sp.GetService<IOptions<Dal.AwsOptions>>().Value);
		}
	}
}
