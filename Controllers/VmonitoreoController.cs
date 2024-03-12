using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VmonitoreoController : ControllerBase
    {
        private readonly ILogger<VmonitoreoController> _logger;
        private readonly VmonitoreoServices _VmonitoreoServices;
        private readonly SensorAireServices _sensorAireServices; // Servicio para manejar los sensores de aire
        private readonly SensorTemperaturaService _sensorTemperaturaServices; // Servicio para manejar los sensores de temperatura
        private readonly VentiladorServices _ventiladorServices; // Servicio para manejar los ventiladores
        public VmonitoreoController(ILogger<VmonitoreoController> logger, VmonitoreoServices vmonitoreoServices, SensorAireServices sensorAireServices, SensorTemperaturaService sensorTemperaturaService, VentiladorServices ventiladorServices)
        {
            _logger = logger;
            _VmonitoreoServices = vmonitoreoServices;
            _sensorAireServices = sensorAireServices;
            _sensorTemperaturaServices = sensorTemperaturaService;
            _ventiladorServices = ventiladorServices;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetVmonitoreo()
        {
            try
            {
                var vmonitoreo = await _VmonitoreoServices.GetAsync();
                return Ok(vmonitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertVmonitoreo([FromBody] Vmonitoreo VmonitoreoToInsert)
        {
            try
            {
                if (VmonitoreoToInsert == null)
                    return BadRequest("No puede ser null");
                if (VmonitoreoToInsert.Titulo == string.Empty)
                    return BadRequest("El titulo del registro no puede estar vacío");

                if (!string.IsNullOrEmpty(VmonitoreoToInsert.SensorAireId))
                {
                    var sensoraire = await _sensorAireServices.GetSensorAireById(VmonitoreoToInsert.SensorAireId);
                    if (sensoraire == null)
                        return BadRequest("El sensor de aire no existe");
                }
                if (!string.IsNullOrEmpty(VmonitoreoToInsert.SensorTemperaturaId))
                {
                    var sensortemperatura = await _sensorTemperaturaServices.GetSensorTemperaturaById(VmonitoreoToInsert.SensorTemperaturaId);
                    if (sensortemperatura == null)
                        return BadRequest("El sensor de temperatura no existe");
                }
                if (!string.IsNullOrEmpty(VmonitoreoToInsert.VentiladorId))
                {
                    var ventilador = await _ventiladorServices.GetVentiladorById(VmonitoreoToInsert.VentiladorId);
                    if (ventilador == null)
                        return BadRequest("El ventilador no existe");
                }


                await _VmonitoreoServices.InsertVmonitoreo(VmonitoreoToInsert);

                // Obtiene el nombre del proveedor después de insertar el producto
                VmonitoreoToInsert = await _VmonitoreoServices.GetVmonitoreoById(VmonitoreoToInsert.Id);

                return Created("Created", VmonitoreoToInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteVmonitoreo(string idToDelete)
        {
            if (idToDelete == null)
                return BadRequest();
            if (idToDelete == string.Empty && idToDelete.Length == 24)
                ModelState.AddModelError("Id", "No debe dejar el id vacio. También ten en cuenta que el id debe contener 24 caracteres exactos.");

            await _VmonitoreoServices.DeleteVmonitoreo(idToDelete);
            return Ok();
        }

        [HttpPut("VmonitoreoToUpdate")]
        public async Task<IActionResult> UpdateVmonitoreo(Vmonitoreo VmonitoreoToUpdate)
        {
            if (VmonitoreoToUpdate == null)
                return BadRequest();
            if (VmonitoreoToUpdate.Titulo == string.Empty)
                ModelState.AddModelError("Titulo del registro", "Ingrese algun titulo, no deje vacio.");
            
            await _VmonitoreoServices.UpdateVmonitoreo(VmonitoreoToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetVmonitoreoById(string idToSearch)
        {
            var ventiladores = await _VmonitoreoServices.GetVmonitoreoById(idToSearch);
            return Ok(ventiladores);
        }
    }
}
