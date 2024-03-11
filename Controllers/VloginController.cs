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
            var Ventilador = await _vloginService.GetAsync();
            return Ok(Ventilador);
        }

        [HttpPost]
        public async Task<IActionResult> InsertVlogin([FromBody] Vlogin vloginToInsert)
        {
            if (vloginToInsert == null)
                return BadRequest();
            if (vloginToInsert.Titulo != null)
                ModelState.AddModelError("Titulo", "Ingrese un titulo para el registro.");
            if (vloginToInsert.Nombre == string.Empty)
                ModelState.AddModelError("Nombre de usuario", "Llene el campo de nombre.");
            if (vloginToInsert.Contrasena != null)
                ModelState.AddModelError("Contraseña", "Se recomienda colocar una contraseña");

            await _vloginService.InsertVlogin(vloginToInsert);

            return Created("Created", true);
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
            if (vlonginToUpdate.Nombre == string.Empty)
                ModelState.AddModelError("Nombre de usuario", "Llene el campo de nombre.");
            if (vlonginToUpdate.Contrasena != null)
                ModelState.AddModelError("Contraseña", "Se recomienda colocar una contraseña");

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
