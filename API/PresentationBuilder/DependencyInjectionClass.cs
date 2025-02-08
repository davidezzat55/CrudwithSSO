using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Threading.RateLimiting;

namespace API.PresentationBuilder
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();


            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowedOrigins",
                                  c =>
                                  {
                                      c.WithOrigins(config.GetSection("Cors:AllowedOrigins").Get<List<string>>().ToArray())
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });
            AddRateLimate(services, config);

            return services;
        }

        private static void AddRateLimate(IServiceCollection services, IConfiguration config)
        {
            var rateLimitingConfig = config.GetSection("RateLimiting");

            int permitLimit = rateLimitingConfig.GetValue<int>("PermitLimit");
            int windowInMinutes = rateLimitingConfig.GetValue<int>("WindowInMinutes");
            int queueLimit = rateLimitingConfig.GetValue<int>("QueueLimit");
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("IpAddressLimiter", httpContext =>
                {
                    var clientIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: clientIp,
                        factory: key => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = permitLimit,
                            Window = TimeSpan.FromMinutes(windowInMinutes),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = queueLimit
                        });
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync("{\"error\": \"Too many requests from your IP. Please wait and try again later.\"}");
                };
            });
        }


        public static ILoggingBuilder AddDBLogging(this ILoggingBuilder loggingBuilder, IConfiguration config)
        {
            var logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(config)
                            .WriteTo.MSSqlServer(config.GetConnectionString("DefaultConnection"),
                                        new MSSqlServerSinkOptions()
                                        {
                                            AutoCreateSqlDatabase = false,
                                            AutoCreateSqlTable = true,
                                            TableName = "Logs"
                                        })
                            .CreateLogger();

            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);

            return loggingBuilder;
        }


    }
}
