using QuasarERPBanking.Models;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class INV_NemaestroController : ParentController
    {
        // GET: INV_nemaestro
        public ActionResult Index()
        {
            return View();
        }

        // GET: INV_nemaestro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: INV_nemaestro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INV_nemaestro/Create
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

        // GET: INV_nemaestro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: INV_nemaestro/Edit/5
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

        // GET: INV_nemaestro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: INV_nemaestro/Delete/5
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

       

        ////////////////// para inv_temp

        [HttpPost]
        public ActionResult Act_invTemp()
        {
       

            var codigo = Request["codigo"];
            var cantidad = decimal.Parse(Request["cantidad"].ToString());
            var cliente = Request["cliente"];
            var deposito = Request["deposito"];
            int opc= int.Parse(Request["opc"]);



            string lote = INV_NEMAESTRO.getTemp(codigo, cantidad, cliente, deposito, opc);



            var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver

            //return Json(resultado);
            return Json(lote);

        }

        [HttpPost]
        public JsonResult getNePend()
        {
            var usuario = Util.ContactoActual();         
            List<INV_NEMAESTRO> lstActivos = new INV_NEMAESTRO().GetPendientes(usuario);
            return Json(lstActivos.ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Ne_maestro()
        {

       
            var contacto = Request["contacto"];
            var lugar = Request["lugar"];
            var cliente = Request["cliente"];
            var observaciones = Request["observaciones"];
            var ccosto = Request["ccosto"];
            var fecha = Request["fecha"];

            int afectado = INV_NEMAESTRO.insertne(contacto, lugar, cliente, observaciones, ccosto, fecha);
            //if (afectado == -1)
            //{  
            //    //fue exitosa
            //    int transaccion = INV_NETRANSACCIONES.insertrans(contacto, lugar, cliente, observaciones, ccosto, fecha);             
            //}
            //else {
            //    //hubo un error
            //}      
            //var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver
            //return Json(resultado);
            return Json(afectado);

        }

    }
}
