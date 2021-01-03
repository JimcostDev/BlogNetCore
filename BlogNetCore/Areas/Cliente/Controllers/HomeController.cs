using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogNetCore.Models;
using BlogNetCore.AccesoDatos.Data.Repository;
using BlogNetCore.Models.ViewModels;

namespace BlogNetCore.Controllers
{
    [Area("Cliente")]
    public class HomeController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public HomeController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public IActionResult Index()
        {
            //para poder tener tanto articulos como sliders en una misma pagina
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Sliders = _contenedorTrabajo.Slider.GetAll(),
                ListaArticulo = _contenedorTrabajo.Articulo.GetAll()
            };
            return View(homeViewModel);
        }
    }
}
