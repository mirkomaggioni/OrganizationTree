namespace OrganizationTree.Migrations
{
    using DataLayer;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OrganizationTree.DataLayer.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(OrganizationTree.DataLayer.Context context)
        {
            List<Node> nodes = new List<Node>();
            Node topNode = new Node()
            {
                Id = new Guid("0bed1f36-745f-47c2-bb61-d83c025a82b8"),
                Description = "Node 0"
            };

            nodes.Add(topNode);

            for (int i = 1; i < 500; i++)
            {
                Node node = new Node()
                {
                    Id = Guid.NewGuid(),
                    Description = "Node " + i.ToString()
                };

                nodes.Add(node);
            }

            List<Relation> relations = new List<Relation>();

            for (int i = 0; i < 100; i++)
            {
                Node parent = nodes.ElementAt(i);
                int startChildsIndex = 1 + (i * 5);
                int endChildsIndex = (startChildsIndex + 5) < nodes.Count() ? (startChildsIndex + 5) : 500;

                for (int a = startChildsIndex; a < endChildsIndex; a++)
                {
                    Node child = nodes.ElementAt(a);
                    Relation relation = new Relation()
                    {
                        IdParent = parent.Id,
                        IdChild = child.Id
                    };

                    relations.Add(relation);
                }
            }

            context.Nodes.AddRange(nodes);
            context.Relations.AddRange(relations);
            context.SaveChanges();
        }
    }
}
