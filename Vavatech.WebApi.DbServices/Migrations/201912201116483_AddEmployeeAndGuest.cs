namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeAndGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Salary", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Users", "Department", c => c.String());
            AddColumn("dbo.Users", "Token", c => c.String());
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Discriminator");
            DropColumn("dbo.Users", "Token");
            DropColumn("dbo.Users", "Department");
            DropColumn("dbo.Users", "Salary");
        }
    }
}
