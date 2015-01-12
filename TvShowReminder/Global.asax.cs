using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using TvShowReminder.DatabaseMigrations;

namespace TvShowReminder
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ISqlDatabaseMigrator sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
