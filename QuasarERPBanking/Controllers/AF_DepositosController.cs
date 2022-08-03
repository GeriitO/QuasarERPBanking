using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarFPMWeb.Models;

namespace QuasarFPMWeb.Controllers
{
    public class AF_DepositosController : ParentController
    {
        // GET: AF_TIPOS

        public ActionResult Index()
        {

            return View(AF_DEPOSITOS.GetDeposito());
        }

        // GET: AF_TIPOS/Edit/5
        public ActionResult Edit(string id)
        {
            AF_DEPOSITOS deposito = new AF_DEPOSITOS();
            return View((AF_DEPOSITOS)deposito.Buscar(id));
        }



        // GET: AF_TIPOS/Delete/5
        public ActionResult Delete(string id)
        {
            AF_DEPOSITOS deposito = new AF_DEPOSITOS();
            return View((AF_DEPOSITOS)deposito.Buscar(id));
        }

        //// POST: AF_TIPOS/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                AF_DEPOSITOS deposito = new AF_DEPOSITOS();
                deposito.AFDEPCOD = id;
                ConectDB.QueryInsert = "Delete  from " + ParametrosGlobales.bd + " " + deposito.tabla + "  where " + deposito.Campo_Clave + "='" + deposito.AFDEPCOD + "'";
                message = deposito.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", AF_DEPOSITOS.GetDeposito());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(AF_DEPOSITOS depositos)
        {

            AF_DEPOSITOS tipo = new AF_DEPOSITOS();
            try
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "" + tipo.tabla + " where " + tipo.Campo_Clave + "='" + depositos.AFDEPCOD + "' ";
                if (ModelState.IsValid)
                {
                    if (!depositos.Existe(ConectDB.QueryExiste))
                    {
                        if (depositos.Insert() == -2)
                        {
                            return View("Edit", depositos);
                        }
                        else
                        {
                            return View("Edit", depositos);
                        }
                    }
                    else
                    {

                        ViewBag.message = Resources.Inventario.StringResources.lblCodExist;
                        //return RedirectToAction("Index");
                        return View("Index", AF_TIPO.GetTipo());
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
        public ActionResult Edit(AF_DEPOSITOS depositos)
        {
            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "" + depositos.tabla + " where " + depositos.Campo_Clave + "='" + depositos.AFDEPCOD + "' ";
                    if (depositos.Existe(ConectDB.QueryExiste))
                    {

                        if (depositos.getParametersUpd() != 0)
                        {

                        }
                        else
                        {
                            ViewBag.message = "el codigo ingresado no existe";

                        }
                    }
                    //ViewBag.TCosto = INV_PARAMETROS.TCosto();
                    return View("Edit", depositos);
                }

            }
            catch (Exception ex)
            {
                return View();
            }



            return View("Index", AF_DEPOSITOS.GetDeposito());


        }



    }
}
