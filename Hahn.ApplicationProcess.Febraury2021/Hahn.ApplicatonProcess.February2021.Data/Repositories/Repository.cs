using Hahn.ApplicatonProcess.February2021.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.February2021.Data.Repositories
{
    
    public class Repository<TId, TEntity>
        where TId : struct
        where TEntity : class
    {
        internal VeronikaContext _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(VeronikaContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;
            if(filter is not null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(orderBy is not null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual async Task<TEntity> FindAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }
        
        public virtual async void Update(TId id)
        {
            TEntity entityToDelete = await _dbSet.FindAsync(id);
            _dbSet.Remove(entityToDelete);
        }
    }    
    //public class Repository<TId, TEntity> : IRepository<TId, TEntity>
    //    where TId : struct
    //    where TEntity : BaseEntity<TId>
    //{
    //    private readonly VeronikaContext _context;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public Repository(VeronikaContext context, IUnitOfWork unitOfWork)
    //    {
    //        _context = context;
    //        _unitOfWork = unitOfWork;
    //    }
        
    //    public async Task AddAsync(TEntity entity)
    //    {
    //        ValidateEntity(entity);
    //        try
    //        {
    //            _context.Set<TEntity>().Add(entity);
    //        }
    //        finally
    //        {
    //            await _unitOfWork.SaveAsync();
    //        }
    //    }

    //    public async Task DeleteAsync(TEntity entity)
    //    {
    //        try
    //        {
    //            _context.Set<TEntity>().Remove(entity);
    //        }
    //        finally
    //        {
    //            await _unitOfWork.SaveAsync();
    //        }
    //    }

    //    public async Task<TEntity> FindAsync(TId id)
    //        => await _context.Set<TEntity>().FindAsync(id);
            

    //    public async Task<IEnumerable<TEntity>> GetAllAsync()
    //    {
    //        return await _context.Set<TEntity>().ToListAsync();
    //    }

    //    public async Task UpdateAsync(TEntity entity)
    //    {
    //        ValidateEntity(entity);
    //        try
    //        {
    //            _context.Set<TEntity>().Update(entity);

    //        }
    //        finally
    //        {

    //            await _unitOfWork.SaveAsync();
    //        }
    //    }

    //    private static void ValidateEntity(TEntity entity)
    //    {
    //        if(entity is null)
    //        {
    //            throw new ArgumentNullException(nameof(entity), "The entity cannot be null");
    //        }

    //    }
    //}
}
