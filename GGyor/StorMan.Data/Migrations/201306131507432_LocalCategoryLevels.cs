namespace StorMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LocalCategoryLevels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LocalCategories", "ParentCategory_Code", c => c.String(maxLength: 128));
            AddForeignKey("dbo.LocalCategories", "ParentCategory_Code", "dbo.LocalCategories", "Code");
            CreateIndex("dbo.LocalCategories", "ParentCategory_Code");
        }
        
        public override void Down()
        {
            DropIndex("dbo.LocalCategories", new[] { "ParentCategory_Code" });
            DropForeignKey("dbo.LocalCategories", "ParentCategory_Code", "dbo.LocalCategories");
            DropColumn("dbo.LocalCategories", "ParentCategory_Code");
        }
    }
}
