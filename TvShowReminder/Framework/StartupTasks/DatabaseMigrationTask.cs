using Autofac;
using NLog;
using TvShowReminder.DatabaseMigrations;

namespace TvShowReminder.Framework.StartupTasks
{
    public class DatabaseMigrationTask : IStartable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ISqlDatabaseMigrator _sqlDatabaseMigrator;

        public DatabaseMigrationTask(ISqlDatabaseMigrator sqlDatabaseMigrator)
        {
            _sqlDatabaseMigrator = sqlDatabaseMigrator;
        }

        public void Start()
        {
            Logger.Info("Running database migration");
            _sqlDatabaseMigrator.MigrateToLatest();
            Logger.Info("Database migration completed");
        }
    }
}