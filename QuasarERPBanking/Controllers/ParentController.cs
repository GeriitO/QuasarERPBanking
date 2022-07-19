using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Reflection;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class ParentController : Controller
    {
        //public CorrespondenciaDBContext db = new CorrespondenciaDBContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string Action = filterContext.ActionDescriptor.ActionName;
            string Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            //Se verifica que el usuario haya iniciado la sesion
            if (Models.Util.ContactoActual() == null//true si no ha iniciado sesion
               && (Controller != "Usuario" && Action != "LogOn")
               && !(Controller == "Home" && Action == "Faq")
                && Controller != "WebAPI")//Para poder entrar al Monitor del sistema

            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary { { "Controller", "Usuario" }, { "Action", "LogOn" } });
                return;
            }
            Menu menu = null;
            if (Util.ContactoActual() != null)
            {
                if (Session["menu"] != null)
                {
                    menu = (Menu)Session["menu"];
                }
                else
                {
                    menu = new Menu(Util.ContactoActual());
                    HttpContext.Session.Add("menu", menu);
                }
            }

            if (!Request.IsAjaxRequest())//SI la peticion es AJAX no se arma el menu
            {
                //menu = MenuActual();
                ViewBag.menu = menu;
            }

            //SI tiene permiso (menu == null solo si la peticion es ajax)
            /*if (menu != null
                && Controller != "WebAPI" //Para poder entrar al Monitor del sistema
                && !menu.TieneAcceso(Controller, Action)) //Si NO tiene acceso
            {
                setAlertMessage("No tiene permiso para acceder a esta función");
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" } });
            }

            //Si el objeto AlertMessage no existe en sesión se agrega
            if (Session["alertMessage"] == null)
            {
                HttpContext.Session.Add("alertMessage", new AlertMessage());
            }*/
            ViewBag.color = Properties.Settings.Default.BackGroundColor;
            /////////////////////Properties.Settings.Default.BackGroundColor = "red";
            Properties.Settings.Default.Save();
            //Properties.Settings.Default.Upgrade();
            ViewBag.hasSales = System.Configuration.ConfigurationManager.AppSettings["hasSales"];

        }
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);

        //    string Action = filterContext.ActionDescriptor.ActionName;
        //    string Controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

        //    //Se verifica que el usuario haya iniciado la sesion
        //    if (Models.Util.ContactoActual() == null//true si no ha iniciado sesion
        //       && (Controller != "Usuario" && Action != "LogOn")
        //       && !(Controller == "Home" && Action == "Faq")
        //        && Controller != "WebAPI")//Para poder entrar al Monitor del sistema

        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //            new System.Web.Routing.RouteValueDictionary { { "Controller", "Usuario" }, { "Action", "LogOn" } });
        //        return;
        //    }
        //    else
        //    {
        //        if (Models.Util.ContactoActual() != null)
        //        {
        //            setCurrentUser(Models.Util.ContactoActual());
        //        }


        //    }

        //    Menu menu = null;

        //    if (!Request.IsAjaxRequest())//SI la peticion es AJAX no se arma el menu
        //    {
        //        menu = MenuActual();
        //        ViewBag.menu = menu;
        //    }

        //    //SI tiene permiso (menu == null solo si la peticion es ajax)
        //    if (menu != null
        //        && Controller != "WebAPI" //Para poder entrar al Monitor del sistema
        //        && !menu.TieneAcceso(Controller, Action)) //Si NO tiene acceso
        //    {
        //        setAlertMessage("No tiene permiso para acceder a esta función");
        //        filterContext.Result = new RedirectToRouteResult(
        //            new System.Web.Routing.RouteValueDictionary { { "Controller", "Home" }, { "Action", "Index" } });
        //    }

        //    //Si el objeto AlertMessage no existe en sesión se agrega
        //    if (Session["alertMessage"] == null)
        //    {
        //        HttpContext.Session.Add("alertMessage", new AlertMessage());
        //    }
        //}
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            if (Models.Util.ContactoActual() != null)//Si ha iniciado sesion
            {
                //Se agrega o actualiza en la estructura para saber la ultima vez que tuvo la sesion activa
                string usuario = Util.ContactoActual();
                ParametrosGlobales.UsersLastAlive.AddOrUpdate(usuario, DateTime.Now, (s, i) => DateTime.Now);


                ViewBag.footer = Util.ContactoActual(); /*+ " - Rol(es): " + RolesActualesString() +
                                " - Departamento o Agencia: " + ClienteInternoNombreActual()*/


                ViewBag.rolActual = RolesActualesString();
                ViewBag.clienteInternoActualID = ClienteInternoIDActual();

                //Extrae solo el ROL mas importante (en el orden administrador, seguridad, acopio, agencia, normal)

                //string rolImportante = "Ninguno";



                ViewBag.rolImportante = ""; // " - " + rolImportante;
            }
            else
            {
                if (ParametrosGlobales.Ambiente.StartsWith("D") || //DESARROLLO 
                    ParametrosGlobales.Ambiente.StartsWith("C")) //CALIDAD
                {
                    ViewBag.footer = ParametrosGlobales.Ambiente;
                }
            }

            //Si el objeto AlertMesssage existe en la sesion se setea
            if (Session["alertMessage"] != null)
            {
                ViewBag.AlertMessage = (AlertMessage)Session["alertMessage"];
            }
        }

        /// <summary>
        /// Mensaje a ser mostrado en la vista del usuario
        /// </summary>
        /// <param name="msg"></param>
        protected void setAlertMessage(string msg)
        {
            AlertMessage am = (AlertMessage)Session["alertMessage"];
            if (am != null)
                am.Message = msg;
        }




        /// <summary>
        /// Retorna el Contacto del usuario que ha iniciado sesion.
        /// </summary>
        /// <returns>
        /// El Contacto del usuario que ha iniciado la sesion. En caso de no haber iniciado sesion retorna null.
        /// </returns>
        public Models.Usuario UsuarioActual()
        {
            return (Models.Usuario)Session["currentuser"];
        }

        /// <summary>
        /// Se invoca 
        /// </summary>
        /// <param name="usuario">el usuario que ha iniciado sesion</param>

        public void setCurrentUser(Models.Usuario usuario)
        {
            HttpContext.Session.Add("currentuser", usuario);

            HttpContext.Session.Add("menu", new Menu(usuario.usuario));
            //PostTracker.Properties.Settings
        }


        /// <summary>
        /// Retorna el Menu del usuario que ha iniciado sesion.
        /// </summary>
        /// <returns>
        /// El Menu del usuario que ha iniciado la sesion. En caso de no haber iniciado sesion retorna null.
        /// </returns>
        public Models.Menu MenuActual()
        {
            Menu m = (Models.Menu)Session["menu"];
            if (m == null)//El usuario no ha iniciado sesión
            {
                m = ParametrosGlobales.MenuUsuarioSinSesion;
            }
            return m;
        }



        public long UsuarioIDActual()
        {
            //return ((CorrespondenciaCodeFirst.Models.Contacto)Session["contactoActual"]).ID;
            return 0;
        }

        public long ClienteInternoIDActual()
        {
            //return ((CorrespondenciaCodeFirst.Models.Contacto)Session["contactoActual"]).ClienteInternoID;
            return 0;
        }


        public string RolesActualesString()
        {
            return (string)Session["rolActual"];
        }



        /* public void RegistrarEnLog(string actividad, string mensaje, TiposEvento te = TiposEvento.Consulta,
             bool exitoso = true, string usuario = null, string fecha = null, long contactoAfectadoID = 0)
         {
             string evento = "Consulta";
             switch (te)
             {
                 case TiposEvento.Eliminacion: evento = "Eliminación"; break;
                 case TiposEvento.Inclusion: evento = "Inclusión"; break;
                 case TiposEvento.Modificacion: evento = "Modificación"; break;
             }

             Registro reg = new Registro
             {
                 Actividad = actividad,
                 ContactoID = ContactoActual() == null ? 0 : UsuarioIDActual(),
                 Fecha = fecha == null ? Util.FechaHoraActual() : fecha,
                 IP = Request.UserHostAddress,
                 Mensaje = usuario != null ?
                             "El usuario " + usuario + " " + mensaje :
                             "El usuario " + ContactoActual().UsuarioAD + " " + mensaje,
                 TipoEvento = evento,
                 Exitoso = exitoso,
                 ContactoAfectadoID = contactoAfectadoID,
                 ContactoAfectado = null
             };
             db.Registros.Add(reg);
             db.SaveChanges();
         }

 *          */
        protected override void OnException(ExceptionContext filterContext)
        {

            string usuario = Util.ContactoActual();

            MyLogger.Error("EXCEPTION (" + usuario + "): " +
                            filterContext.Exception.Message +
                            "\r\nInnerException:\r\n" + filterContext.Exception.InnerException +
                            "\r\nStacktrace:\r\n" + filterContext.Exception.StackTrace);
            base.OnException(filterContext);
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        #region ACTIONS GENERALES PARA LOS CONTROLADORES
        //public virtual ActionResult Indexx(string nom)
        //{

        //    return View(/*"~/Views/CXP/CXP_maestro.cshtml"*/);
        //}
        //public virtual ActionResult Index()
        //{

        //    return View(/*"~/Views/CXP/CXP_maestro.cshtml"*/);
        //}


        [HttpPost]
        public virtual ActionResult Edit_(clsMain cxp_maestro)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_maestro.Edit();

            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", cxp_maestro);
        }
        //[HttpGet]
        //public ActionResult Edit()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        data.Update();
        //    }
        //    return View("Edit", cxp_maestro);
        //}




        [HttpPost]
        public ActionResult Create_(clsMain cxp_maestro)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = cxp_maestro.Create();


                return View("Edit", cxp_maestro);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

                // Breakpoint, Log or examine the list with Exceptions.
                return View("Create", cxp_maestro);
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete_(string cxpcod)
        {
            clsMain cxp_maestro = new clsMain();
            PropertyInfo propertyInfo = cxp_maestro.GetType().GetProperty(cxp_maestro.Campo_Clave);
            propertyInfo.SetValue(cxp_maestro, cxpcod, null);
            //cxp_maestro.CXPCOD = cxpcod;
            int result = cxp_maestro.Delete();
            if (result != 0)
            {
                if (result == -2)
                {
                    ViewBag.message = "El registro NO ha sido eliminado ";
                }
                else
                {
                    ViewBag.message = "Registro eliminado satisfactoriamente";
                }

            }
            else
            {

            }

            return View("Index");

        }
        public virtual ActionResult Buscar(string codigo)
        {
            ConectDB modelo = new ConectDB();
            PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
            propertyInfo.SetValue(modelo, codigo, null);
            modelo.Buscar(codigo);

            return View("Edit", modelo);

        }

        public virtual ActionResult BuscarProv(string codigo, string provee)
        {
            clsMain modelo = new clsMain();
            PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
            propertyInfo.SetValue(modelo, codigo, null);
            modelo.Buscar();

            return View("Edit", modelo);

        }
        #endregion



        //public virtual ActionResult BuscarCot(string selected)
        //{
        //    List<clsMain> Provee = new List<clsMain>();
        //    PropertyInfo propertyInfo = Provee.GetType().GetProperty(Provee.Campo_Clave);
        //    propertyInfo.SetValue(Provee, selected, null);
        //    Provee.BuscarCot(selected);

        //    return View("EditCot", Provee);

        //}



        //public virtual ActionResult Buscar2(string codigo)
        //{


        //    //CXP_ORDENPAGO modelo = new CXP_ORDENPAGO();
        //    //PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
        //    //propertyInfo.SetValue(modelo, codigo, null);
        //    //modelo.Buscar3(codigo);

        //    //return View("Edit", modelo);

        //}


        }
}

