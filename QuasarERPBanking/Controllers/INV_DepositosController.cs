using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class INV_DepositosController : ParentController
    {

        //para el autocomplete de depositos en inv_transacciones
        public JsonResult getDepositos(string term)
        {
            List<string> lstActivos = new INV_DEPOSITOS().GetCampos(term);
            return Json(lstActivos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult Index()
        {

            return View(INV_DEPOSITOS.GetDeposito());
        }

        // GET: INV_DEPOSITOS/Edit/5
        public ActionResult Edit(string id)
        {
            INV_DEPOSITOS deposito = new INV_DEPOSITOS();
            return View((INV_DEPOSITOS)deposito.Buscar(id));
        }



        // GET: INV_DEPOSITOS/Delete/5
        public ActionResult Delete(string id)
        {
            INV_DEPOSITOS deposito = new INV_DEPOSITOS();
            return View((INV_DEPOSITOS)deposito.Buscar(id));
        }

        //// POST: INV_DEPOSITOS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                INV_DEPOSITOS deposito = new INV_DEPOSITOS();
                deposito.INVDEPCOD = id;
                message = deposito.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", INV_DEPOSITOS.GetDeposito());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(INV_DEPOSITOS grupos)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = grupos.Create();
                    //return RedirectToAction("Index");
                    return View("Index", INV_DEPOSITOS.GetDeposito());
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
        public ActionResult Edit(INV_DEPOSITOS grupos)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = grupos.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(grupos);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", INV_DEPOSITOS.GetDeposito());


        }
    }
}
