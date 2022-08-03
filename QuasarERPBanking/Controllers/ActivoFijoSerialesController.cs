using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class ActivoFijoSerialesController : ParentController
    {
        //// GET: AF_Seriales
        public ActionResult Index()
        {
            return View();
        }

        // GET: AF_Seriales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AF_Seriales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AF_Seriales/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AF_Seriales/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AF_Seriales/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AF_Seriales/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AF_Seriales/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //para llenar el grid al consultar por serial
        public JsonResult getSerial(string af_serial)
        {
            List<ActivoFijoSeriales> lstSeriales = ActivoFijoSeriales.EtiquetasPorSerial(af_serial);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {

                "AF_SERIAL",
            };
            int cont = 0;
            foreach (ActivoFijoSeriales producto in lstSeriales)
            {
                //producto.ToString();
                table.Append("<tr>");
                table.Append("<td>");
                table.Append("<a href='/AF_ASIGNA/Buscar?codigo=" + producto.AF_ETIQ + "'>");
                //table.Append("@Html.ActionLink(\"" + producto.AF_ETIQ + "\", \"Buscar\", \"AF_ASIGNA\", new { id = \"123\" }, null)");              
                table.Append(producto.AF_ETIQ);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td>");
                    table.Append(producto.GetType().GetProperty(campo).GetValue(producto));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }





    }
}
