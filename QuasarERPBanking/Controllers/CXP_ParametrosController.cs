using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class CXP_ParametrosController : ParentController
    {
        // GET: CC_Parametros

        public ActionResult Index()
        {
            CXP_PARAMETROS parametro = new CXP_PARAMETROS();
            parametro.Buscar();


            return View(parametro);
        }


        [HttpPost]
        //public ActionResult Edit(FormCollection collection)
        public ActionResult Index(CXP_PARAMETROS cxp_parametros)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (cxp_parametros.Existe())
                    {

                        if (cxp_parametros.Update() == 0)
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


        public JsonResult getGrid()
        {
            List<CXP_PARAMETROS> lstActividades = CXP_PARAMETROS.llenarGrid();

            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                   "CPPPARNOMB",
                   "CPPPARLOTE",
                   "CPPPARTRANSID",
                   "CPPPARACTID",                                   
                   //"CPPPARINTMG",
                    "MAYORGENR",
                   "CPPPARDIRMG",                  
                   //"CPPPARINTCC",
                    "CENTROCOST",
                   "CPPPARDIRCC",
                   //"CPPPARINTIMP",
                    "IMPUESTO",
                   "CPPPARDIRIMP",
                   "CPPUNIDTRIB"
            };
            int cont = 0;
            foreach (CXP_PARAMETROS actividad in lstActividades)
            {


                table.Append("<tr>");
                foreach (string campo in arCampos)
                {
                    table.Append("<td style='width: 90px'>");
                    table.Append(actividad.GetType().GetProperty(campo).GetValue(actividad));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }



    }
}
