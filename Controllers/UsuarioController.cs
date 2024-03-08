using Airepuro.Api.Models;
using Airepuro.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airepuro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioServices _UsuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, UsuarioServices UsuarioService)
        {
            _logger = logger;
            _UsuarioService = UsuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensorAires()
        {
            var SensorAire = await _UsuarioService.GetAsync();
            return Ok(SensorAire);
        }

        [HttpPost]
        public async Task<IActionResult> InsertUsuario([FromBody] Usuario UsuarioToInsert)
        {
            if (UsuarioToInsert == null)
                return BadRequest();
            if (UsuarioToInsert.Nombre == string.Empty)
                ModelState.AddModelError("Nombre de usuario","Introduzca algun nombre, porfavor.");
            if (UsuarioToInsert.Contrasena == string.Empty)
                ModelState.AddModelError("Contraseña de usuario", "Ingrese una contraseña validad.");
            if (UsuarioToInsert.Numero != null)
                ModelState.AddModelError("Número telefonico de usuario", "Ingrese algun número de telefono");

            await _UsuarioService.InsertUsuario(UsuarioToInsert);

            return Created("Created", true);
        }

        [HttpDelete("ID")]
        public async Task<IActionResult> DeleteUsuario(string idToDelete)
        {
            if (idToDelete == null)
                return BadRequest();
            if (idToDelete == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio");

            await _UsuarioService.DeleteUsuario(idToDelete);

            return Ok();
        }

        [HttpPut("UsuarioToUpdate")]
        public async Task<IActionResult> UpdateUsuario(Usuario usuarioToUpdate)
        {
            if (usuarioToUpdate == null)
                return BadRequest();
            if (usuarioToUpdate.Id == string.Empty)
                ModelState.AddModelError("Id", "No debe dejar el id vacio");
            if (usuarioToUpdate.Nombre == string.Empty)
                ModelState.AddModelError("Nombre de usuario", "Introduzca algun nombre, porfavor.");
            if (usuarioToUpdate.Contrasena == string.Empty)
                ModelState.AddModelError("Contraseña de usuario", "Ingrese una contraseña validad.");
            if (usuarioToUpdate.Numero != null)
                ModelState.AddModelError("Número telefonico de usuario", "Ingrese algun número de telefono");

            await _UsuarioService.UpdateUsuario(usuarioToUpdate);

            return Ok();
        }

        [HttpGet("ID")]
        public async Task<IActionResult> GetUsuarioById(string idToSearch)
        {
            var drivers = await _UsuarioService.GetUsuarioById(idToSearch);
            return Ok(drivers);
        }
    }
}
