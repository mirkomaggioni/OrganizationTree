using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.BusinessLayer
{
    interface ICacheRepository<TObject> where TObject : class
    {
        void InsertOrUpdate(string key, TObject entity);
        Task InsertOrUpdateAsync(string key, TObject entity);
        void InsertOrUpdate(string key, IEnumerable<TObject> entity);
        Task InsertOrUpdateAsync(string key, IEnumerable<TObject> entity);
        TObject Get(string id);
        Task<TObject> GetAsync(string id);
        IEnumerable<TObject> GetList(string id);
        Task<IEnumerable<TObject>> GetListAsync(string id);
        void Invalidate(string id);
        Task InvalidateAsync(string id);
    }
}
