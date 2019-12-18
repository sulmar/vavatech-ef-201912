namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWeightToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Weight", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Weight");
        }
    }
}
