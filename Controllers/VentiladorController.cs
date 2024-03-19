using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentiladorController : ControllerBase
    {
        private readonly ILogger<VentiladorController> _logger;
        private readonly VentiladorServices _ventiladorService;

        public VentiladorController(ILogger<VentiladorController> logger, VentiladorServices ventiladorServices)
        {
            _logger = logger;
            _ventiladorService = ventiladorServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetVentilador()
        {
            var Ventilador = await _ventiladorService.GetAsync();
            return Ok(Ventilador);
        }

        [HttpPost]
        public async Task<IActionResult> InsertSensorAire([FromBody] Ventilador VentiladorToInsert)
        {
            if (VentiladorToInsert == null)
                return BadRequest();
            if (VentiladorToInsert.RPM != null)
                ModelState.AddModelError("RPM", "Ingrese un estimado de las revoluciones por minuto.");
            if (VentiladorToInsert.Ubicacion == string.Empty)
                ModelState.AddModelError("Ubicación", "Llene la ubicacion del ventilador.");
            if (VentiladorToInsert.Encendido != null)
                ModelState.AddModelError("Estado del ventilador", "¿El ventilador esta encendido o apagado?");

            await _ventiladorService.InsertVentilador(VentiladorToInsert);

            return Created("Created", true);
        }

        [HttpDelete("borrrar/{ID}")]
        public async Task<IActionResult> DeleteVentilador(string ID)
        {
            if (ID == null)
                return BadRequest();

            await _ventiladorService.DeleteVentilador(ID);

            return Ok();
        }

        [HttpPut("VentiladorToUpdate")]
        public async Task<IActionResult> UpdateVentilador(Ventilador VentiladorToUpdate)
        {
            if (VentiladorToUpdate == null)
                return BadRequest();
            if (VentiladorToUpdate.Id == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio.");
            if (VentiladorToUpdate.RPM != null)
                ModelState.AddModelError("RPM", "Ingrese la un estimado de las revoluciones por minuto.");
            if (VentiladorToUpdate.Ubicacion == string.Empty)
                ModelState.AddModelError("Ubicación", "Llene la ubicacion del ventilador.");
            if (VentiladorToUpdate.Encendido != null)
                ModelState.AddModelError("Estado del ventilador", "¿El ventilador esta encendido o apagado?");

            await _ventiladorService.UpdateVentilador(VentiladorToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetVentiladorById(string idToSearch)
        {
            var ventiladores = await _ventiladorService.GetVentiladorById(idToSearch);
            return Ok(ventiladores);
        }

        [HttpPut("VentiladorToUpdateArduino/{id}/{rpm}")]
        public async Task<IActionResult> UpdateVentiladorArduino(string id, string rpm)
        {
            await _ventiladorService.UpdateVentiladorArduino(id, rpm);
            return Ok();
        }
    }
}
