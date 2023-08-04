using System;
using System.Collections.Generic;

namespace RiverRunner.WebApi.HealthCheck
{
    /// <summary>
    /// Healthcheck response model
    /// </summary>
    public class HealthCheckReponse
    {
        /// <summary>
        /// The global health check response status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The response of each components
        /// </summary>
        public IEnumerable<ComponentCheckResponse> HealthChecks { get; set; }

        /// <summary>
        /// The duration of the health check
        /// </summary>
        public string HealthCheckDuration { get; set; }
    }
}