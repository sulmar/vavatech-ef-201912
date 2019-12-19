namespace Vavatech.WebApi.DbServices.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomersView : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE VIEW dbo.vwGetCustomers AS SELECT * FROM dbo.Customers");
        }
        
        public override void Down()
        {
            Sql("DROP VIEW dbo.vwGetCustomers");
        }
    }
}
