using System.Collections.Generic;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Domain.Interfaces.Services;

namespace TEP.Domain.Services
{
    public class BaseService<TEntity> : IServiceBase<TEntity> where TEntity : EntityBase
    {
        protected readonly IBaseRepository<TEntity> _repository;

        public BaseService(IBaseRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void Delete(TEntity entity)
        {
            _repository.Delete(entity);
        }

        public TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public int Insert(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        public IEnumerable<TEntity> List()
        {
            return _repository.List();
        }

        public void Update(TEntity entity)
        {
            _repository.Update(entity);
        }
    }
}
