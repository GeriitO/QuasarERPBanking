using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class INV_ZonasController : ParentController
    {
        // GET: INV_Zonas
        public ActionResult Index()
        {

            return View(INV_ZONAS.GetListZona());
        }

        // GET: AF_TIPOS/Edit/5
        public ActionResult Edit(string id)
        {
            INV_ZONAS zona = new INV_ZONAS();
            return View((INV_ZONAS)zona.Buscar(id));
        }



        // GET: AF_TIPOS/Delete/5
        public ActionResult Delete(string id)
        {
            INV_ZONAS zona = new INV_ZONAS();
            return View((INV_ZONAS)zona.Buscar(id));
        }

        //// POST: AF_TIPOS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                INV_ZONAS zona = new INV_ZONAS();
                zona.INVZONCOD = id;
                message = zona.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", INV_ZONAS.GetListZona());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(INV_ZONAS zonas)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = zonas.Create();
                    //return RedirectToAction("Index");
                    return View("Index", INV_ZONAS.GetListZona());
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(INV_ZONAS zonas)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = zonas.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(zonas);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", INV_ZONAS.GetListZona());


        }
    }
}
