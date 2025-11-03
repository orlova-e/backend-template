using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(3, "add images table")]
public class NTS3 : Migration
{
    public override void Up()
    {
        Create
            .Table("images")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable().Indexed()
            .WithColumn("mimetype").AsString().NotNullable()
            .WithColumn("base64").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table("images");
    }
}