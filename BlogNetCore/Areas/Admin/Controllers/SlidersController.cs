using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.AccesoDatos.Data.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            //con esto puedo acceder a todas las entedidades
            _contenedorTrabajo = contenedorTrabajo;
            //para poder subir las imagenes
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        #region LLAMADAS A LA API
        //OBTENER TODOS LOS DATOS DE LA ENTIDAD
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        #endregion
    }
}
