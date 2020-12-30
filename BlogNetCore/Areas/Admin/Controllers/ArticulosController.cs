using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;

namespace BlogNetCore.Areas.Admin.Controllers
{

    [Area("Admin")]


    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            //con esto puedo acceder a todas las entedidades
            _contenedorTrabajo = contenedorTrabajo;
            //para poder subir las imagenes
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region CREATE
        /*********************************** CREAR ARTICULO ********************************************/
        [HttpGet]
        public IActionResult Create()
        {
            //instaciamos el viewmodel para poder obtener la informacion de categoria
            ArticuloViewModel articuloViewModel = new ArticuloViewModel()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };
            return View(articuloViewModel);
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloViewModel articuloViewModel)
        {
            if (ModelState.IsValid)
            {
                #region SUBIR IMAGEN
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // wwwroot
                var archivos = HttpContext.Request.Form.Files;
                if (articuloViewModel.Articulo.Id == 0)
                {
                    //nuevo articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    articuloViewModel.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    articuloViewModel.Articulo.FechaCreacion = DateTime.Now.ToString(); 
                    #endregion

                    //guardar
                    _contenedorTrabajo.Articulo.Add(articuloViewModel.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            articuloViewModel.ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias();
            return View(articuloViewModel);
        }
        #endregion
        #region EDIT
        /*********************************** EDITAR ARTICULO ********************************************/
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //instaciamos el viewmodel para poder obtener la informacion de categoria
            ArticuloViewModel articuloViewModel = new ArticuloViewModel()
            {
                Articulo = new Models.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListaCategorias()

            };
            if (id != null)
            {
                articuloViewModel.Articulo = _contenedorTrabajo.Articulo.Get(id.GetValueOrDefault());
            }
            return View(articuloViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloViewModel articuloViewModel)
        {
            if (ModelState.IsValid) 
            {
                #region SUBIR IMAGEN
                string rutaPrincipal = _hostingEnvironment.WebRootPath; // wwwroot
                var archivos = HttpContext.Request.Form.Files;
                //obtener por su id el articulo
                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get(articuloViewModel.Articulo.Id);

                if (archivos.Count() > 0)
                {
                    //Editar imagen
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.UrlImagen.TrimStart('\\'));

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
                    articuloViewModel.Articulo.UrlImagen = @"\imagenes\articulos\" + nombreArchivo + nuevaExtension;
                    articuloViewModel.Articulo.FechaCreacion = DateTime.Now.ToString();
                    #endregion

                    //guardar
                    _contenedorTrabajo.Articulo.Update(articuloViewModel.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //cuando la imagen ya existe y no se reemplaza, debe conservar la que ya tiene
                    articuloViewModel.Articulo.UrlImagen = articuloDesdeDb.UrlImagen;
                    //guardar
                    _contenedorTrabajo.Articulo.Update(articuloViewModel.Articulo);
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
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }
        /*********************************** ELIMINAR ARTICULO ********************************************/
        [HttpDelete]
        public IActionResult Delete(int id)
        {

            var objFromDb = _contenedorTrabajo.Articulo.Get(id);//buscar categoria por su id
            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath; // wwwroot
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, objFromDb.UrlImagen.TrimStart('\\'));
            //eliminar imagen
            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error al borrar artículo" });
            }
            _contenedorTrabajo.Articulo.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Se elimino artículo correctamente." });


        }
        #endregion
    }
}
