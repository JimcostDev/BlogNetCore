using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
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
        #region  LLAMADAS A LA API
        //OBTENER TODOS LOS DATOS DE LA ENTIDAD
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        //[HttpDelete]
        //public IActionResult Delete(int id)
        //{

        //    var objFromDb = _contenedorTrabajo.Categoria.Get(id);//buscar categoria por su id
        //    if (objFromDb == null)
        //    {
        //        return Json(new { success = false, message = "Error al borrar categoría" });
        //    }
        //    _contenedorTrabajo.Categoria.Remove(objFromDb);
        //    _contenedorTrabajo.Save();
        //    return Json(new { success = true, message = "Se elimino categoría correctamente." });

        //    
        //}
        #endregion
    }
}
