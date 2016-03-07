using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace OrganizationTree.DataLayer
{
    public class Context : DbContext
    {
        public Context() : base()
        {
        }

        public DbSet<Node> Nodes { get; set; }
        public DbSet<Relation> Relations { get; set; }
    }
}
