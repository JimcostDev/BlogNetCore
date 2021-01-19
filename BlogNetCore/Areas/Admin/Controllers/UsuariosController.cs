using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogNetCore.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public UsuariosController(IContenedorTrabajo contenedorTrabajo)
        {
            //con esto puedo acceder a todas las entedidades
            _contenedorTrabajo = contenedorTrabajo;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity; //Usuario logeado
            var usuarioActual = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);//Id
            return View(_contenedorTrabajo.Usuario.GetAll(u => u.Id != usuarioActual.Value));//lista de usuarios exeptuando el actual(login)

        }
    }
}
