using System.Configuration;
using Autofac;
using TvShowReminder.DatabaseMigrations;

namespace TvShowReminder.Startup.Modules
{
    public class DataSourceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            builder.Register(c => new SqlDatabaseMigrator(connectionString)).As<ISqlDatabaseMigrator>();
        }
    }
}