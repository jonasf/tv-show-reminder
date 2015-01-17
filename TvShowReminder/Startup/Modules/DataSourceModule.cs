using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            IDbConnection dbConnection = new SqlConnection(connectionString);
            
            builder.Register(c => new SqlDatabaseMigrator(connectionString)).As<ISqlDatabaseMigrator>();
            builder.Register(c => new SubscriptionCommandDataSource(dbConnection)).As<ISubscriptionCommandDataSource>();
            builder.Register(c => new SubscriptionQueryDataSource(dbConnection)).As<ISubscriptionQueryDataSource>();
        }
    }
}