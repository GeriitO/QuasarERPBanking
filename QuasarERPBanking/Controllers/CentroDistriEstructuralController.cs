using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Resources;

namespace QuasarERPBanking.Controllers
{
    public class CentroDistriEstructuralController :  ParentController
    {
        // GET: CC_Maestro
        public ActionResult Index()
        {
          

            return View();
        }

        // GET: CC_Maestro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CC_Maestro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CC_Maestro/Create
        [HttpPost]
        public ActionResult Create(CentroDistriEstructural cc_maestro)
        {
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_maestro where cc_cod='" + cc_maestro.CC_COD + "'";
                ConectDB.QueryInsert = "insert into " + ParametrosGlobales.bd + "cc_maestro(cc_cod,cc_cteasoc,cc_nomb, cc_pert_estruc, cc_pert_geog, cc_ulthijo) values('" + cc_maestro.CC_COD.ToString() + "' , '" + cc_maestro.CC_CTEASOC.ToString() + "' , '" + cc_maestro.CC_NOMB.ToString() + "' , '" + cc_maestro.CC_PERT_ESTRUC.ToString() + "' , '" + cc_maestro.CC_PERT_GEOG.ToString() + "' , '1');";
                ConectDB.QueryInsert +="update " + ParametrosGlobales.bd  + "cc_maestro set cc_ulthijo=0 where cc_cod='" + cc_maestro.CC_PERT_ESTRUC.ToString() + "'";
                ViewBag.message = cc_maestro.Create();
                return View("Edit", cc_maestro);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", cc_maestro);
            }

        }

        // GET: CC_Maestro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CC_Maestro/Edit/5
        [HttpPost]
        public ActionResult Edit(CentroDistriEstructural cc_maestro)
        {
            cc_maestro.CC_COD = cc_maestro.CODEDIT;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_maestro where cc_cod='" + cc_maestro.CC_COD + "'";
                ConectDB.QueryInsert = "Update " + ParametrosGlobales.bd + "cc_maestro set cc_nomb='" + cc_maestro.CC_NOMB.ToString() + "' , cc_pert_estruc='" + cc_maestro.CC_PERT_ESTRUC.ToString() + "' , cc_pert_geog='" + cc_maestro.CC_PERT_GEOG.ToString() + "' , cc_cteasoc='" + cc_maestro.CC_CTEASOC.ToString() + "' where cc_cod='" + cc_maestro.CC_COD.ToString() + "';";
                ConectDB.QueryInsert += "update " + ParametrosGlobales.bd + "cc_maestro set cc_ulthijo=0 where cc_cod='" + cc_maestro.CC_PERT_ESTRUC.ToString() + "'";
                message = cc_maestro.Edit();
                //string [] sts = Request.UserHostAddressUserLanguages;
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Index", cc_maestro);

        }

        // GET: CC_Maestro/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CC_Maestro/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        [HttpPost]
        // para el dropdownlist de nota de entrega
        //public JsonResult getContactos_(string ctecod)
        public JsonResult getCCosto()
        {

            var ctecod = Request["codigo"];

            List<CentroDistriEstructural> lstContactos = CentroDistriEstructural.CCostoXCtesInt(ctecod);
            //// si se quiere crear el dropdownlist por el controlador
            //StringBuilder table = new StringBuilder();
            //int cont = 0;
            //foreach (CC_CTE_CONTACTOS contacto in lstContactos)
            //{
            //    table.Append("<option value=" + contacto.CTECONTCOD + "'>");
            //    table.Append(contacto.CTECONTNOM);
            //    table.Append("</option>");
            //    cont++;
            //}
            //return Json(table.ToString(), JsonRequestBehavior.AllowGet);

            return Json(lstContactos.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('-');
            //CC_MAESTRO lstCodigos = new CC_MAESTRO().getDescri(codigo[0].Trim());
            List<CentroDistriEstructural> lstCodigos = CentroDistriEstructural.getDescri(codigo[0].Trim());
            return Json(lstCodigos[0], JsonRequestBehavior.AllowGet);

        }

        public JsonResult getCodigos(string term)
        {
            List<string> lstCodigos = new CentroDistriEstructural().GetCCostos(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult Exist(string cod)
        {
            string lstCodigos = new CentroDistriEstructural().GetExist(cod);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }

        //////////////////////// falta el SP

        public ActionResult Delete(string ccod, string ccpert)
        {
            string message = string.Empty;
            CentroDistriEstructural cc_maestro = new CentroDistriEstructural();
            cc_maestro.CC_COD = ccod;
            if (Puede_Eliminar (ccod)==true )
            { 
                ConectDB.QueryInsert = "Delete from " + ParametrosGlobales.bd + "cc_maestro where cc_cod='" + ccod.ToString() + "';";
                ConectDB.QueryInsert += "Update " + ParametrosGlobales.bd + "cc_maestro set cc_ulthijo=(select case when (select count(*) from " + ParametrosGlobales.bd + "cc_maestro where cc_pert_estruc='" + ccpert  + "')=0 then 1 else 0 end) where cc_cod='" + ccpert  + "'" ;
                message = cc_maestro.Eliminar();
            }
            else
            {
                message= StringResources.msgDeleteFAIL;
            }
            ViewBag.message = message;
            return View("Index");

        }





        [HttpPost]
        public ActionResult Get_Tree()
        {
            var codigo = Request["codigo"];
            List<CentroDistriEstructural> afectado = CentroDistriEstructural.getree(codigo);
            return Json(afectado.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult getGrid(string codigo)
        {
            List<CentroDistriEstructural> lstFilas = CentroDistriEstructural.ListHijos(codigo);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                "CC_NOMB",
                "CC_DES_GEOGR",
                "CC_DES_CTEASOC",
                
            };
            int cont = 0;
            foreach (CentroDistriEstructural Filas in lstFilas)
            {
                //contacto.ToString();
                table.Append("<tr>");
                table.Append("<td style='width:16%'>");                                  
                table.Append("<a id='To' style = 'cursor:pointer;' onclick='editarcc(" + Filas.CC_COD + ");'>");
                table.Append(Filas.CC_COD);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td style='width:28%'>");
                    table.Append(Filas.GetType().GetProperty(campo).GetValue(Filas));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }

        public bool Puede_Eliminar(string CC)
        {
            bool resultado = true;
            CentroDistriEstructural ccm = new Models.CentroDistriEstructural();
            if (ccm.Existe("Select * from " + ParametrosGlobales.bd + "cc_transacciones where cc_cod='" +  CC + "'")==true )
            {
                resultado = false;
                return resultado;
            };
            if (ccm.Existe("Select * from " + ParametrosGlobales.bd + "cxp_tempdet where cc_cod='" + CC + "'") == true)
            {
                resultado = false;
                return resultado;
            };
            if (ccm.Existe("Select * from " + ParametrosGlobales.bd + "cxp_det where cc_cod='" + CC + "'") == true)
            {
                resultado = false;
                return resultado;
            };
            if (ccm.Existe("Select * from " + ParametrosGlobales.bd + "inv_transacciones where invtranscc='" + CC + "'") == true)
            {
                resultado = false;
                return resultado;
            };
            if (ccm.Existe("Select * from " + ParametrosGlobales.bd + "af_transacciones where aftransccosto='" + CC + "'") == true)
            {
                resultado = false;
                return resultado;
            };
            return resultado;
            
        }

    }
}
