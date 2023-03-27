using System.Linq.Expressions;

namespace Task.Domain
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<List<TEntity>> GetAllAsyncAsNoTracking(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(int startIdndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true, string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true, string includeProperties = "");
        Task<List<TEntity>> GetAllAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> sortExpression = null, bool sortASC = true);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task<int> SaveChangesAsync();
    }
}
