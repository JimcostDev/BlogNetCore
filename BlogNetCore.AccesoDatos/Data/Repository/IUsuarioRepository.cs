using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListaCategorias();
        void Update(Categoria categoria);
    }
}
