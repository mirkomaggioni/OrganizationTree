using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackExchange.Redis;
using OrganizationTree.DataLayer;
using OrganizationTree.BusinessLayer.Services;
using OrganizationTree.Models;
using System.Threading.Tasks;

namespace OrganizationTreeTest
{
    [TestClass]
    public class Tests
    {
        private Context _context;
        private IDatabase _cache;
        private TreeNodeService _service;
        private Guid _idTopNode;

        [TestInitialize]
        public void Test()
        {
            _context = new Context();
            _cache = Cache.Connection.GetDatabase();
            _service = new TreeNodeService(_context, _cache);
            _idTopNode = new Guid("0bed1f36-745f-47c2-bb61-d83c025a82b8");
        }

        [TestMethod]
        public void getTree()
        {
            Task<TreeNode> tree = _service.loadTree(_idTopNode);
            tree.Wait();
            Assert.IsTrue(tree.Result != null);
        }
    }
}
