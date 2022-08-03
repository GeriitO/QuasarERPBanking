using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class CXP_ACTIVIDADESController : ParentController
    {

        [HttpPost]
        public JsonResult Create(CXP_ACTIVIDADES cxp_actividad)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_actividad.Create();

            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                StringBuilder messages = new StringBuilder();
                
                foreach(ModelError error in allErrors)
                {
                    messages.Append( error.ErrorMessage + "\n");
                }
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                message += messages.ToString();
            }
            return Json(message, JsonRequestBehavior.DenyGet);

        }
        public JsonResult Edit(CXP_ACTIVIDADES cxp_actividad)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_actividad.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet);

        }



        public JsonResult Delete(string cxpactid)
        {
            string message = string.Empty;
            CXP_ACTIVIDADES cxp_actividad = new CXP_ACTIVIDADES();
            cxp_actividad.ACTID = cxpactid;
            message = cxp_actividad.Eliminar();

            ViewBag.message = message;
            return Json(message, JsonRequestBehavior.DenyGet); ;

        }

        public JsonResult getActividades(string cxpcod)
        {
            List<CXP_ACTIVIDADES> lstActividades = CXP_ACTIVIDADES.ActividadesPorCXPCOD(cxpcod);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                    "ACT_AUTOR",
                    "strACTFECHCREA",
                    //"ACT_CXPCOD",
                    "strACTFECHREAL",
                    "ACT_ASUNTO",
                    "ACT_CUERPO",
                    //"ACT_PERSOUR",
                    "ACT_PERSCOMP",
                    //"ACT_DESC",
                    //"ACT_CORSRES",
                    //"ACT_CORATN",
                    //"ACT_CORTPO",
                    //"ACT_ADJ"
            };
            int cont = 0;
            foreach (CXP_ACTIVIDADES actividad in lstActividades)
            {
                table.Append("<tr>");
                table.Append("<td>");
                table.Append("<a href='#' id='To' onclick='showActivities(" + cont + ");'>");
                table.Append(actividad.ACTID);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td>");
                    table.Append(actividad.GetType().GetProperty(campo).GetValue(actividad));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}