using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(7, "add checklists table")]
public class NTS7 : Migration
{
    public override void Up()
    {
        Create
            .Table("checklists")
            .WithColumn("id").AsGuid().PrimaryKey();
        
        Create
            .ForeignKey("FK_checklists_notebases_id")
            .FromTable("checklists").ForeignColumn("id")
            .ToTable("notebases").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("checklists");
    }
}