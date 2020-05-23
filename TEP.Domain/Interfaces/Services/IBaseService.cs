using System;
using System.Collections.Generic;
using System.Text;
using TEP.Domain.Entities;

namespace TEP.Domain.Interfaces.Services
{
    //The service (or business logic layer) provides the functionality. How to fullfill a business request (ie. calculate salary), what you have to do.
    public interface IBaseService<TEntity> where TEntity : EntityBase
    {
        int Insert(TEntity entity);
        void Delete(int id);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> List();
    }
}
}
