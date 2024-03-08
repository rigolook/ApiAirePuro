using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorTemperaturaController : ControllerBase
    {
        private readonly ILogger<SensorTemperaturaController> _logger;
        private readonly SensorTemperaturaService _SensorTemperaturaService;

        public SensorTemperaturaController(ILogger<SensorTemperaturaController> logger, SensorTemperaturaService SensorTemperatura)
        {
            _logger = logger;
            _SensorTemperaturaService = SensorTemperatura;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensorTemperatura()
        {
            var SensorTemperatura = await _SensorTemperaturaService.GetAsync();
            return Ok(SensorTemperatura);
        }

        [HttpPost]
        public async Task<IActionResult> InsertSensorTemperatura([FromBody] SensorTemperatura SensorTemperaturaToInsert)
        {
            if (SensorTemperaturaToInsert == null)
                return BadRequest();
            if (SensorTemperaturaToInsert.Temperatura != 0)
                ModelState.AddModelError("Temperatura", "Proporcione la temperatura.");
            if (SensorTemperaturaToInsert.Ubicacion == string.Empty)
                ModelState.AddModelError("Ubicación", "Llene la ubicacion del sensor.");
            if (SensorTemperaturaToInsert.Humedad == string.Empty)
                ModelState.AddModelError("Humedad", "Coloque la humedad detectada.");

            await _SensorTemperaturaService.InsertSensorTemperatura(SensorTemperaturaToInsert);

            return Created("Created", true);
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteSensorTemperatura(string idToDelete)
        {
            if (idToDelete == null)
                return BadRequest();
            if (idToDelete == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio");

            await _SensorTemperaturaService.DeleteSensorTemperatura(idToDelete);

            return Ok();
        }

        [HttpPut("SensorTemperaturaToUpdate")]
        public async Task<IActionResult> UpdateSensorTemperatura(SensorTemperatura SensorAireToUpdate)
        {
            if (SensorAireToUpdate == null)
                return BadRequest();
            if (SensorAireToUpdate.Id == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio");
            if (SensorAireToUpdate.Temperatura != 0)
                ModelState.AddModelError("Temperatura", "Proporcione la temperatura.");
            if (SensorAireToUpdate.Ubicacion == string.Empty)
                ModelState.AddModelError("Ubicación", "Llene la ubicacion del sensor");
            if (SensorAireToUpdate.Humedad == string.Empty)
                ModelState.AddModelError("Humedad", "Coloque la humedad detectada.");

            await _SensorTemperaturaService.UpdateSensorTemperatura(SensorAireToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetSensorTemperaturaById(string idToSearch)
        {
            var drivers = await _SensorTemperaturaService.GetSensorTemperaturaById(idToSearch);
            return Ok(drivers);
        }
    }
}
