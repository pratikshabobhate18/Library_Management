using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Library_Management_Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_Error()
        {
            Exception ex = Server.GetLastError();

            string logFilePath = Server.MapPath("~/logs/errors.txt");

            // Ensure directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

            File.AppendAllText(logFilePath, $"{DateTime.Now:G} | {ex.Message}\n{ex.StackTrace}\n\n");

            Server.ClearError();
        }

    }
}
