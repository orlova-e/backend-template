using FluentMigrator;

namespace Template.Infrastructure.Migrator.Migrations;

[Migration(6, "add notifications table")]
public class NTS6 : Migration
{
    public override void Up()
    {
        Create
            .Table("notifications")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("remindat").AsDateTime2().NotNullable()
            .WithColumn("isrecurring").AsBoolean().NotNullable()
            .WithColumn("interval").AsCustom("interval").NotNullable()
            .WithColumn("times").AsInt32().Nullable()
            .WithColumn("text").AsString().Nullable();
        
        Create
            .ForeignKey("FK_notifications_notebases_id")
            .FromTable("notifications").ForeignColumn("id")
            .ToTable("notebases").PrimaryColumn("id");
    }

    public override void Down()
    {
        Delete
            .Table("notifications");
    }
}