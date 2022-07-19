using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    //[BreadCrumb]
    public class HomeController : ParentController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Descripcion de bla bla bla";

            return View("Contact");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public JsonResult getfechaQuasar(/*string term*/)
        {
            //List<string> lstCodigos = new INV_MAESTRO().GetProducto(term);
            string fechaQuasar = "       ";
            bool validfecha =bool.Parse( System.Configuration.ConfigurationManager.AppSettings["ValidarFecha"].ToString());

            clsAS400 cn = new clsAS400();
            if (validfecha)
            {
                if (!cn.Execute(ref fechaQuasar, "BANFOSIFOB", "CLQUA02", 7))
                {

                }
                else
                {
                    Util.FechaQuasar = fechaQuasar.Substring(5, 2) + "/" + fechaQuasar.Substring(3, 2) + "/" + "20" + fechaQuasar.Substring(1, 2);

                }
            }
            else
            { 
                fechaQuasar = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() +  "/"+ DateTime.Now.Year.ToString();
                Util.FechaQuasar = fechaQuasar.ToString();
            }
            return Json(fechaQuasar, JsonRequestBehavior.AllowGet);

        }

    }
}