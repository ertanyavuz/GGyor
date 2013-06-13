namespace StorMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.LocalCategories",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.LocalCategoryCategories",
                c => new
                    {
                        LocalCategory_Code = c.String(nullable: false, maxLength: 128),
                        Category_Code = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LocalCategory_Code, t.Category_Code })
                .ForeignKey("dbo.LocalCategories", t => t.LocalCategory_Code, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_Code, cascadeDelete: true)
                .Index(t => t.LocalCategory_Code)
                .Index(t => t.Category_Code);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.LocalCategoryCategories", new[] { "Category_Code" });
            DropIndex("dbo.LocalCategoryCategories", new[] { "LocalCategory_Code" });
            DropForeignKey("dbo.LocalCategoryCategories", "Category_Code", "dbo.Categories");
            DropForeignKey("dbo.LocalCategoryCategories", "LocalCategory_Code", "dbo.LocalCategories");
            DropTable("dbo.LocalCategoryCategories");
            DropTable("dbo.LocalCategories");
            DropTable("dbo.Categories");
        }
    }
}
