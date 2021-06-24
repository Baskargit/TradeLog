using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ShareMarket.TradeLog.Business.Implementation;
using ShareMarket.TradeLog.Business.Interface;
using ShareMarket.TradeLog.DataRepository;
using ShareMarket.TradeLog.DataRepository.Implementation;
using ShareMarket.TradeLog.DataRepository.Interface;
using ShareMarket.TradeLog.EntityMapper;

namespace ShareMarket.TradeLog.Api
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "TradeLog API", 
                    Version = "V1.0.0" ,
                    Description = "TradeLog Web API",
                    Contact = new OpenApiContact
                    {
                        Name = "Baskar",
                        Email = "basskar142@gmail.com",
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under No License",
                    }
                });
                
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // DB Connection
            services.AddDbContext<TradeLogDbContext>(options => 
                options.UseMySql(Configuration.GetConnectionString("DevelopmentDB"),
                b => b.MigrationsAssembly("ShareMarket.TradeLog.Api"))
                .UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()))
            );

            // Business DI Services
            services.AddTransient<ICloseTradeBusiness,CloseTradeBusiness>();
            services.AddTransient<IMarketBusiness,MarketBusiness>();
            services.AddTransient<IOpenTradeBusiness,OpenTradeBusiness>();
            services.AddTransient<ISymbolBusiness,SymbolBusiness>();
            services.AddTransient<ISymbolTypeBusiness,SymbolTypeBusiness>();
            services.AddTransient<ITradeResultBusiness,TradeResultBusiness>();
            services.AddTransient<ITradeStatusBusiness,TradeStatusBusiness>();
            services.AddTransient<ITradeTypeBusiness,TradeTypeBusiness>();
            

            // Repository Data DI Services
            services.AddTransient<ICloseTradeRepository,CloseTradeRepository>();
            services.AddTransient<IMarketRepository,MarketRepository>();
            services.AddTransient<IOpenTradeRepository,OpenTradeRepository>();
            services.AddTransient<ISymbolRepository,SymbolRepository>();
            services.AddTransient<ISymbolTypeRepository,SymbolTypeRepository>();
            services.AddTransient<ITradeResultRepository,TradeResultRepository>();
            services.AddTransient<ITradeStatusRepository,TradeStatusRepository>();
            services.AddTransient<ITradeTypeRepository,TradeTypeRepository>();

            
            // Mapper DI Service
            services.AddAutoMapper(
                Assembly.GetAssembly(typeof(TradeLogBaseMappingProfile))
            );

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradeLog API V1.0.0");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "TradeLog - Web Api Documentation";
            });

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
