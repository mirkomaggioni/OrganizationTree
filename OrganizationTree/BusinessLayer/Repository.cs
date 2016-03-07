using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.BusinessLayer
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private IUnitOfWork unitOfWork;
        internal DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork == null)
                {
                    unitOfWork = new UnitOfWork(_dbContext);
                }
                return unitOfWork;
            }
        }
    }
}
