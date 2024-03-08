using Airepuro.Api.Models;
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

    [HttpPost]
    public async Task<IActionResult> InsertSensorAire([FromBody] SensorAire SensorAireToInsert)
    {
        if (SensorAireToInsert == null)
            return BadRequest();
        if (SensorAireToInsert.GasDetectado != null)
            ModelState.AddModelError("Gas detectado", "Llene el tipo de gas o si hay oxigeno");
        if (SensorAireToInsert.Ubicacion == string.Empty)
            ModelState.AddModelError("Ubicación", "Llene la ubicacion del sensor");
        if (SensorAireToInsert.NombreGas == string.Empty)
            ModelState.AddModelError("Nombre de gas", "Coloque un nombre o apodo del tipo de gas");

        await _sensorAireService.InsertSensorAire(SensorAireToInsert);

        return Created("Created", true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteSensorAire(string idToDelete)
    {
        if (idToDelete == null)
            return BadRequest();
        if (idToDelete == string.Empty)
            ModelState.AddModelError("Id", "No debe dejar el id vacio");

        await _sensorAireService.DeleteSensorAire(idToDelete);

        return Ok();
    }

    [HttpPut("SensorAireToUpdate")]
    public async Task<IActionResult> UpdateSensorAire(SensorAire SensorAireToUpdate)
    {
        if (SensorAireToUpdate == null)
            return BadRequest();
        if (SensorAireToUpdate.Id == string.Empty)
            ModelState.AddModelError("Id", "No debe dejar el id vacio");
        if (SensorAireToUpdate.GasDetectado != null)
            ModelState.AddModelError("Gas detectado", "Llene el tipo de gas o si hay oxigeno");
        if (SensorAireToUpdate.Ubicacion == string.Empty)
            ModelState.AddModelError("Ubicación", "Llene la ubicacion del sensor");
        if (SensorAireToUpdate.NombreGas == string.Empty)
            ModelState.AddModelError("Nombre de gas", "Coloque un nombre o apodo del tipo de gas");

        await _sensorAireService.UpdateSensorAire(SensorAireToUpdate);

        return Ok();
    }

    [HttpGet("ID")]
    public async Task<IActionResult> GetSensorAireById(string idToSearch)
    {
        var drivers = await _sensorAireService.GetSensorAireById(idToSearch);
        return Ok(drivers);
    }
}