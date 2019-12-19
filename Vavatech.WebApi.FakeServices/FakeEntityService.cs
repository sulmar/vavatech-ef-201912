using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.WebApi.IServices;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.FakeServices
{
    public class FakeEntityService<TEntity> : IEntityService<TEntity>
        where TEntity : EntityBase
    {
        protected readonly ICollection<TEntity> entities;
        private readonly Faker<TEntity> entityFaker;

        public FakeEntityService(Faker<TEntity> entityFaker)
        {
            this.entityFaker = entityFaker;

            entities = entityFaker.Generate(100);
        }

        public void Add(TEntity entity)
        {
            entities.Add(entity);
        }

        public bool Exists(int id)
        {
            return entities.Any(p => p.Id == id);
        }

        public IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public void Remove(int id)
        {
            entities.Remove(Get(id));
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
