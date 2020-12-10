using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BlogNetCore.Areas.Admin.Controllers
{

    [Area("Admin")]


    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo)
        {
            //con esto puedo acceder a todas las entedidades
            _contenedorTrabajo = contenedorTrabajo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //instaciamos el viewmodel para poder obtener la informacion de categoria
            ArticuloViewModel articuloViewModel = new ArticuloViewModel()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };
            return View(articuloViewModel);

        }
        #region LLAMADAS A LA API
        //OBTENER TODOS LOS DATOS DE LA ENTIDAD
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        } 
        #endregion
    }
}
