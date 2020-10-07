using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogNetCore.AccesoDatos.Data.Repository
{
    //Interface : define las reglas que las entidades deben cumplir al momento de implementarlas (contrato)
    //me dice que hacer, mas no como implementarlo
    public interface IRepository<TEntity> where TEntity : class  //recibe un objeto donde sea una clase
    {
        //retornar registro por id
        TEntity Get(int id);

        //retornar todos los registros de una tabla
        IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null
        );
        //retornar el primer registro o por defecto
        TEntity GetFirtsOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null
        );

        void Add(TEntity entity);
        void Remove(int id);
        void Remove(TEntity entity);
    }
}
