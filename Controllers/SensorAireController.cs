using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Airepuro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SensorAireController : ControllerBase
{
    private readonly ILogger<SensorAireController> _logger;
    private readonly SensorAireServices _sensorAireService;

    public SensorAireController(ILogger<SensorAireController> logger, SensorAireServices sensorAireServices)
    {
        _logger = logger;
        _sensorAireService = sensorAireServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetSensorAires()
    {
        var SensorAire = await _sensorAireService.GetAsync();
        return Ok(SensorAire);
    }
}