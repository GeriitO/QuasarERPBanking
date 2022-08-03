using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Rows = System.Collections.Generic.List<object>;

namespace QuasarERPBanking.Controllers
{
    public class INV_FIFOController : Controller
    {
        // GET: INV_FIFO
        public ActionResult Index()
        {
            return View();
        }

        // GET: INV_FIFO/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: INV_FIFO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INV_FIFO/Create
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

        // GET: INV_FIFO/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: INV_FIFO/Edit/5
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

        // GET: INV_FIFO/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: INV_FIFO/Delete/5
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

        //para llenar el grid en inv_maestro
        public JsonResult getFifo(string invcod)
        {
            List<INV_FIFO> lstFifos = INV_FIFO.FifosDeINVCOD(invcod);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                //"INVCANT",
                "INVCOSTO",
                "VALORACION",
                "strINVFECHATRANS",
                "INVDEP",
            };
            int cont = 0;
            foreach (INV_FIFO fifo in lstFifos)
            {
                //producto.ToString();
                table.Append("<tr>");
                table.Append("<td id='td" + cont + "'>");
                table.Append(fifo.INVCANT);
                //table.Append("<a href='AF_ASIGNA/Buscar?codigo=" + producto.AF_ETIQ + "'>");
                ////table.Append("@Html.ActionLink(\"" + producto.AF_ETIQ + "\", \"Buscar\", \"AF_ASIGNA\", new { id = \"123\" }, null)");              
                //table.Append(producto.AF_ETIQ);
                //table.Append("</a>");
                //table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    //table.Append("<tr>");
                    //table.Append("<td id='td"+ cont +"'>");
                    table.Append("<td>");
                    table.Append(fifo.GetType().GetProperty(campo).GetValue(fifo));
                    table.Append("</td>");
                }
                table.Append("</td>");
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }

        //para el autocomplete de nota de entrega en inv_nemaestro
        public JsonResult getFifos(string codigo, string dep)
        {
            var cod = codigo.Split('-');
            var codigos = cod[0].Replace(" ", "");
            List<INV_FIFO> lstFifo = new INV_FIFO().GetAutocom(codigos, dep);
            //string lote = INV_FIFO.getLote(codigos, dep);
            return Json(lstFifo, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult Act_invFifo()
        {

            //int afectado = 0;
            var codigos = Request["codigo"].Split(',');
            var cantidades = Request["cantidad"].Split(',');
            var deposito = Request["deposito"];
            //var depositos = Request["unidad"].Split(',');
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(3);
            int indice = 0;
            int a = 0;
            foreach (string codigo in codigos)
            {                                                                            
                parametros.Add(codigo);
                parametros.Add(cantidades[indice]);
                parametros.Add(deposito);
                Rows reader = cn.execProc("SP_DEL_INV_FIFO", parametros, false);                                         
                indice++;
                parametros.Clear();
                a = cn.afectados;

            }
            //afectado = afectado + indice;
            /*var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver*/

            return Json(cn.afectados);

        }






    }
}
