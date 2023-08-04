namespace RiverRunner.WebApi.HealthCheck
{
    /// <summary>
    /// Component check response model
    /// </summary>
    public class ComponentCheckResponse
    {
        /// <summary>
        /// Component Status
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Component Name
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// Component Description
        /// </summary>
        public string Description { get; set; }
    }
}