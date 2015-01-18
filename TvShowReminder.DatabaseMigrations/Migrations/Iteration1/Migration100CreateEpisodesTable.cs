using FluentMigrator;
using FluentMigrator.Runner.Extensions;

namespace TvShowReminder.DatabaseMigrations.Migrations.Iteration1
{
    [Migration(101)]
    public class Migration100CreateEpisodesTable : Migration
    {
        private const string TableName = "Episodes";

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsInt32().Identity()
                .WithColumn("SubscriptionId").AsInt32()
                .WithColumn("SeasonNumber").AsInt32()
                .WithColumn("EpisodeNumber").AsInt32()
                .WithColumn("Title").AsString(255)
                .WithColumn("AirDate").AsDate();

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