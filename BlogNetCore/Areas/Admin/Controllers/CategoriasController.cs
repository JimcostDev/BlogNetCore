﻿using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Categoria categoria)
        {
            //validar el modelo - validar que los campos obligatorios se envien (DataAnnontations)
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        #endregion

        #region EDIT
        /*********************************** EDITAR CATEGORIA ********************************************/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Categoria categoria = new Categoria();
            categoria = _contenedorTrabajo.Categoria.Get(id);//buscar categoria por su id
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Categoria categoria)
        {
            //validar el modelo - validar que los campos obligatorios se envien (DataAnnontations)
            if (ModelState.IsValid)
            {
                _contenedorTrabajo.Categoria.Update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }
        #endregion


        #region  LLAMADAS A LA API
        //OBTENER TODOS LOS DATOS DE LA ENTIDAD
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _contenedorTrabajo.Categoria.Get(id);//buscar categoria por su id
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar categoría" });
            }
            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se elimino categoría correctamente." });

            
        }
        #endregion
    }
}