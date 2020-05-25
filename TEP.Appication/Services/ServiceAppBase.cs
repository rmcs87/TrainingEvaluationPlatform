using AutoMapper;
using System;
using System.Collections.Generic;
using TEP.Appication.DTO;
using TEP.Appication.Interfaces;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Services;

namespace TEP.Appication.Services
{
    public class ServiceAppBase<TEntity, TEntityDTO> : IAppBase<TEntity, TEntityDTO>
        where TEntity : EntityBase
        where TEntityDTO : DTOBase
    {
        protected readonly IServiceBase<TEntity> _service;
        protected readonly IMapper _iMapper;

        public ServiceAppBase(IServiceBase<TEntity> service, IMapper iMapper) : base()
        {
            _service = service;
            _iMapper = iMapper;
        }
        public void Delete(int id)
        {
            _service.Delete(id);
        }
        public void Delete(TEntityDTO entity)
        {
            _service.Delete(_iMapper.Map<TEntity>(entity));
        }
        public TEntityDTO GetById(int id)
        {
            return _iMapper.Map<TEntityDTO>(_service.GetById(id));
        }
        public int Insert(TEntityDTO entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TEntityDTO> List()
        {
            return _iMapper.Map<IEnumerable<TEntityDTO>>(_service.List());
        }
        public void Update(TEntityDTO entity)
        {
            _service.Update(_iMapper.Map<TEntity>(entity));
        }
    }
}
