using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using QuasarERPBanking.Models;
using System.Linq;

namespace QuasarERPBanking.Controllers
{
    public class UsuarioController : ParentController
    {
        //
        // GET: /Usuario/

        public ActionResult Index()
        {
            return RedirectToAction("LogOn", "Usuario");
        }

        //
        // GET/POST: /Usuario/LogOn

        public ActionResult LogOn()
        {
            try
            {
                if (Util.ContactoActual() != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                System.Collections.Specialized.NameValueCollection Form = HttpContext.Request.Form;
                if (Form.Count > 0)
                {
                    string usuario = Form["usuario"], clave = Form["clave"];
                    string det_cc;

                    if (usuario != null && usuario.Contains("@"))
                    {
                        usuario = usuario.Substring(0, usuario.IndexOf("@"));
                    }

                    bool valido = false;

                    valido = ValidarUsuarioClave(usuario, clave, out det_cc);

                    if (valido)
                    {
                        Session["LoginUserName"] = usuario;
                        Util.contacto = usuario.ToUpper();
                        Util.det_cc = det_cc;
                        ViewBag.DET_CC = det_cc;
                        //return RedirectToAction("pro_maestro", "pro_maestro");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        setAlertMessage("La clave o usuario es inválido.");
                    }
                }
            }
            //catch (System.DirectoryServices.AccountManagement.PrincipalException pe)
            catch (Exception pe)
            {
                setAlertMessage("Ha ocurrido un problema de conexión con el Active Directory. Espere un momento e intente de nuevo.");
                MyLogger.Exception(pe);
            }

            return View();
        }

        //
        // GET: /Usuario/LogOff



        public ActionResult LogOff()
        {
            if (Models.Util.ContactoActual() != null)
            {
                //RegistrarEnLog("Cierre de sesión", "ha cerrado sesión.");

                //Se saca el contacto de la estructura de los que ha iniciado sesion
                DateTime fecha = new DateTime();
                ParametrosGlobales.UsersLastAlive.TryRemove(Util.ContactoActual(), out fecha);
            }
            AlertMessage am = (AlertMessage)Session["alertMessage"];//Se extrae el alertMessage de la sesión
            HttpContext.Session.Clear();
            Util.FechaQuasar = "";
            if (am != null)
            {
                //Se limpia la sesión
                if (am.HayMensaje())                                    //SI existe un mensaje pendiente
                {
                    ViewBag.AlertMessage = am;                              //se agrega a la vista para mostrarse
                }
            }
            Util.contacto = null;
            return RedirectToAction("LogOn", "Usuario");
        }

        protected bool ValidarUsuarioClave(string usuario, string clave, out string det_cc)
        {
            const short DATA_SIZE = 50;
            byte[] data = new byte[DATA_SIZE];
            byte[] result;

            bool aux = false;
            det_cc = "";

            Models.ConectDB  cn = new Models.ConectDB();

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(3);
            string whereParam = "";
            parametros.Add(usuario);
            //Hash.SHA cl = new Hash.SHA();
            SHA1 cl = new SHA1CryptoServiceProvider();
            //string pwd = cl.SHA(clave);
            data = Encoding.Default.GetBytes(clave);
            result = cl.ComputeHash(data);
            string pwd = BitConverter.ToString(result);
            pwd = pwd.Replace("-", "");
            parametros.Add(pwd);
            whereParam ="UPPER ( USUARIO ) = UPPER ( '"+ usuario + "' ) and CLAVE = '"+ pwd + "'";
            List<object> reader = cn.execute2("Q_USUARIOS", whereParam, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    det_cc = item["CTEINT"].ToString();
                    ViewBag.DET_CC = det_cc;
                    aux = true;
                }
            }
            else
            {
                det_cc = "";
            }
            return aux;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //Todos los filtros deben estar definidos en ParentController
        }

        protected override void Dispose(bool disposing)
        {

            base.Dispose(disposing);
        }

        protected new void setAlertMessage(string msg)
        {
            AlertMessage am = (AlertMessage)Session["alertMessage"];
            if (am != null)
                am.Message = msg;
        }

        public JsonResult UserAutocomplete(string term)
        {
            List<string> lstUsers = new Usuario().GetUsers(term);
            return Json(lstUsers.ToList(), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult Get_Submod()
        {
            var usuario = Util.ContactoActual();
            List<OPC> afectado = OPC.getOpcUser2(usuario);        
            return Json(afectado.ToList(), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult Get_Level3()
        {
            //var usuario = Util.ContactoActual();
            List<OPC> opciones = OPC.getOpcLevel3();
            return Json(opciones.ToList(), JsonRequestBehavior.AllowGet);

        }



    }
}