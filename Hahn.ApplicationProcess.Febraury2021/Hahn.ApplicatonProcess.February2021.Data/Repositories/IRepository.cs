using Hahn.ApplicatonProcess.February2021.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    public interface IRepository<TId, TEntity>
        where TId : struct
        where TEntity : BaseEntity<TId>
    {
        Task AddAsync(TEntity entity);
        Task<TEntity> FindAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);
    }
}
