using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(11, "add plans table")]
public class NTS11 : Migration
{
    public override void Up()
    {
        Create
            .Table("plans")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("duration").AsInt32().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("locationid").AsGuid().NotNullable();
        
        Create
            .ForeignKey("FK_plans_locations")
            .FromTable("plans").ForeignColumn("locationid")
            .ToTable("locations").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("plans");
    }
}