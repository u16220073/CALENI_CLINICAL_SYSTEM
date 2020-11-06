namespace Hospital_Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeProperty : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Psychologists", "Designation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Psychologists", "Designation", c => c.String(nullable: false));
        }
    }
}
