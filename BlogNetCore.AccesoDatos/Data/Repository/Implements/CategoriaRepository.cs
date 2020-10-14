using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository.Implements
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListaCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem()
            {
                Text = i.Nombre,
                Value = i.Id.ToString()
            });
        }

        //El Update se utiliza en cada entidad (es algo unico), ya que sus parametros cambian dependiendo la tabla/modelo, por eso no va en el Repository generico.
        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;
            _db.SaveChanges();
        }
    }
}
