using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(9, "update notes and images tables")]
public class NTS9 : Migration
{
    public override void Up()
    {
        Delete
            .ForeignKey("FK_notes_images_imageid")
            .OnTable("notes");

        Delete
            .Column("imageid").FromTable("notes");

        Create
            .Column("noteid").OnTable("images").AsGuid().Nullable();
        
        Create
            .ForeignKey("FK_images_notes_imageid")
            .FromTable("images").ForeignColumn("noteid")
            .ToTable("notes").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .ForeignKey("FK_images_notes_imageid")
            .OnTable("notes");

        Delete
            .Column("noteid").FromTable("images");

        Create
            .Column("imageid").OnTable("notes").AsGuid().Nullable();
        
        Create
            .ForeignKey("FK_notes_images_imageid")
            .FromTable("notes").ForeignColumn("imageid")
            .ToTable("images").PrimaryColumn("id");
    }
}