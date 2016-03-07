using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrganizationTree.DataLayer;
using System.Runtime.Serialization;

namespace OrganizationTree.Models
{
    [DataContract]
    public class TreeNode
    {
        public TreeNode()
        {
            IEnumerable<Node> Childs = new List<Node>();
        }

        [DataMember]
        public Node Node { get; set; }
        [DataMember]
        public TreeNode Parent { get; set; }
        public IEnumerable<TreeNode> Childs { get; set; }
    }
}
