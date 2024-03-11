using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VhistorialController : ControllerBase
    {
        private readonly ILogger<VhistorialController> _logger;
        private readonly VhistorialServices _VhistorialServices;
        private readonly SensorAireServices _sensorAireServices; // Servicio para manejar los sensores de aire
        private readonly SensorTemperaturaService _sensorTemperaturaServices; // Servicio para manejar los sensores de temperatura
        private readonly VentiladorServices _ventiladorServices; // Servicio para manejar los ventiladores
        public VhistorialController(ILogger<VhistorialController> logger, VhistorialServices vHistorialServices, SensorAireServices sensorAireServices, SensorTemperaturaService sensorTemperaturaService, VentiladorServices ventiladorServices)
        {
            _logger = logger;
            _VhistorialServices = vHistorialServices;
            _sensorAireServices = sensorAireServices;
            _sensorTemperaturaServices = sensorTemperaturaService;
            _ventiladorServices = ventiladorServices;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetVhistorial()
        {
            try
            {
                var vhistorial = await _VhistorialServices.GetAsync();
                return Ok(vhistorial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertVhistorial([FromBody] Vhistorial vhistorialToInsert)
        {
            try
            {
                if (vhistorialToInsert == null)
                    return BadRequest("El producto no puede ser null");
                if (vhistorialToInsert.Titulo == string.Empty)
                    return BadRequest("El titulo del registro no puede estar vacío");
                if (vhistorialToInsert.Fecha == string.Empty)
                    ModelState.AddModelError("Fecha del registro", "Escriba la fecha correspondiente, dia, mes y año. ejemplo preferible:'20/12/2024'.");
                if (vhistorialToInsert.Hora == string.Empty)
                    ModelState.AddModelError("Hora del registro", "Ingrese una hora estimada; ejemplo: 6:00 p.m");

                if (!string.IsNullOrEmpty(vhistorialToInsert.SensorAireId))
                {
                    var sensoraire = await _sensorAireServices.GetSensorAireById(vhistorialToInsert.SensorAireId);
                    if (sensoraire == null)
                        return BadRequest("El sensor de aire no existe");
                }
                if (!string.IsNullOrEmpty(vhistorialToInsert.SensorTemperaturaId))
                {
                    var sensortemperatura = await _sensorTemperaturaServices.GetSensorTemperaturaById(vhistorialToInsert.SensorTemperaturaId);
                    if (sensortemperatura == null)
                        return BadRequest("El sensor de temperatura no existe");
                }
                if (!string.IsNullOrEmpty(vhistorialToInsert.VentiladorId))
                {
                    var ventilador = await _ventiladorServices.GetVentiladorById(vhistorialToInsert.VentiladorId);
                    if (ventilador == null)
                        return BadRequest("El ventilador no existe");
                }


                await _VhistorialServices.InsertVhistorial(vhistorialToInsert);

                // Obtiene el nombre del proveedor después de insertar el producto
                vhistorialToInsert = await _VhistorialServices.GetVhistorialById(vhistorialToInsert.Id);

                return Created("Created", vhistorialToInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteVhistorial(string idToDelete)
        {
            if (idToDelete == null)
                return BadRequest();
            if (idToDelete == string.Empty && idToDelete.Length == 24)
                ModelState.AddModelError("Id", "No debe dejar el id vacio. También ten en cuenta que el id debe contener 24 caracteres exactos.");

            await _VhistorialServices.DeleteVhistorial(idToDelete);
            return Ok();
        }

        [HttpPut("VhistorialToUpdate")]
        public async Task<IActionResult> UpdateVhistorial(Vhistorial VhistorialToUpdate)
        {
            if (VhistorialToUpdate == null)
                return BadRequest();
            if (VhistorialToUpdate.Titulo == string.Empty)
                ModelState.AddModelError("Titulo del registro", "Ingrese algun titulo, no deje vacio.");
            if (VhistorialToUpdate.Fecha == string.Empty)
                ModelState.AddModelError("Fecha del registro", "Escriba la fecha correspondiente, dia, mes y año. ejemplo preferible:'20/12/2024'.");
            if (VhistorialToUpdate.Hora == string.Empty)
                ModelState.AddModelError("Hora del registro", "Ingrese una hora estimada; ejemplo: 6:00 p.m");
          
            await _VhistorialServices.UpdateVhistorial(VhistorialToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetVentiladorById(string idToSearch)
        {
            var ventiladores = await _VhistorialServices.GetVhistorialById(idToSearch);
            return Ok(ventiladores);
        }
    }
}
