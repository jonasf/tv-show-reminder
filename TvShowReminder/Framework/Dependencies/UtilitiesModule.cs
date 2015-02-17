using Autofac;

namespace TvShowReminder.Framework.Dependencies
{
    public class UtilitiesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CommandSender>().As<ICommandSender>();
            builder.RegisterType<QuerySender>().As<IQuerySender>();
        }
    }
}