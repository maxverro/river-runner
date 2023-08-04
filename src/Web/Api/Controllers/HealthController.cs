using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace RiverRunner.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DebugController : ControllerBase
{
    private readonly ILogger<DebugController> _logger;

    public DebugController(ILogger<DebugController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns the name and assembly version of the application
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult Get()
    {
        return Ok(new
        {
            Name = nameof(RiverRunner),
            Version = Assembly.GetEntryAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion
        });
    }
}
