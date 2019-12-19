using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;


namespace Vavatech.WebApi.DbServices
{
    public class DbEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : EntityBase, new()
    {
        private readonly WarehouseContext context;

        public DbEntityService(WarehouseContext context)
        {
            this.context = context;
        }

        protected DbSet<TEntity> entities => context.Set<TEntity>();

        public void Add(TEntity entity)
        {
            entities.Add(entity);
            context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return entities.Any(p => p.Id == id);
        }

        public IEnumerable<TEntity> Get()
        {
            return entities.ToList();
        }

        public TEntity Get(int id)
        {
            return entities.Find(id);
        }

        public void Remove(int id)
        {
            TEntity entity = new TEntity { Id = id };
            entities.Remove(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
