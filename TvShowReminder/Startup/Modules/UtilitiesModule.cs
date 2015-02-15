using Autofac;
using TvShowReminder.Framework;

namespace TvShowReminder.Startup.Modules
{
    public class UtilitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandSender>().As<ICommandSender>();
        }
    }
}