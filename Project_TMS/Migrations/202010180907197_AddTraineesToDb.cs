namespace Project_TMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTraineesToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ManageTrainees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TraineeId = c.String(maxLength: 128),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.TraineeId)
                .Index(t => t.TraineeId)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ManageTrainees", "TraineeId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ManageTrainees", "TopicId", "dbo.Topics");
            DropIndex("dbo.ManageTrainees", new[] { "TopicId" });
            DropIndex("dbo.ManageTrainees", new[] { "TraineeId" });
            DropTable("dbo.ManageTrainees");
        }
    }
}
