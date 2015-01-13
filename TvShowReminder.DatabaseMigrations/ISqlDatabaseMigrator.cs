namespace TvShowReminder.DatabaseMigrations
{
    public interface ISqlDatabaseMigrator
    {
        void MigrateToLatest();
    }
}