using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(1, "add users table")]
public class NTS1 : Migration
{
    public override void Up()
    {
        Create
            .Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("created").AsDateTime().NotNullable()
            .WithColumn("updated").AsDateTime().Nullable()
            .WithColumn("deleted").AsDateTime().Nullable();
    }

    public override void Down()
    {
        Delete
            .Table("users");
    }
}