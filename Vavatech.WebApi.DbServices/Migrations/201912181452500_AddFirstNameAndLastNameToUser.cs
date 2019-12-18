namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFirstNameAndLastNameToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FirstName", c => c.String());
            AddColumn("dbo.Users", "LastName", c => c.String());
            Sql("update Users set FirstName = SUBSTRING(FullName, 0, CHARINDEX(' ', FullName)), LastName = rtrim(SUBSTRING(FullName, CHARINDEX(' ', FullName), 100))");
            DropColumn("dbo.Users", "FullName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "FullName", c => c.String());
            Sql("update Users set FullName = FirstName + ' ' + LastName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "FirstName");
           
        }
    }
}
