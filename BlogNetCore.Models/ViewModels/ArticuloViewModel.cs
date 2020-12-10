using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.Models.ViewModels
{
    public class ArticuloViewModel
    {
        public Articulo Articulo { get; set; }
        public IEnumerable<SelectListItem> ListaCategorias  { get; set; }
    }
}
