using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TEP.Domain.Entities;
using TEP.Domain.Interfaces.Repositories;
using TEP.Infra.Data.Contexto;

namespace TEP.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IBaseRepository<TEntity>
    where TEntity : EntityBase
    {
        protected readonly Context context;

        public RepositoryBase(Context context)
            : base()
        {
            this.context = context;
        }

        public void Update(TEntity entity)
        {
            context.InitTransacao();
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SendChanges();
        }

        public void Delete(int id)
        {
            var entidade = GetById(id);
            if (entidade != null)
            {
                context.InitTransacao();
                context.Set<TEntity>().Remove(entidade);
                context.SendChanges();
            }
            else
            {
                throw new DbUpdateException($"Entity with ID={id} not Found.");
            }
        }

        public void Delete(TEntity entity)
        {
            context.InitTransacao();
            context.Set<TEntity>().Remove(entity);
            context.SendChanges();
        }

        public int Insert(TEntity entity)
        {
            context.InitTransacao();
            context.Set<TEntity>().Add(entity);
            context.SendChanges();
            return entity.Id;
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> List()
        {
            return context.Set<TEntity>().ToList();
        }
    }
}
