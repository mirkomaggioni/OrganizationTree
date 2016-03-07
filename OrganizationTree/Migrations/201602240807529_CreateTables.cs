namespace OrganizationTree.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Relations",
                c => new
                    {
                        IdParent = c.Guid(nullable: false),
                        IdChild = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdParent, t.IdChild })
                .ForeignKey("dbo.Nodes", t => t.IdChild)
                .ForeignKey("dbo.Nodes", t => t.IdParent)
                .Index(t => t.IdParent)
                .Index(t => t.IdChild);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Relations", "IdParent", "dbo.Nodes");
            DropForeignKey("dbo.Relations", "IdChild", "dbo.Nodes");
            DropIndex("dbo.Relations", new[] { "IdChild" });
            DropIndex("dbo.Relations", new[] { "IdParent" });
            DropTable("dbo.Relations");
            DropTable("dbo.Nodes");
        }
    }
}
