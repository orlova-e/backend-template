using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(12, "add features table")]
public class NTS12 : Migration
{
    public override void Up()
    {
        Create
            .Table("features")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("isdeleted").AsBoolean().NotNullable()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("duration").AsInt32().NotNullable()
            .WithColumn("price").AsDecimal().NotNullable()
            .WithColumn("planid").AsGuid().NotNullable();
        
        Create
            .ForeignKey("FK_features_plans")
            .FromTable("features").ForeignColumn("planid")
            .ToTable("plans").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("features");
    }
}