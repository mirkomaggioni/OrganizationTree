using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizationTree.DataLayer
{
    [Table("Relations")]
    public class Relation
    {
        [Key, Column(Order = 0)]
        public Guid IdParent { get; set; }
        [ForeignKey("IdParent")]
        public virtual Node Parent { get; set; }
        [Key, Column(Order = 1)]
        public Guid IdChild { get; set; }
        [ForeignKey("IdChild")]
        public virtual Node Child { get; set; }
    }
}
