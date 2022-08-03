using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{

    public class ActivoFijoTipoController : ParentController
    {
        // GET: ActivoFijoTipoS

        public ActionResult Index()
        {

            return View(ActivoFijoTipo.GetTipo());
        }

        // GET: ActivoFijoTipoS/Edit/5
        public ActionResult Edit(string id)
        {
            ActivoFijoTipo tipo = new ActivoFijoTipo();
            return View((ActivoFijoTipo)tipo.Buscar(id));
        }



        // GET: ActivoFijoTipoS/Delete/5
        public ActionResult Delete(string id)
        {
            ActivoFijoTipo tipo = new ActivoFijoTipo();
            return View((ActivoFijoTipo)tipo.Buscar(id));
        }

        //// POST: ActivoFijoTipoS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {

                string message = string.Empty;
                ActivoFijoTipo tipo = new ActivoFijoTipo();
                tipo.AFTIPCOD = id;
                ConectDB.QueryInsert = "Delete  from " + ParametrosGlobales.bd + " " + tipo.tabla + "  where " + tipo.Campo_Clave + "='" + tipo.AFTIPCOD + "'";
                message = tipo.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", ActivoFijoTipo.GetTipo());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(ActivoFijoTipo tipos)
        {
            ActivoFijoTipo tipo = new ActivoFijoTipo();
            try
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "" + tipo.tabla + " where " + tipo.Campo_Clave + "='" + tipos.AFTIPCOD + "' ";
                if (ModelState.IsValid)
                {
                    if (!tipos.Existe(ConectDB.QueryExiste))
                    {
                        if (tipos.Insert() == -2)
                        {
                            return View("Edit", tipos);
                        }
                        else
                        {
                            return View("Edit", tipos);
                        }
                    }
                    else
                    {

                        ViewBag.message = Resources.Inventario.StringResources.lblCodExist;
                        //return RedirectToAction("Index");
                        return View("Index", ActivoFijoTipo.GetTipo());
                    }
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
        public ActionResult Edit(ActivoFijoTipo tipos)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    //if (tipos.Existe())
                    //{

                    //    if (tipos.getParametersUpd() != 0)
                    //    {

                    //    }
                    //    else
                    //    {
                    //        ViewBag.message = "el codigo ingresado no existe";

                    //    }
                    //}
                    //ViewBag.TCosto = INV_PARAMETROS.TCosto();
                    return View("Edit", tipos);
                }

            }
            catch (Exception ex)
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", ActivoFijoTipo.GetTipo());
        }








    }
}
