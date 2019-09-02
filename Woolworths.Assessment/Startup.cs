using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Woolworths.Assessment.Services;
using Woolworths.Assessment.Services.Interfaces;
using Woolworths.Assessment.Services.SortingStrategies;

namespace Woolworths.Assessment
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<AppSettings>();
            services.AddTransient<IProductSortingStrategy, LowSortingStrategy>();
            services.AddTransient<IProductSortingStrategy, HighSortingStrategy>();
            services.AddTransient<IProductSortingStrategy, AscendingSortingStrategy>();
            services.AddTransient<IProductSortingStrategy, DescendingSortingStrategy>();
            services.AddTransient<IProductSortingStrategy, RecommendedSortingStrategy>();
            services.AddTransient<ITrolleyCalculator, TrolleyCalculator>();

            services.AddScoped<IProductSorter, ProductSorter>();

            if (Environment.IsDevelopment())
            {
                services.AddScoped<IWoolworthsResourceClient, MockWoolworthsResourceClient>();
            }
            else
            {
                services.AddScoped<IWoolworthsResourceClient, WoolworthsResourceClient>();
            }

            services.AddScoped<IWoolworthsResourceProvider, WoolworthsResourceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            var isDevelopment = env.IsDevelopment();
            logger.LogDebug($"IsDevelopment={isDevelopment}");
            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
