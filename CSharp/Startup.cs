using Accessors;
using Accessors.Context;
using Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CSharp
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

			services.AddSwaggerGen();

			// Dependencies (Managers/Accessors)
			services.AddTransient<IInventoryManager, InventoryManager>();
			services.AddTransient<IInventoryAccessor, InventoryAccessor>();

			// DBs
			var inventoryConnectionString = Configuration.GetConnectionString(nameof(InventoryDbContext));
			services
				.AddDbContext<InventoryDbContext>(options => options.UseSqlServer(inventoryConnectionString, sqlOptions => sqlOptions.EnableRetryOnFailure()));

			// Automapper
			services.AddAutoMapper(
				typeof(Accessors.Mapping.OrderMappingProfile).Assembly,
				typeof(Managers.Mapping.OrderMappingProfile).Assembly);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory API V1");
				c.RoutePrefix = string.Empty;
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
