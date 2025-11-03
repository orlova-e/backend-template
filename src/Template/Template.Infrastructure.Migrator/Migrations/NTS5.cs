using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(5, "add notes table")]
public class NTS5 : Migration
{
    public override void Up()
    {
        Create
            .Table("notes")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("text").AsString().Nullable()
            .WithColumn("imageid").AsGuid().Nullable();
        
        Create
            .ForeignKey("FK_notes_notebases_id")
            .FromTable("notes").ForeignColumn("id")
            .ToTable("notebases").PrimaryColumn("id");
        
        Create
            .ForeignKey("FK_notes_images_imageid")
            .FromTable("notes").ForeignColumn("imageid")
            .ToTable("images").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("notes");
    }
}