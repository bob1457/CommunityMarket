using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CommonMarket.DataAccess.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get all objects from database
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Select single item by specified experssion
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find object by specified expresssion (id in this case, could be more general..., see below)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(long id);

        //TEntity GetById(Guid id); // id could be guid type instaled of integer

        /// <summary>
        /// More general way to find object by specified expression
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> expression, string[] includes = null); //more general Find 

        /// <summary>
        /// Insert a new record to the database
        /// </summary>
        /// <param name="entity">Specify a new object to insert</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Delete record in the database
        /// </summary>
        /// <param name="entity">Specify a exsisting object to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Delete objects from database by specified expression (mass deletion)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="predicate"></param>
        void Detele(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Save changes of the objects in the database
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Update(TEntity entity);
    }
}
