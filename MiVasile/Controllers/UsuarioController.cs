using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiVasile.Domain.Models;
using MiVasile.Infrastructure.Business;


namespace MiVasile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            Usuarios_Business usuarios_Business = new Usuarios_Business();

            List<Usuario> usuarios = await usuarios_Business.GetAllAsync();

            Usuario usuarioEncontrado = usuarios.FirstOrDefault(x => x.Correo == login.Correo.Trim() && x.Clave == login.Clave.Trim());//BUSCA EN LA BASE DE DATOS SQL EL USUARIO

            if (usuarioEncontrado == null) return StatusCode(StatusCodes.Status404NotFound, null);

            //Recuperar datos personales del usuario encontrado
            DatosPersonales_Business datosPersonales_Business = new DatosPersonales_Business();
            List<DatosPersonalesXUsuario> datosPersonalesXUsuario = await datosPersonales_Business.GetAllAsync();
            DatosPersonalesXUsuario datosPersonales = datosPersonalesXUsuario.FirstOrDefault(x => x.ID_Usuario == usuarioEncontrado.ID);//BUSCA DATOS PERSONALES X USUARIO

            //Recuperar Rol del usuario encontrado
            RolesXUsuario_Business rolesXUsuario = new RolesXUsuario_Business();
            List<RolxUsuario> rolxUsuarios = await rolesXUsuario.GetAllAsync();
            RolxUsuario RolUsuario = rolxUsuarios.FirstOrDefault(x => x.ID_Usuario == usuarioEncontrado.ID);

            //asignar datos recuperado a la sesion 
            Sesion sesionActual = new Sesion();
            sesionActual.ID_Usuario = usuarioEncontrado.ID;
            sesionActual.Correo = usuarioEncontrado.Correo;
            sesionActual.Nombre = datosPersonales.NombreCompleto;
            sesionActual.Leg = datosPersonales.Legajo.ToString();
            sesionActual.Rol = RolUsuario.Rol.ToString();
            sesionActual.dni = datosPersonales.DNI;


            return StatusCode(StatusCodes.Status200OK, sesionActual);
            
        }
    }
}
