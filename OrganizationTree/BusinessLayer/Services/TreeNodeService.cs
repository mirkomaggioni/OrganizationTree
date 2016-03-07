using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationTree.DataLayer;
using OrganizationTree.Models;
using System.Data.Entity;
using StackExchange.Redis;

namespace OrganizationTree.BusinessLayer.Services
{
    public class TreeNodeService
    {
        IRepository<Node> _nodeRepository;
        IRepository<Relation> _relationRepository;
        ICacheRepository<TreeNode> _cacheRepository;

        public TreeNodeService(DbContext dbContext, IDatabase cache)
        {
            _nodeRepository = new Repository<Node>(dbContext);
            _relationRepository = new Repository<Relation>(dbContext);
            _cacheRepository = new RedisCacheRepository<TreeNode>(cache);
        }

        public async Task<TreeNode> loadTree(Guid idTopNode)
        {
            return await getTree(idTopNode);
        }

        public async Task<IEnumerable<TreeNode>> getChilds(TreeNode treeTopNode)
        {
            IEnumerable<TreeNode> childNodes = await _cacheRepository.GetListAsync(treeTopNode.Node.Id.ToString());
            if (childNodes.Count() > 0)
            {
                return await Task.Run(() => getChildsFromCache(treeTopNode).ToList());
            }
            else
            {
                IEnumerable<TreeNode> childsTopNode = await Task.Run(() => getChildsFromDatabase(treeTopNode).ToList());
                await _cacheRepository.InsertOrUpdateAsync(treeTopNode.Node.Id.ToString(), childsTopNode);
                return childsTopNode;
            }
        }

        private async Task<TreeNode> getTree(Guid idTopNode)
        {
            Node topNode = _nodeRepository.FirstOrDefault(n => n.Id == idTopNode);
            TreeNode treeTopNode = new TreeNode();

            if (topNode != null)
            {
                treeTopNode.Node = topNode;
                treeTopNode.Childs = await getChilds(treeTopNode);
            }

            return treeTopNode;
        }

        private IEnumerable<TreeNode> getChildsFromDatabase(TreeNode parentTreeNode)
        {
            List<Relation> relations = _relationRepository.Find(r => r.IdParent == parentTreeNode.Node.Id).ToList();

            foreach (Relation rel in relations)
            {
                TreeNode childTreeNode = new TreeNode();
                childTreeNode.Node = rel.Child;
                childTreeNode.Parent = parentTreeNode;

                childTreeNode.Childs = getChildsFromDatabase(childTreeNode).ToList();

                if (childTreeNode.Childs.Count() > 0)
                {
                    _cacheRepository.InsertOrUpdateAsync(childTreeNode.Node.Id.ToString(), childTreeNode.Childs);
                }

                yield return childTreeNode;
            }
        }

        private IEnumerable<TreeNode> getChildsFromCache(TreeNode parentTreeNode)
        {
            parentTreeNode.Childs = _cacheRepository.GetList(parentTreeNode.Node.Id.ToString());

            foreach (TreeNode childTreeNode in parentTreeNode.Childs)
            {
                childTreeNode.Childs = getChildsFromCache(childTreeNode).ToList();
                yield return childTreeNode;
            }
        }
    }
}
