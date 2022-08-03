using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class INV_TipoController : ParentController
    {
        // GET: INV_Tipo
        public ActionResult Index()
        {

            return View(INV_TIPO.GetListTipo());
        }

        // GET: AF_TIPOS/Edit/5
        public ActionResult Edit(string id)
        {
            INV_TIPO tipo = new INV_TIPO();
            return View((INV_TIPO)tipo.Buscar(id));
        }



        // GET: AF_TIPOS/Delete/5
        public ActionResult Delete(string id)
        {
            INV_TIPO tipo = new INV_TIPO();
            return View((INV_TIPO)tipo.Buscar(id));
        }

        //// POST: AF_TIPOS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                INV_TIPO tipo = new INV_TIPO();
                tipo.INVTIPCOD = id;
                message = tipo.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", INV_TIPO.GetListTipo());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(INV_TIPO tipos)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = tipos.Create();
                    //return RedirectToAction("Index");
                    return View("Index", INV_TIPO.GetListTipo());
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
        public ActionResult Edit(INV_TIPO tipos)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = tipos.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(tipos);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", INV_TIPO.GetListTipo());


        }
    }
}
