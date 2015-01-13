using Autofac;
using TvShowReminder.Startup.Tasks;

namespace TvShowReminder.Startup.Modules
{
    public class StartupTaskModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseMigrationTask>().As<IStartable>();
        }
    }
}