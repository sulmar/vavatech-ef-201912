using System.Collections.Generic;

namespace Vavatech.WebApi.IServices
{
    // interfejs generyczny
    public interface IEntityService<TEntity>
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
        bool Exists(int id);
    }



 

}
