namespace DefinitionExtractionWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ascriptors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ascriptor_content = c.String(nullable: false, maxLength: 50),
                        Descriptor_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Descriptors", t => t.Descriptor_ID)
                .Index(t => t.Descriptor_ID);
            
            CreateTable(
                "dbo.Descriptors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descriptor_content = c.String(nullable: false, maxLength: 100),
                        Start_line = c.Int(nullable: false),
                        Start_char = c.Int(nullable: false),
                        End_line = c.Int(nullable: false),
                        End_char = c.Int(nullable: false),
                        Relator = c.String(maxLength: 50),
                        RelatorID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Relators", t => t.RelatorID)
                .Index(t => t.RelatorID);
            
            CreateTable(
                "dbo.DefinitionLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descriptor_ID = c.Int(nullable: false),
                        Start_char = c.Int(nullable: false),
                        Definition_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Definitions", t => t.Definition_ID)
                .ForeignKey("dbo.Descriptors", t => t.Descriptor_ID)
                .Index(t => t.Descriptor_ID)
                .Index(t => t.Definition_ID);
            
            CreateTable(
                "dbo.Definitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descriptor_ID = c.Int(nullable: false),
                        Definition_content = c.String(nullable: false, maxLength: 300),
                        Start_line = c.Int(nullable: false),
                        Start_char = c.Int(nullable: false),
                        End_line = c.Int(nullable: false),
                        End_char = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Insert_date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .ForeignKey("dbo.Descriptors", t => t.Descriptor_ID)
                .Index(t => t.Descriptor_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        First_name = c.String(nullable: false, maxLength: 50),
                        Last_name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password_hash = c.String(nullable: false, maxLength: 100),
                        registration_date = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Relations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descriptor1_ID = c.Int(nullable: false),
                        Descriptor2_ID = c.Int(nullable: false),
                        Relation_type_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Relation_types", t => t.Relation_type_ID)
                .ForeignKey("dbo.Descriptors", t => t.Descriptor1_ID)
                .ForeignKey("dbo.Descriptors", t => t.Descriptor2_ID)
                .Index(t => t.Descriptor1_ID)
                .Index(t => t.Descriptor2_ID)
                .Index(t => t.Relation_type_ID);
            
            CreateTable(
                "dbo.Relation_types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type_name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Relators",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Descriptors", "RelatorID", "dbo.Relators");
            DropForeignKey("dbo.Relations", "Descriptor2_ID", "dbo.Descriptors");
            DropForeignKey("dbo.Relations", "Descriptor1_ID", "dbo.Descriptors");
            DropForeignKey("dbo.Relations", "Relation_type_ID", "dbo.Relation_types");
            DropForeignKey("dbo.Definitions", "Descriptor_ID", "dbo.Descriptors");
            DropForeignKey("dbo.DefinitionLinks", "Descriptor_ID", "dbo.Descriptors");
            DropForeignKey("dbo.Definitions", "User_ID", "dbo.Users");
            DropForeignKey("dbo.DefinitionLinks", "Definition_ID", "dbo.Definitions");
            DropForeignKey("dbo.Ascriptors", "Descriptor_ID", "dbo.Descriptors");
            DropIndex("dbo.Relations", new[] { "Relation_type_ID" });
            DropIndex("dbo.Relations", new[] { "Descriptor2_ID" });
            DropIndex("dbo.Relations", new[] { "Descriptor1_ID" });
            DropIndex("dbo.Definitions", new[] { "User_ID" });
            DropIndex("dbo.Definitions", new[] { "Descriptor_ID" });
            DropIndex("dbo.DefinitionLinks", new[] { "Definition_ID" });
            DropIndex("dbo.DefinitionLinks", new[] { "Descriptor_ID" });
            DropIndex("dbo.Descriptors", new[] { "RelatorID" });
            DropIndex("dbo.Ascriptors", new[] { "Descriptor_ID" });
            DropTable("dbo.Relators");
            DropTable("dbo.Relation_types");
            DropTable("dbo.Relations");
            DropTable("dbo.Users");
            DropTable("dbo.Definitions");
            DropTable("dbo.DefinitionLinks");
            DropTable("dbo.Descriptors");
            DropTable("dbo.Ascriptors");
        }
    }
}
