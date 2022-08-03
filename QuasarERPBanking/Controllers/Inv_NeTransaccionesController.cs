using QuasarERPBanking.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class Inv_NeTransaccionesController : ParentController
    {
        // GET: Inv_NeTransacciones
        public ActionResult Index()
        {
            return View();
        }

        // GET: Inv_NeTransacciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inv_NeTransacciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inv_NeTransacciones/Create
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

        // GET: Inv_NeTransacciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Inv_NeTransacciones/Edit/5
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

        // GET: Inv_NeTransacciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inv_NeTransacciones/Delete/5
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

        [HttpPost]
        public ActionResult gridnemaestro()
        {

            int afectado = 0;
            var codigos = Request["codigo"].Split(',');
            var cantidades = Request["cantidad"].Split(',');
            var fecha = Request["fecha"];
            //var depositos = Request["unidad"].Split(',');

            int indice = 0;
     
            string lote = INV_NETRANSACCIONES.getLote();
            foreach (string codigo in codigos)
            {

                INV_NETRANSACCIONES in_netrans = new INV_NETRANSACCIONES
                {
                    NETRANSINVCOD = codigo,
                    NETRANSCANT = int.Parse(cantidades[indice]),
                    NETRANSOCELAB = lote,
                    NETRANSAPR ="-",
                    NETRANSOCCOD="-",
                    NETRANSID="0",
                    NETRANSCOSTO=0,
                    NETRANSFECHARECIBE= DateTime.Parse(fecha),
                    

                };
                afectado = afectado + in_netrans.Insert();
                indice++;

            }
            afectado = afectado + indice;
            /*var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver*/

            return Json(afectado);

        }



    }
}
