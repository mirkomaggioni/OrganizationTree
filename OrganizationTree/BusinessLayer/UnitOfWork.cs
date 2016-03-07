using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.BusinessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _dbContext;
        private bool _disposed;

        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();

            _disposed = true;
        }
    }
}
