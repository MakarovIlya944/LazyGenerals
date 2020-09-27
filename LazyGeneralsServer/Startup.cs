using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using LazyGeneralsServer.Models.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using LazyGeneralsServer.Models;
using Serilog;

namespace LazyGeneralsServer
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

            services.AddLogging();
            services.AddSwaggerGen();

            services.Configure<MongoDBOptions>(
                Configuration.GetSection(nameof(MongoDBOptions)));

            services.AddSingleton<IMongoDBOptions>(sp =>
                sp.GetRequiredService<IOptions<MongoDBOptions>>().Value);

            services.AddTransient<IServerContext, ServerContext>();
            //    services.AddSingleton<RedisJobFetchingService>();
            //    services.AddSingleton<IJobFetchingService<IProcessingData>, RedisJobFetchingService>
            //        (sp => sp.GetRequiredService<RedisJobFetchingService>());
            //    services.AddSingleton<IStoppableService, RedisJobFetchingService>
            //        (sp => sp.GetRequiredService<RedisJobFetchingService>());

            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetValue<string>("REDIS_URI")));
            //services.AddSingleton<IRedisQueues, RedisQueues>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lazy generals server API V1");
                c.EnableDeepLinking();
            });

        }
    }
}
