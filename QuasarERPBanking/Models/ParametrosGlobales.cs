using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace QuasarERPBanking.Models
{
    public class ParametrosGlobales
    {
        //public const string csDesarrollo = "server=10.112.2.103;uid=ecasa;pwd=Equipa2011;database=q_correspondencia";
        //public const string csCalidad = "server=10.112.2.43;uid=q_correspondencia;pwd=C0rr3spondencia;database=q_correspondencia";
        //public const string csProduccion = "";

        /// <summary>
        /// Debe contener la fecha del pase a producción o certificación (o en su defecto la fecha del último cambio subido a desarrollo)
        /// </summary>
        public const string Version = "2014-07-02";

        public static string ConnString;//Se arma al llamar a IniciarConexion() --> Toma los valores del web.config

        public static string ConnStringAS400;

        public static string Ambiente = "";

        public static int LogOffTimeout = /*30;*/ 1800;//en segundos

        public static int KeepAliveTime = 60;//en segundos

        public static bool AutenticacionUtilizaActiveDirectory = false;//Se cambia automaticamente a true si en el web.config estan presentes los parametros de conexion con el AD

        public static int CierreAutomatico = 0;//días que deben pasar para que las guías no entregadas sean cerradas automáticamente por el sistema (si es 0 no se hace el cierre automático)

        public static ConcurrentDictionary<string, DateTime> UsersLastAlive;//Contiene los usuarios activos y la fecha-hora de la ultima actividad

        //public static SystemMonitor systemMonitor;

        //public static Menu MenuUsuarioSinSesion = null;
        public static Menu MenuUsuarioSinSesion = null;

        public static string Organizacion = "", UrlSalida = "/Usuario/LogOff";

        public static bool MostrarHeaderFooter = false;

        public static string culture;

        public static string bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];

        public static string bd2 = System.Configuration.ConfigurationManager.AppSettings["bdCn"];
        public static string dfmt = System.Configuration.ConfigurationManager.AppSettings["DateFormat"];
        public static bool IniciarConexion()
        {
            //Se leen los datos de conexion del AD en caso de ser necesario


            bool successful = true;
            string usuario = "", clave = "";
            string servidor = "", bd = "";

            try
            {
                //Se toman los parametros de conexion desde el archivo "Web.config"
                servidor = System.Configuration.ConfigurationManager.AppSettings["servidor"];
                bd = System.Configuration.ConfigurationManager.AppSettings["bd"];
                usuario = System.Configuration.ConfigurationManager.AppSettings["usuario"];
                clave = System.Configuration.ConfigurationManager.AppSettings["clave"];

                if (servidor == null || bd == null || usuario == null || clave == null)
                {
                    throw new Exception("Alguno de los valores de conexión es null");
                }
            }
            catch (Exception)
            {
                //MyLogger.Error("Error leyendo los parámetros de conexión a Base de Datos en el Web.config");
                successful = false;
            }

            if (servidor.Contains("10.112.2.103"))//DESARROLLO
            {
                Ambiente = "DESARROLLO"; //usuario = "ecasa"; clave = "Equipa2011";
            }
            else if (servidor.Contains("10.112.2.43"))//CALIDAD
            {
                Ambiente = "CALIDAD"; //usuario = "q_correspondencia"; clave = "q_correspondencia";
            }
            else //PRODUCCION
            {
                Ambiente = "PRODUCCION";
            }

            if (successful) MyLogger.Info("Conectado al ambiente de {0} desde {1}", Ambiente, System.Net.Dns.GetHostName());

            ConnString = "server=" + servidor + ";uid=" + usuario + ";pwd=" + clave + ";database=" + bd;

            return successful;
        }
        public static bool IniciarConexionAS400()
        {
            //Se leen los datos de conexion del AD en caso de ser necesario


            bool successful = true;
            string usuario = "", clave = "";
            string servidor = "", bd = "";

            try
            {
                //Se toman los parametros de conexion desde el archivo "Web.config"
                servidor = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
                bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];
                usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
                clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];

                if (servidor == null || bd == null || usuario == null || clave == null)
                {
                    throw new Exception("Alguno de los valores de conexión es null");
                }
            }
            catch (Exception)
            {
                MyLogger.Error("Error leyendo los parámetros de conexión a Base de Datos AS400 en el Web.config");
                successful = false;
            }

            /*   if (servidor.Contains("10.112.2.103"))//DESARROLLO
               {
                   Ambiente = "DESARROLLO"; //usuario = "ecasa"; clave = "Equipa2011";
               }
               else if (servidor.Contains("10.112.2.43"))//CALIDAD
               {
                   Ambiente = "CALIDAD"; //usuario = "q_correspondencia"; clave = "q_correspondencia";
               }
               else //PRODUCCION
               {
                   Ambiente = "PRODUCCION";
               }
           */
            // if (successful) MyLogger.Info("Conectado al ambiente de {0} desde {1}", Ambiente, System.Net.Dns.GetHostName());

            ConnStringAS400 = "server=" + servidor + ";uid=" + usuario + ";pwd=" + clave + ";database=" + bd;

            return successful;
        }
        public static bool IniciarParametrosGlobales()
        {
            bool successful = true;

            try
            {
                //Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                //Se toman los parametros globales desde el archivo "Web.config"
                Organizacion = System.Configuration.ConfigurationManager.AppSettings["organizacion"];
                UrlSalida = System.Configuration.ConfigurationManager.AppSettings["urlSalida"];
                string mostrarHeaderFooterCAD = System.Configuration.ConfigurationManager.AppSettings["mostrarHeaderFooter"];

                if (Organizacion == null)
                {
                    MyLogger.Warning("No se encontró el parámetro 'Organizacion'");
                    Organizacion = "_";
                }
                else
                    MyLogger.Info("Organizacion: {0}", Organizacion);

                if (UrlSalida == null)
                {
                    MyLogger.Warning("No se encontró el parámetro 'UrlSalida'");
                    Organizacion = "#";
                }
                else
                    MyLogger.Info("URL Salida: {0}", UrlSalida);

                if (mostrarHeaderFooterCAD == null)
                {
                    MyLogger.Warning("No se encontró el parámetro 'mostrarHeaderFooter'");
                }
                else
                {
                    if (mostrarHeaderFooterCAD == "1" ||
                        mostrarHeaderFooterCAD == "true" ||
                        mostrarHeaderFooterCAD == "True")
                    {
                        MostrarHeaderFooter = true;
                    }

                    MyLogger.Info("MostrarHeaderFooter: {0}", MostrarHeaderFooter);
                }
            }
            catch (Exception)
            {
                MyLogger.Error("Error leyendo los parámetros globales en el Web.config");
                successful = false;
            }

            //if (successful) MyLogger.Info("Inicializando Parámetros Globales:\nOrganización: {0}\nURL Salida: {1}", Organizacion, UrlSalida);

            return successful;
        }

        public static string GetCulture()
        {
            string culture= string.Empty;
            
            ConectDB cn = new ConectDB();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execute("CONFIG", parametros, false);
           

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    
                    culture = item["CULTURE"].ToString();
                }
            }


            return culture;
        }

        public static List<string> GetCultures()
        {
            List<string> cultures = new List<string>();

            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc("SP_GET_CULTURES", parametros, false);


            if (reader != null)
            {
                string culture = string.Empty;
                foreach (Dictionary<string, object> item in reader)
                {

                    culture = item["CULTURE"].ToString();
                    cultures.Add(culture);
                }
            }


            return cultures;
        }
    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_GET_CONFIG
//SP_GET_CULTURES
