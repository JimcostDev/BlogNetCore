<<<<<<< HEAD
﻿using BlogNetCore.AccesoDatos.Data.Repository;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.AccesoDatos.Data.Repository;
>>>>>>> 1326d8e62ff5a1da37f3a8bd91b58e84aa4cd109
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BlogNetCore.Areas.Admin.Controllers
{
<<<<<<< HEAD
    [Area("Admin")]
=======
>>>>>>> 1326d8e62ff5a1da37f3a8bd91b58e84aa4cd109
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
<<<<<<< HEAD
            };
            return View(articuloViewModel);
=======
        };
        return View(articuloViewModel);
            
>>>>>>> 1326d8e62ff5a1da37f3a8bd91b58e84aa4cd109
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
