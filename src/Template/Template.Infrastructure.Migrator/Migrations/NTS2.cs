using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(2, "add accounts table")]
public class NTS2 : Migration
{
    public override void Up()
    {
        Create
            .Table("accounts")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable().Indexed()
            .WithColumn("email").AsString().NotNullable()
            .WithColumn("hash").AsString().NotNullable()
            .WithColumn("userid").AsGuid().NotNullable()
            .WithColumn("created").AsDateTime().NotNullable()
            .WithColumn("updated").AsDateTime().Nullable()
            .WithColumn("deleted").AsDateTime().Nullable();

        Create
            .ForeignKey("FK_accounts_userid")
            .FromTable("accounts").ForeignColumn("userid")
            .ToTable("users").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("accounts");
    }
}