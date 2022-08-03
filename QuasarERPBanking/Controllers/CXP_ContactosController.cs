using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class CXP_ContactosController : ParentController
    {
        [HttpPost]
        //public ActionResult Create(CXP_CONTACTOS cxp_contactos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ViewBag.message = cxp_contactos.Create();


        //        return View("Edit", cxp_contactos);
        //    }
        //    else
        //    {
        //        var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
        //        return View("Create", cxp_contactos);
        //    }

        //}

        //public ActionResult Edit(CXP_CONTACTOS cxp_contactos)
        //{
        //    string message = string.Empty;
        //    if (ModelState.IsValid)
        //    {
        //        message = cxp_contactos.Edit();
        //    }
        //    else
        //    {
        //        message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
        //    }

        //    ViewBag.message = message;
        //    return View("Index");

        //}

        public JsonResult Create(CXP_CONTACTOS cxp_contactos)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_contactos.Create();
                message = message + "1"; 
            }
            else
            {                
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                message = message + "0";
            }
            return Json(message, JsonRequestBehavior.DenyGet);

        }
        public JsonResult Edit(CXP_CONTACTOS cxp_contactos)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_contactos.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet); 

        }

        //public ActionResult Delete(string cxpcontcod)
        //{
        //    string message = string.Empty;
        //    CXP_CONTACTOS cxp_contactos = new CXP_CONTACTOS();
        //    cxp_contactos.CXPCONTCOD = cxpcontcod;
        //    message = cxp_contactos.Eliminar();
        //    ViewBag.message = message;
        //    return View("Index");
        //}

        public JsonResult Delete(string cxpcontcod)
        {
            string message = string.Empty;
            CXP_CONTACTOS cxp_contactos = new CXP_CONTACTOS();
            cxp_contactos.CXPCONTCOD = cxpcontcod;
            message = cxp_contactos.Eliminar();

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet); ;

        }
        public JsonResult getContactos(string cxpcod)
        {
            List<CXP_CONTACTOS> lstContactos = CXP_CONTACTOS.ContactosPorCXPCOD(cxpcod);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                "CXPCONTNOM",
                "CXPCONTTEL",
                "CXPCONTFAX",
                "CXPCONTEMAIL",
                "CXPCONTDIR",
            };
            int cont = 0;
            foreach (CXP_CONTACTOS contacto in lstContactos)
            {
                //contacto.ToString();
                table.Append("<tr>");
                table.Append("<td>");
                table.Append("<a href='#' id='To' onclick='showContact("+ cont + ");'>");
                table.Append(contacto.CXPCONTCOD);
                table.Append("</a>");
                table.Append("</td>");
               
                foreach (string campo in arCampos)
                {
                    table.Append("<td>");
                    table.Append(contacto.GetType().GetProperty(campo).GetValue(contacto));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }



     



    }
}