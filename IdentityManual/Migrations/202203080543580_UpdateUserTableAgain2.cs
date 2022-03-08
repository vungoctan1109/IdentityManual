namespace IdentityManual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateUserTableAgain2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
        }

        public override void Down()
        {
        }
    }
}