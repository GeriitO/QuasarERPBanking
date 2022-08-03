using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using Resources;

namespace QuasarERPBanking.Controllers
{
    public class INV_TransaccionesController : ParentController
    {
        // GET: INV_TRANSACCIONES
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getProducto(string term)
        {
            List<string> lstProductos = new INV_TRANSACCIONES().GetCampos(term);
            return Json(lstProductos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult getProAutocom(string term)
        {


            INV_TRANSACCIONES lstProductos = (INV_TRANSACCIONES)new INV_TRANSACCIONES().Buscar(term);
            return Json(lstProductos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create(List<INV_TRANSACCIONES> listObject)//INV_TRANSACCIONES[] inv_transacciones
        {
            return View("Create");
        }




        [HttpPost]
        public ActionResult PostTrans()
        {


            var codigos = Request["codigo"].Split(',');
            var cantidades = Request["cantidad"].Split(',');
            var depositos = Request["deposito"].Split(',');
            var centrocos = Request["centroco"].Split(',');
            var fecha = Request["fecha"];
            var fechas = Request["fechamov"].Split(',');
            var costos = Request["costo"].Split(',');
            //var calcost= Request["costo"].Split(',');
            var referencias = Request["referencia"].Split(','); 
            //var departamentos = Request["departamento"].Split(',');
            var documentos = Request["documento"].Split(',');
            //var centros = Request["centro"].Split(',');
            var trans_tipo = Request["trans_tipo"];
            int indice = 0;

            string lote = INV_TRANSACCIONES.getLote();
            foreach (string codigo in codigos)
            {

                INV_TRANSACCIONES af_trans = new INV_TRANSACCIONES
                {
                    INVTRANSCOD = codigo,
                    INVTRANSDEP= int.Parse(depositos[indice]),
                    INVTRANSCC= centrocos[indice],
                    INVTRANSFECHAMOV = DateTime.Parse(fechas[indice]),
                    INVTRANSFECHA = DateTime.Parse(fecha),
                    INVTRANSTIPO = trans_tipo,
                    INVTRANSCANT = int.Parse(cantidades[indice]),
                    INVTRANSCOSTO = decimal.Parse(costos[indice].Replace(".", ",")),
                    //COSTOCAL = decimal.Parse(calcost[indice].Replace(".", ",")),
                    INVTRANSREF = referencias[indice],
                    //INVTRANSDEP = int.Parse(departamentos[indice]),
                    INVTRANSLOTE = lote,
                    INVTRANSDOC = documentos[indice],
                    //INVTRANSCC= centros[indice],
                    INVTRANSID = "0",
                    
                    

                };
                af_trans.Insert();
                indice++;
               
            }
            
            var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver

            return Json(resultado);

        }

        [HttpPost]
        public ActionResult a(INV_TRANSACCIONES inv_transacciones)
        {

            if (ModelState.IsValid)
            {
                ViewBag.message = inv_transacciones.Create();


                return View("Edit", inv_transacciones);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", inv_transacciones);
            }
         

            return View();
        }



        public ActionResult Edit(INV_TRANSACCIONES inv_transacciones)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = inv_transacciones.Edit();
                //string [] sts = Request.UserHostAddressUserLanguages;
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", inv_transacciones);
        }

        public ActionResult Delete(string translote)
        {
            string message = string.Empty;
            INV_TRANSACCIONES inv_transacciones = new INV_TRANSACCIONES();
            inv_transacciones.INVTRANSLOTE = translote;
            message = inv_transacciones.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }
    }
}
