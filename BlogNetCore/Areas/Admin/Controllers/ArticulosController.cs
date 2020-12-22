using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

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
            return View(articuloViewModel.Articulo);
        }
        #endregion

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
