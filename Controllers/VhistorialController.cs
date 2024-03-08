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
        public VhistorialController(ILogger<VhistorialController> logger, VhistorialServices vHistorialServices)
        {
            _logger = logger;
            _VhistorialServices = vHistorialServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetVhistorial()
        {
            var Vhistorial = await _VhistorialServices.GetAsync();
            return Ok(Vhistorial);
        }

        [HttpPost]
        public async Task<IActionResult> InsertVhistorial([FromBody] Vhistorial vhistorialToInsert)
        {
            if (vhistorialToInsert == null)
                return BadRequest();
            if (vhistorialToInsert.Titulo == string.Empty)
                ModelState.AddModelError("Titulo del registro", "Ingrese algun titulo, no deje vacio.");
            if (vhistorialToInsert.Fecha == string.Empty)
                ModelState.AddModelError("Fecha del registro", "Escriba la fecha correspondiente, dia, mes y año. ejemplo preferible:'20/12/2024'.");
            if (vhistorialToInsert.Hora == string.Empty)
                ModelState.AddModelError("Hora del registro", "Ingrese una hora estimada; ejemplo: 6:00 p.m");
           
            if(vhistorialToInsert.SensorAire == string.Empty && vhistorialToInsert.SensorAire.ToString().Length == 24)
                ModelState.AddModelError("Sensor de aire", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");
            if (vhistorialToInsert.SensorTemperatura == string.Empty && vhistorialToInsert.SensorTemperatura.ToString().Length == 24)
                ModelState.AddModelError("Sensor de temperatura", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");
            if (vhistorialToInsert.Ventilador == string.Empty && vhistorialToInsert.Ventilador.ToString().Length == 24)
                ModelState.AddModelError("Ventilador", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");

            await _VhistorialServices.InsertVhistorial(vhistorialToInsert);
            return Created("Created", true);
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
            if (VhistorialToUpdate.SensorAire == string.Empty && VhistorialToUpdate.SensorAire.ToString().Length == 24)
                ModelState.AddModelError("Sensor de aire", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");
            if (VhistorialToUpdate.SensorTemperatura == string.Empty && VhistorialToUpdate.SensorTemperatura.ToString().Length == 24)
                ModelState.AddModelError("Sensor de temperatura", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");
            if (VhistorialToUpdate.Ventilador == string.Empty && VhistorialToUpdate.Ventilador.ToString().Length == 24)
                ModelState.AddModelError("Ventilador", "Ingrese un id de 24 caracteres no mas no menos; ejemplo: '65e0d6b9730bfdf77944a477'");

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
