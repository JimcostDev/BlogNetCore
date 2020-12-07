using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            //con esto puedo acceder a todas las entedidades
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #region CREATE
        /*********************************** CREAR CATEGORIA ********************************************/
        //mostar formulario para crear una nueva categoria, por ello es de tipo HttpGet
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        
        #endregion


        #region  LLAMADAS A LA API
        //OBTENER TODOS LOS DATOS DE LA ENTIDAD
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }
        #endregion

    }
}