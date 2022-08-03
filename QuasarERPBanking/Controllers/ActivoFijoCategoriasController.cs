using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class ActivoFijoCategoriasController : ParentController
    {
        // GET: AF_TIPOS

        public ActionResult Index()
        {

            return View(ActivoFijoCategorias.GetCategoria());
        }

        // GET: AF_TIPOS/Edit/5
        public ActionResult Edit(string id)
        {
            ActivoFijoCategorias categoria = new ActivoFijoCategorias();
            return View((ActivoFijoCategorias)categoria.Buscar(id));
        }



        // GET: AF_TIPOS/Delete/5
        public ActionResult Delete(string id)
        {
            ActivoFijoCategorias categoria = new ActivoFijoCategorias();
            return View((ActivoFijoCategorias)categoria.Buscar(id));
        }

        //// POST: AF_TIPOS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                ActivoFijoCategorias categoria = new ActivoFijoCategorias();
                categoria.AFGRUCOD = id;
                message = categoria.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", ActivoFijoCategorias.GetCategoria());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(ActivoFijoCategorias categorias)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = categorias.Create();
                    //return RedirectToAction("Index");
                    return View("Index", ActivoFijoCategorias.GetCategoria());
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
        public ActionResult Edit(ActivoFijoCategorias categorias)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = categorias.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(categorias);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", ActivoFijoCategorias.GetCategoria());


        }



    }
}
