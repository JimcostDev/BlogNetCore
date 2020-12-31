using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models;
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
        #region CREATE
        /*********************************** CREAR SLIDER ********************************************/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                #region SUBIR IMAGEN
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // wwwroot
                var archivos = HttpContext.Request.Form.Files;
                if (slider.Id == 0)
                {
                    //nuevo Slider
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                    #endregion

                    
                    //guardar
                    _contenedorTrabajo.Slider.Add(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            
            return View(slider);
        }
        #endregion
        #region EDIT
        /*********************************** EDITAR ARTICULO ********************************************/
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Slider slider = new Slider();
            if (id != null)
            {
                slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                #region SUBIR IMAGEN
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // wwwroot
                var archivos = HttpContext.Request.Form.Files;
                //obtener por su id el articulo
                var objDesdeDb = _contenedorTrabajo.Slider.Get(slider.Id);

                if (archivos.Count() > 0)
                {
                    //Editar imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, objDesdeDb.UrlImagen.TrimStart('\\'));

                    //Remplazar imagen
                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    //subimos nuevamente el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + nuevaExtension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + nuevaExtension;
                    #endregion

                    //guardar
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y no se reemplaza, debe conservar la que ya tiene
                    slider.UrlImagen = objDesdeDb.UrlImagen;
                    //guardar
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }
        #endregion

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
