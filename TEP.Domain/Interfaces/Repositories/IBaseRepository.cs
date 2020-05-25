using System.Collections.Generic;
using TEP.Domain.Entities;

namespace TEP.Domain.Interfaces.Repositories
{
    //a repository works with data (ie. SQL, Webservice etc.) but that's the only job. CRUD operations, nothing more. There is no place for stored procedure based busines logic.
    public interface IBaseRepository <TEntity> where TEntity : EntityBase
    {
        int Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> List();
    }
}
