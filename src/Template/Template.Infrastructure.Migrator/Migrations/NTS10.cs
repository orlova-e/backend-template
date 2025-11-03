using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(10, "add locations table")]
public class NTS10 : Migration
{
    public override void Up()
    {
        Create
            .Table("locations")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("code").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete
            .Table("locations");
    }
}