using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class CXP_GContactosController : ParentController
    {
        [HttpPost]
        public JsonResult Create(CXP_GCONTACTOS cxp_gcontactos)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_gcontactos.Create();
                message = message + "1";
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                message = message + "0";
            }
            return Json(message, JsonRequestBehavior.DenyGet);

        }
        public JsonResult Edit(CXP_GCONTACTOS cxp_gcontactos)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_gcontactos.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet);

        }

   
        public JsonResult Delete(string cxpcontcod)
        {
            string message = string.Empty;
            CXP_GCONTACTOS cxp_gcontactos = new CXP_GCONTACTOS();
            cxp_gcontactos.CXPCONTCOD = cxpcontcod;
            message = cxp_gcontactos.Eliminar();

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet); ;

        }
        public JsonResult getContactos(string cxpcod)
        {
            List<CXP_GCONTACTOS> lstContactos = CXP_GCONTACTOS.ContactosPorCXPCOD(cxpcod);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                "CXPCONTNOM",
                "CXPCONTTEL",
                "CXPCONTFAX",
                "CXPCONTEMAIL",
                "CXPCONTDIR",
            };
            int cont = 0;
            foreach (CXP_GCONTACTOS contacto in lstContactos)
            {
                //contacto.ToString();
                table.Append("<tr>");
                table.Append("<td>");
                table.Append("<a href='#' id='To' onclick='showContact(" + cont + ");'>");
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
