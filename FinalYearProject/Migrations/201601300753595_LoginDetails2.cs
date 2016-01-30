namespace FinalYearProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoginDetails2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobRoles",
                c => new
                    {
                        JobRoleID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(),
                        Privleges_ID = c.Int(),
                    })
                .PrimaryKey(t => t.JobRoleID)
                .ForeignKey("dbo.Privleges", t => t.Privleges_ID)
                .Index(t => t.Privleges_ID);
            
            CreateTable(
                "dbo.Privleges",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            AddColumn("dbo.AspNetUsers", "JobRole_JobRoleID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "JobRole_JobRoleID");
            AddForeignKey("dbo.AspNetUsers", "JobRole_JobRoleID", "dbo.JobRoles", "JobRoleID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "JobRole_JobRoleID", "dbo.JobRoles");
            DropForeignKey("dbo.JobRoles", "Privleges_ID", "dbo.Privleges");
            DropIndex("dbo.JobRoles", new[] { "Privleges_ID" });
            DropIndex("dbo.AspNetUsers", new[] { "JobRole_JobRoleID" });
            DropColumn("dbo.AspNetUsers", "JobRole_JobRoleID");
            DropColumn("dbo.AspNetUsers", "Surname");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.Privleges");
            DropTable("dbo.JobRoles");
        }
    }
}
