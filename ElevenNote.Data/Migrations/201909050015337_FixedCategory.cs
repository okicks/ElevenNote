namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "OwnerId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "OwnerId");
        }
    }
}
