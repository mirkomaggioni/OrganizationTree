using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationTree.DataLayer
{
    [Table("Nodes")]
    public class Node
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
