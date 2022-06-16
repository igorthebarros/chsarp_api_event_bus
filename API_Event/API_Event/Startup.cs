using API_Event.Repositories;
using API_Event.Repositories.Interfaces;
using Microsoft.OpenApi.Models;

namespace API_Event
{
    public class Startup
    {
        public IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureService(IServiceCollection services)
        {
            services.AddMvc();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            //Swagger
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Event_Bus",
                    Version = "v1"
                });
            });

            //DI
            services.AddScoped<IMemoryCacheRepository, MemoryCacheRepository>();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapControllers();
        }
    }
}
