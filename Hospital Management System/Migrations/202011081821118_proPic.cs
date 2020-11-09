namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patients", "ProPic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patients", "ProPic");
        }
    }
}
