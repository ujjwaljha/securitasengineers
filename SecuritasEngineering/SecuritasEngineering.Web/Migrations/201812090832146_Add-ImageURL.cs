namespace SecuritasEngineering.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "ImageURL", c => c.String());
            AddColumn("dbo.Manufacturers", "ImageURL", c => c.String());
            AddColumn("dbo.Products", "ImageURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageURL");
            DropColumn("dbo.Manufacturers", "ImageURL");
            DropColumn("dbo.Categories", "ImageURL");
        }
    }
}
