using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> ListaArticulo { get; set; }
    }
}
