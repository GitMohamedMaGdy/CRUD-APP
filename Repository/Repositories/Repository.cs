using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Task.Domain;
using Task.Repository;
using Task.Repository.App;

namespace Task.Repositories
{
    //Allow to be inherited and not initiated
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await DbSet.FindAsync(new object[] { id });
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsyncAsNoTracking(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(int startIdndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.Skip(startIdndex).Take(pageSize).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (sortExpression != null)
            {
                query = sortASC ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
            }
            return await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

        }

        public Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (sortExpression != null)
            {
                query = sortASC ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
            }
            return query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();

        }

        public Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            if (condition != default)
            {
                query = DbSet.Where(condition);
            }
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (sortExpression != null)
            {
                query = sortASC ? query.OrderBy(sortExpression) : query.OrderByDescending(sortExpression);
            }
            return query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        }

        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.CountAsync();
        }

        public Task<int> GetCountAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking().Where(condition);
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.CountAsync();
        }


        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();
            foreach (var includeProperty in includeProperties.Split(new string[] { Constants.STRING_DELIMITER }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.AnyAsync();
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
