using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QuasarERPBanking
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static BackgroundWorker worker;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            string culture = Models.ParametrosGlobales.GetCulture();
            Models.CONFIG.setResource(culture);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            Models.Util.loadFormats();

            Models.ParametrosGlobales.UsersLastAlive = new System.Collections.Concurrent.ConcurrentDictionary<string, DateTime>();

            var razorViewEngine = new RazorViewEngine();
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(razorViewEngine);
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());




        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            //routes.MapRoute(
            //   "CentroCosto", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "CentroCosto", action = "CentroCosto", id = UrlParameter.Optional } // Parameter defaults
            //);
        }

        private static void IncWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
            {
                //DUERME POR 10 HORAS Y CORRE EL PROCESO DE INCLUSION DE NUEVOS CONTACTOS
                //(solo corre sino esta en horario laborable)
                //System.Threading.Thread.Sleep(3600000*10);
                //worker.RunWorkerAsync();
            }
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            //If background worker process is running then clean up that object.
            if (worker != null)
                worker.CancelAsync();
        }

        void Session_Start(object sender, EventArgs e)
        {
            /* Code that runs when a new session is started 
             */
            if (Session["LoginUserName"] != null)
            {
                //Redirect to Welcome Page if Session is not null  
                Response.Redirect("/Home/Index");

            }
            else
            {
                //Redirect to Login Page if Session is null & Expires   
                Response.Redirect("/Usuario/logOn");

            }
        }
        void Session_End(object sender, EventArgs e)
        {
            // Response.Redirect("/Usuario/logOn");
            // Code that runs when a session ends.   
            // Note: The Session_End event is raised only when the sessionstate mode  
            // is set to InProc in the Web.config file. If session mode is set to StateServer   
            // or SQLServer, the event is not raised.  

        }
    }
}
