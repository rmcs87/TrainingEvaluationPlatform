using System;
using System.Collections.Generic;
using System.Text;
using TEP.Appication.DTO;
using TEP.Domain.Entities;

namespace TEP.Appication.Interfaces
{
    public interface IAppBase<TEntity, TEntityDTO>
        where TEntity : EntityBase
        where TEntityDTO : DTOBase
    {
        int Insert(TEntityDTO entity);
        void Delete(int id);
        void Delete(TEntityDTO entity);
        void Update(TEntityDTO entity);
        TEntityDTO GetById(int id);
        IEnumerable<TEntityDTO> List();
    }
}
