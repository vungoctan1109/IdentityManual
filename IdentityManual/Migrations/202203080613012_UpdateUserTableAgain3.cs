namespace IdentityManual.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class UpdateUserTableAgain3 : DbMigration
    {
        public override void Up()
        {
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Description");
        }
    }
}