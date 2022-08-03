using QuasarERPBanking.Controllers;
using QuasarERPBanking.Models;
using Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class ActivoFijoTransaccionesController : ParentController
    {
        // GET: ActivoFijoTransacciones
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getProducto(string term)
        {
            List<string> lstProductos = new ActivoFijoTransacciones().GetCampos(term);
            return Json(lstProductos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult getProAutocom(string term)
        {


            ActivoFijoTransacciones lstProductos = (ActivoFijoTransacciones)new ActivoFijoTransacciones().Buscar(term);
            return Json(lstProductos, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult Create(List<ActivoFijoTransacciones> listObject)//ActivoFijoTransacciones[] ActivoFijoTransacciones
        {
            return View("Create");
        }





        [HttpPost]
        public ActionResult PostTrans()
        {

            var codigos = Request["codigo"].Split(',');
            var cantidades = Request["cantidad"].Split(',');
            var fecha = Request["fecha"];
            var fechas = Request["fechamov"].Split(',');
            var costos = Request["costo"].Split(',');         
            var referencias = Request["referencia"].Split(',');
            var documentos = Request["documento"].Split(',');
            var trans_tipo = Request["trans_tipo"];
            int indice = 0;

            ActivoFijoTransacciones NMROLOTE = new ActivoFijoTransacciones().getLote();

            string lote = NMROLOTE.AFPARLOTE_PROD;
            foreach (string codigo in codigos)
            {

                ActivoFijoTransacciones af_trans = new ActivoFijoTransacciones
                {
                    TRANS_PROD = codigo,
                    TRANS_CANT = int.Parse(cantidades[indice]),
                    TRANS_FECHAMOV = DateTime.Parse(fechas[indice]),
                    TRANS_FECHA = DateTime.Parse(fecha),                
                    TRANS_MONTO = decimal.Parse(costos[indice].Replace(".",",")),
                    TRANS_REF = referencias[indice],
                    TRANS_DOC = documentos[indice],
                    TRANS_LOTE = lote,
                    TRANS_ID = "0",
                    TRANS_TIPO = trans_tipo,

                };
                af_trans.Insert();
                indice++;

            }

            var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver

            return Json(resultado);

        }





        [HttpPost]

      public ActionResult Create(ActivoFijoTransacciones ActivoFijoTransacciones)
        {

            if (ModelState.IsValid)
            {
                ViewBag.message = ActivoFijoTransacciones.Create();


                return View("Edit", ActivoFijoTransacciones);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", ActivoFijoTransacciones);
            }               
        }
        


        public ActionResult Edit(ActivoFijoTransacciones ActivoFijoTransacciones)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = ActivoFijoTransacciones.Edit();
                //string [] sts = Request.UserHostAddressUserLanguages;
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", ActivoFijoTransacciones);
        }

        public ActionResult Delete(string translote)
        {
            string message = string.Empty;
            ActivoFijoTransacciones ActivoFijoTransacciones = new ActivoFijoTransacciones();
            ActivoFijoTransacciones.TRANS_LOTE = translote;
            message = ActivoFijoTransacciones.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }
    }
}
