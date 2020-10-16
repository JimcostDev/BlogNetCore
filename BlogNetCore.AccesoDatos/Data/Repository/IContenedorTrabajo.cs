using System;
using System.Collections.Generic;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository
{
    public interface IContenedorTrabajo : IDisposable
    {
        //aqui van todas mis entidades (unidad contenedora)
        ICategoriaRepository Categoria { get; }
        void Save();
    }
}
