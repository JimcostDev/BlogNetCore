using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slider> Slider { get; set; }
        public IEnumerable<Articulo> Articulo { get; set; }
    }
}
