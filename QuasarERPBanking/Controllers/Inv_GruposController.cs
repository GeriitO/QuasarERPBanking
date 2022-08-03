using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarWeb.Models;

namespace QuasarWeb.Controllers
{
    public class Inv_GruposController : ParentController
    {

        // GET: CXP_ESTADO
        public ActionResult Index()
        {

            return View(INV_GRUPOS.GetGrupo());
        }


        public ActionResult Edit(int id)
        {
            INV_GRUPOS grupo = new INV_GRUPOS();
            return View((INV_GRUPOS)grupo.Buscar(id.ToString()));
        }


        public ActionResult Delete(int id)
        {
            INV_GRUPOS grupo = new INV_GRUPOS();
            return View((INV_GRUPOS)grupo.Buscar(id.ToString()));
        }

        //// POST: CXP_ESTADO/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                string message = string.Empty;
                INV_GRUPOS grupo = new INV_GRUPOS();
                grupo.INVGRUCOD = id;
                message = grupo.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", INV_GRUPOS.GetGrupo());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(INV_GRUPOS grupo)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = grupo.Create();
                    //return RedirectToAction("Index");
                    return View("Index", INV_GRUPOS.GetGrupo());
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
        public ActionResult Edit(INV_GRUPOS grupo)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = grupo.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(grupo);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", INV_GRUPOS.GetGrupo());


        }

        // para cambiar de activar a dasativar
        public ViewResult ActivarDesactivar(int id)
        {
            INV_GRUPOS grupos = new INV_GRUPOS();
            grupos.Buscar(id.ToString());
            grupos.ACTIVO = !grupos.ACTIVO;
            grupos.Update();
            return View(grupos);
        }




    }
}
