﻿using BlogNetCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository.Implements
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now.AddMonths(6);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
