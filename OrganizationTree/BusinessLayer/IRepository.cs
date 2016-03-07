using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.BusinessLayer
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Insert(TEntity entity);
        IUnitOfWork UnitOfWork { get; }
    }
}
