
using ApiGateway.Settings;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<MeterComponent>();
            builder.Services.AddAuthorization();

            builder.Services.AddReverseProxy()
            .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            string? originPath = builder.Configuration.GetValue<string>("CorsPolicy:OriginPath")
                ?? "http://localhost:3000";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("EcommerceCors", builder =>
                {
                    builder.WithOrigins(originPath)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            builder.Logging.AddOpenTelemetry(options =>
            {
                options
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(TraceSetting.RESOURCE))
                    .AddConsoleExporter();
            });
            builder.Services.AddOpenTelemetry()
                  .ConfigureResource(resource => resource.AddService(TraceSetting.RESOURCE))
                  .WithTracing(tracing => tracing
                      .AddAspNetCoreInstrumentation()
                      .AddConsoleExporter()
                      .SetSampler(new TraceIdRatioBasedSampler(0.1)))
                  .WithMetrics(metrics => metrics
                      .AddAspNetCoreInstrumentation()
                      .AddMeter("ECommerceGateway")
                      .AddConsoleExporter());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //this is intentional because still don't know what to put here
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("EcommerceCors");

            app.MapReverseProxy();

            app.Run();
        }
    }
}
