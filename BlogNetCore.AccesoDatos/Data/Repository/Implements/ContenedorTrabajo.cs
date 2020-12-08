using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository.Implements
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
        }
        #region ENTIDADES
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; } 
        #endregion

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
