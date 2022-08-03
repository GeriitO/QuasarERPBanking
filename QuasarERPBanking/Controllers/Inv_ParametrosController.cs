using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class Inv_ParametrosController : ParentController
    {
        // GET: Inv_Parametros
        public  ActionResult Index()
        {
            INV_PARAMETROS parametro = new INV_PARAMETROS();
            parametro.Buscar();


            return View(parametro);
        }

        // GET: Inv_Parametros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inv_Parametros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inv_Parametros/Create
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

        // GET: Inv_Parametros/Edit/5
        public ActionResult Edit(int id)
        {


            return View();
        }

        // POST: Inv_Parametros/Edit/5
        [HttpPost]
        //public ActionResult Edit(FormCollection collection)
        public ActionResult Index(INV_PARAMETROS inv_parametros)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (inv_parametros.Existe())
                    {

                        if (inv_parametros.Update() == 0)
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
            catch
            {
                return View();
            }
        }

        // GET: Inv_Parametros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inv_Parametros/Delete/5
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
