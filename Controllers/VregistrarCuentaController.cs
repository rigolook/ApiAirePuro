using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VregistrarCuentaController : ControllerBase
    {
        private readonly ILogger<VregistrarCuentaController> _logger;
        private readonly VregistrarCuentaServices _VregistrarCuentaServices;
        private readonly UsuarioServices _UsuarioServices;
        public VregistrarCuentaController(ILogger<VregistrarCuentaController> logger, VregistrarCuentaServices vregistrarCuentaServices, UsuarioServices usuarioServices)
        {
            _logger = logger;
            _VregistrarCuentaServices = vregistrarCuentaServices;
            _UsuarioServices = usuarioServices;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetVmonitoreo()
        {
            try
            {
                var vmonitoreo = await _VregistrarCuentaServices.GetAsync();
                return Ok(vmonitoreo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertVmonitoreo([FromBody] VregistrarCuenta VregistrarCuentaToInsert)
        {
            try
            {
                if (VregistrarCuentaToInsert == null)
                    return BadRequest("No puede ser null");
                if (VregistrarCuentaToInsert.Titulo == string.Empty)
                    return BadRequest("El titulo de la cuenta no puede estar vacío");
                if (VregistrarCuentaToInsert.Mensaje == string.Empty)
                    return BadRequest("El mensaje no puede estar vacío");

                if (!string.IsNullOrEmpty(VregistrarCuentaToInsert.UsuarioId))
                {
                    var sensoraire = await _UsuarioServices.GetUsuarioById(VregistrarCuentaToInsert.UsuarioId);
                    if (sensoraire == null)
                        return BadRequest("No existe el usuario");
                }

                await _VregistrarCuentaServices.InsertVregistrarCuenta(VregistrarCuentaToInsert);

                // Obtiene el nombre del proveedor después de insertar el producto
                VregistrarCuentaToInsert = await _VregistrarCuentaServices.GetVregistrarCuentaById(VregistrarCuentaToInsert.Id);

                return Created("Created", VregistrarCuentaToInsert);
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

            await _VregistrarCuentaServices.DeleteVregistrarCuenta(idToDelete);
            return Ok();
        }

        [HttpPut("VmonitoreoToUpdate")]
        public async Task<IActionResult> UpdateVmonitoreo(VregistrarCuenta VmonitoreoToUpdate)
        {
            if (VmonitoreoToUpdate == null)
                return BadRequest();
            if (VmonitoreoToUpdate.Titulo == string.Empty)
                ModelState.AddModelError("Titulo del registro", "Ingrese algun titulo, no deje vacio.");

            await _VregistrarCuentaServices.UpdateVregistrarCuenta(VmonitoreoToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetVmonitoreoById(string idToSearch)
        {
            var ventiladores = await _VregistrarCuentaServices.GetVregistrarCuentaById(idToSearch);
            return Ok(ventiladores);
        }
    }
}
