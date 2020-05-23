using System;
using System.Collections.Generic;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;

namespace TEP.Appication.Services
{
    public class ServiceAppBase<TEntity, TEntityDTO> : IAppBase<TEntity, TEntityDTO>
        where TEntity : EntityBase
        where TEntityDTO : DTOBase
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntityDTO entity)
        {
            throw new NotImplementedException();
        }

        public TEntityDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(TEntityDTO entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntityDTO> List()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntityDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
