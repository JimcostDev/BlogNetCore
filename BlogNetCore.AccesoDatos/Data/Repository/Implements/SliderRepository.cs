using BlogNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository.Implements
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
            //_db.SaveChanges();
        }
    }
}
