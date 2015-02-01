using System.Configuration;
using Autofac;
using TvShowReminder.DatabaseMigrations;
using TvShowReminder.DataSource;

namespace TvShowReminder.Startup.Modules
{
    public class DataSourceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.Register(c => new DbConnectionHelper(connectionString)).As<IDbConnectionHelper>();
            builder.Register(c => new SqlDatabaseMigrator(connectionString)).As<ISqlDatabaseMigrator>();
            builder.RegisterType<SubscriptionCommandDataSource>().As<ISubscriptionCommandDataSource>();
            builder.RegisterType<SubscriptionQueryDataSource>().As<ISubscriptionQueryDataSource>();
            builder.RegisterType<EpisodeCommandDataSource>().As<IEpisodeCommandDataSource>();
            builder.RegisterType<EpisodesQueryDataSource>().As<IEpisodesQueryDataSource>();
        }
    }
}