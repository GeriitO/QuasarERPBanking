using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class CentroCostoParametrosController : ParentController
    {
        // GET: CC_Parametros

        public ActionResult Index()
        {
            CentroCostoParametros parametro = new CentroCostoParametros();
            parametro.Buscar();


            return View(parametro);
        }


        [HttpPost]
        //public ActionResult Edit(FormCollection collection)
        public ActionResult Index(CentroCostoParametros cc_parametros)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_parametros";
                    ConectDB.QueryInsert = "update " + ParametrosGlobales.bd + "cc_parametros set cteparnomb='" + cc_parametros.CTEPARNOMB.ToString() + "' , cteparactid='" + cc_parametros.CTEPARACTID.ToString() + "' , cc_idtrans='" + cc_parametros.CC_IDTRANS.ToString() + "' , ccparintmg='" + Convert.ToInt32(Convert.ToBoolean(cc_parametros.CCPARINTMG.ToString()))  + "' , ccpardirmg='" + cc_parametros.CCPARDIRMG.ToString() + "' ";
                    if (cc_parametros.Existe())
                    {

                        if (cc_parametros.Update() == 0)
                        {

                        }
                    }
                    else
                    {
                        ViewBag.message = "el codigo ingresado no existe";

                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: AF_Parametros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AF_Parametros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AF_Parametros/Create
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

        // GET: AF_Parametros/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AF_Parametros/Edit/5
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

        // GET: AF_Parametros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AF_Parametros/Delete/5
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
    }
}
