using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RiverRunner.WebApi.HealthCheck;

namespace RiverRunner.WebApi.Extensions
{
    /// <summary>
    ///     Configures Custom HealthChecks
    /// </summary>
    public static class HealthCheckConfigurationExtension
    {
        /// <summary>
        ///     Configure les health checks
        /// </summary>
        /// <param name="services"></param>
        public static void AddCustomHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck(
                    "liveness",
                    () => HealthCheckResult.Healthy(),
                    new[] { "k8s-liveness-probe" }
                );

            //Enable when database will be configured
            // .AddDbContextCheck<BrmDbContext>(
            //     "authserver_database",
            //     tags: new[] { "all", "service", "k8s-readiness-probe" }
            // );
        }

        /// <summary>
        ///     Configure les custom healtcheck
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomHealthChecks(this IApplicationBuilder app)
        {
            // Used by K8s liveness probe
            app.UseHealthChecks("/api/v1/status/health", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("k8s-liveness-probe")
            });

            // Used by K8s readiness probe
            app.UseHealthChecks("/api/v1/status/readiness", new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains("k8s-readiness-probe")
            });

            app.UseHealthChecks("/api/v1/status/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var response = new HealthCheckReponse
                    {
                        Status = report.Status.ToString(),
                        HealthChecks = report.Entries.Select(x => new ComponentCheckResponse
                        {
                            Component = x.Key,
                            Status = x.Value.Status.ToString(),
                            Description = x.Value.Description
                        }),
                        HealthCheckDuration = report.TotalDuration.ToString()
                    };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                },
                Predicate = check => check.Tags.Contains("all")
            });
        }
    }
}