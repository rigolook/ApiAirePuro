using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VloginController : ControllerBase
    {
        private readonly ILogger<VloginController> _logger;
        private readonly VloginServices _vloginService;
        private readonly UsuarioServices _usuarioService;

        public VloginController(ILogger<VloginController> logger,
            VloginServices vloginServices,
            UsuarioServices usuarioService)
        {
            _logger = logger;
            _vloginService = vloginServices;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVlogin()
        {
            try
            {
                var vlogin = await _vloginService.GetAsync();
                return Ok(vlogin);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los productos");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertVlogin([FromBody] Vlogin vloginToInsert)
        {
            try {
                if (vloginToInsert == null)
                    return BadRequest();
                if (vloginToInsert.Titulo != null)
                    ModelState.AddModelError("Titulo", "Ingrese un titulo para el registro.");

                if (!string.IsNullOrEmpty(vloginToInsert.UsuarioId))
                {
                    var sensoraire = await _usuarioService.GetUsuarioById(vloginToInsert.UsuarioId);
                    if (sensoraire == null)
                        return BadRequest("No existe el usuario");
                }

                await _vloginService.InsertVlogin(vloginToInsert);



                return Created("Created", vloginToInsert);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el producto");
                return StatusCode(500, "Error interno del servidor. Por favor, inténtalo de nuevo más tarde.");
            }
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteVlogin(string idToDelete)
        {
            if (idToDelete == null)
                return BadRequest();
            if (idToDelete == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio.");

            await _vloginService.DeleteVlogin(idToDelete);

            return Ok();
        }

        [HttpPut("vlonginToUpdate")]
        public async Task<IActionResult> UpdateVlogin(Vlogin vlonginToUpdate)
        {
            if (vlonginToUpdate == null)
                return BadRequest();
            if (vlonginToUpdate.Id == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio.");
            if (vlonginToUpdate.Titulo != null)
                ModelState.AddModelError("Titulo", "Ingrese un titulo para el registro.");
            await _vloginService.UpdateVlogin(vlonginToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetVloginById(string idToSearch)
        {
            var ventiladores = await _vloginService.GetVloginById(idToSearch);
            return Ok(ventiladores);
        }
    }
}
