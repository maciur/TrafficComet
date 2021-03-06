﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using TrafficComet.Abstracts.Writers;
using TrafficComet.Core;
using TrafficComet.WebApiTest.Mocks;

namespace TrafficComet.WebApiTest
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
			services.AddMvc();
			services.AddTransient<ITrafficLogWriter, MockTrafficLogWriter>();
			services.AddTrafficComet(Configuration, true);
			services.Configure<GzipCompressionProviderOptions>((opts) => opts.Level = CompressionLevel.Optimal);
			services.AddResponseCompression((opts) =>
			{
				opts.EnableForHttps = true;
				opts.MimeTypes = new[]
					{
						"text/plain",
						"text/css",
						"application/javascript",
						"text/html",
						"application/xml",
						"text/xml",
						"application/json",
						"text/json",
						"image/svg+xml"
					};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseTrafficComet();
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseResponseCompression();
			app.UseMvc();
		}
	}
}