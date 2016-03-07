using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.BusinessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        new void Dispose();
        void Save();
        void Dispose(bool disposing);
    }
}
