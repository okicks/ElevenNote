namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            AddColumn("dbo.Note", "CategoryRefId", c => c.Int(nullable: true));
            CreateIndex("dbo.Note", "CategoryRefId");
            AddForeignKey("dbo.Note", "CategoryRefId", "dbo.Category", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CategoryRefId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CategoryRefId" });
            DropColumn("dbo.Note", "CategoryRefId");
            DropTable("dbo.Category");
        }
    }
}
