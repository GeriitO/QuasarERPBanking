using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class ActivoFijoParametrosController : ParentController
    {
        // GET: AF_Parametros
        public ActionResult Index()
        {
            ActivoFijoParametros parametro = new ActivoFijoParametros();
            parametro.Buscar();


            return View(parametro);
        }


        [HttpPost]
        //public ActionResult Edit(FormCollection collection)
        public ActionResult Index(ActivoFijoParametros af_parametros)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (af_parametros.Existe())
                    {

                        if (af_parametros.Update() == 0)
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
