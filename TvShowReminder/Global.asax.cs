using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using NLog;
using TvShowReminder.DatabaseMigrations;

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

            ISqlDatabaseMigrator sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            Logger.Info("Application startup complete");
        }

        protected void Application_Error()
        {
            Exception lastException = Server.GetLastError();
            Logger.Fatal(lastException);
        }
    }
}
