namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRegonToCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Regon", c => c.String(maxLength: 14, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Regon");
        }
    }
}
