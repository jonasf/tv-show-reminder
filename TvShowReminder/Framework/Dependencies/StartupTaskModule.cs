using Autofac;
using TvShowReminder.Framework.StartupTasks;

namespace TvShowReminder.Framework.Dependencies
{
    public class StartupTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseMigrationTask>().As<IStartable>();
        }
    }
}