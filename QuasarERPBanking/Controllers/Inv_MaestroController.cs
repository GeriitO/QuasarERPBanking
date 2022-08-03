using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    //public class InventarioController : ParentController
    public class Inv_MaestroController : ParentController
    {
        // GET: Inv_Maestro
        public  ActionResult Index()
        {
            ViewBag.TCosto = INV_PARAMETROS.TCosto();
            return View(/*"~/Views/Inv/Inv_Maestro.cshtml"*/);
        }

        [ActionName("getGrupos")]
        public JsonResult getGrupos(string term)
        {
            var lstGrupos = new Models.INV_GRUPOS().getGrupos(term);
            return Json(lstGrupos.ToList(), JsonRequestBehavior.AllowGet);

        }



        [HttpGet]
        public ActionResult Edit() { 
            return View(); }


        [HttpPost]
        public ActionResult Edit(Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                if (inventario.Existe())
                {

                    if (inventario.getParametersUpd() == 0)
                    {

                    }
                }
                else
                {
                    ViewBag.message = "el codigo ingresado no existe";

                }
            }
            ViewBag.TCosto = INV_PARAMETROS.TCosto();
            return View("Edit", inventario);
        }
        //[HttpGet]
        //public ActionResult Edit()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        data.Update();
        //    }
        //    return View("Edit", inv_maestro);
        //}
        [HttpPost]
        public ActionResult Create(Inventario inventario)        
{
            //ConectDB.QueryInsert = "Insert into " + ParametrosGlobales.bd + "Inventario(INVNOM ,INVDES ,INVREF ,INVGRU ,INVTIP ,INVUBI ,INVDEP ,INVZON ,INVMAR ,INVMOD ,INVSER ,INVUNI ,INVMIN ,INVMAX ,INVEXI ,INVIVA ,INVPREMAY ,INVPREPROM ,INVPREDET ,INVCOSANT ,INVCOSPRO ,INVCOSACT ,INVCOSVAL ,INVREO ,INVORD ,INVRES ,INVFIS ,INVCOD ,INVCANTREQ ,INVCTAGTO ,INVCTAINV ,INVDISP ,INVSERD ,INVSERO ,INVVAL ,INVADJ ,INVISLR ,INVGASTO ,INVMARCAJE)";
            //ConectDB.QueryInsert += " values('" + inventario.INVDES.ToString() + "' ,'" + inventario.INVREF.ToString() + "' ,'" + inventario.INVGRU.ToString() + "' ,'" + inventario.INVTIP.ToString() + "' ,'" + inventario.INVUBI.ToString() + "' ,'" + inventario.INVDEP.ToString() + "' ,'" + inventario.INVZON.ToString() + "' ,'" + inventario.INVMAR.ToString() + "' ,'" + inventario.INVMOD.ToString() + "' ,'" + inventario.INVSER.ToString() + "' ,'" + inventario.INVUNI.ToString() + "' ,'" + inventario.INVMIN.ToString() + "' ,'" + inventario.INVMAX.ToString() + "' ,'" + inventario.INVEXI.ToString() + "' ,'" + inventario.INVIVA.ToString() + "' ,'" + inventario.INVPREMAY.ToString() + "' ,'" + inventario.INVPREPROM.ToString() + "' ,'" + inventario.INVPREDET.ToString() + "' ,'" + inventario.INVCOSANT.ToString() + "' ,'" + inventario.INVCOSPRO.ToString() + "' ,'" + inventario.INVCOSACT.ToString() + "' ,'" + inventario.INVCOSVAL.ToString() + "' ,'" + inventario.INVREO.ToString() + "' ,'" + inventario.INVORD.ToString() + "' ,'" + inventario.INVRES.ToString() + "' ,'" + inventario.INVFIS.ToString() + "' ,'" + inventario.INVCOD.ToString() + "' ,'" + inventario.INVCANTREQ.ToString() + "' ,'" + inventario.INVCTAGTO.ToString() + "' ,'" + inventario.INVCTAINV.ToString() + "' ,'" + inventario.INVDISP.ToString() + "' ,'" + inventario.INVSERD.ToString() + "' ,'" + inventario.INVSERO.ToString() + "' ,'" + inventario.INVVAL.ToString() + "' ,'" + inventario.INVADJ.ToString() + "' ,'" + inventario.INVISLR.ToString() + "' ,'"+inventario.INVGASTO  + "' ,'" + inventario.INVMARCAJE.ToString() + "' )";
            if (ModelState.IsValid)
            {
                if (!inventario.Existe())
                {


                    if (inventario.Insert() == -2)
                    {
                        return View("Edit", inventario);
                    }
                    else
                    {
                        return View("Edit", inventario);
                    }
                }
                else
                {
                    ViewBag.message = Resources.Inventario.StringResources.lblCodExist;
                    return View("Edit", inventario);
                }
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));

                // Breakpoint, Log or examine the list with Exceptions.
                return View("Create", inventario);
            }

        }

        //public new ActionResult Create()
        //{
        //    Util.FechaQuasar = "12";
        //    return View();
        //}


        public ActionResult Delete(string invcod)
        {
            string message = string.Empty;
            Inventario inventario = new Inventario();
            inventario.INVCOD = invcod;

            bool resultado = inventario.Existe("Select * from " + ParametrosGlobales.bd + "INV_MAESTRO where INVCOD='" + invcod + "'");

            if (resultado == false)
            {
                message = "No se puede eliminar este centro de costo, ya que existen registros asociados a el";
            }
            else
            {
                ConectDB.QueryInsert = "Delete from " + ParametrosGlobales.bd + "INV_MAESTRO where INVCOD='" + invcod.ToString() + "'";
                message = inventario.Eliminar();
            }

            ViewBag.message = message;
            return View("Index");

        }


        public ActionResult Delete2(string invcod)
        {
            ConectDB.QueryInsert = "Delete from " + ParametrosGlobales.bd + "INV_MAESTRO where cc_cod='" + invcod.ToString() + "'";
            Inventario Inventario = new Inventario();
            Inventario.INVCOD = invcod;
            Inventario.Delete();

            return View("Index");

        }



        public ActionResult Buscar(string invcod)
        {
            Inventario inventario = new Inventario();
            inventario.INVCOD = invcod;
            inventario.Buscar();

            return View("Edit", inventario);

        }

       

        [ActionName("getCodigos")]
        public JsonResult getCodigos(string term)
        {
            List<string> lstCodigos = new Inventario().GetProducto(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }
        

        [ActionName("getProductos")]
        public JsonResult getProductos(string term, string dep)
        {
            List<string> lstCodigos = new Inventario().GetProducXdep(term, dep);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }




        public JsonResult getCodigo(string term)
        {
            string valor = "";
            Inventario Inventario = new Inventario();
            Inventario.INVCOD = term;
            if (Inventario.Existe())
                valor = "1";
            else
                valor = "0";
            return Json(valor, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('-');

            List<Inventario> lstCodigos = new Inventario().GetProdAutoCom(codigo[0]);

            return Json(lstCodigos.ToList().First(), JsonRequestBehavior.AllowGet);

        }

    }
}