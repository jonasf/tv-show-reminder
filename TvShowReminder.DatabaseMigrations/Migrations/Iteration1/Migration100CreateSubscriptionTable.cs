using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace TvShowReminder.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(100)]
    public class Migration100CreateSubscriptionTable : Migration
    {
        private const string TableName = "Subscription";

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsInt32().Identity()
                .WithColumn("TvShowId").AsInt32()
                .WithColumn("TvShowName").AsString(255)
                .WithColumn("LastAirDate").AsDate();

            Create.PrimaryKey(string.Format("PK_{0}", TableName))
                .OnTable(TableName)
                .Column("Id")
                .Clustered();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}