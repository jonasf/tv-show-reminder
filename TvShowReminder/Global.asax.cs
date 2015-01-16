using System;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using NLog;
using TvShowReminder.DatabaseMigrations;
using TvShowReminder.Startup.Modules;

namespace TvShowReminder
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Logger.Info("Starting application");

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            SetupDependencies();
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
            Logger.Fatal(lastException);
        }

        private void SetupDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterSource(new ViewRegistrationSource());
            builder.RegisterFilterProvider();

            builder.RegisterModule(new DataSourceModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new StartupTaskModule());

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
