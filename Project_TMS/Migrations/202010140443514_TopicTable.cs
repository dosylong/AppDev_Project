namespace Project_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TopicTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Descriptions = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Topics");
        }
    }
}
