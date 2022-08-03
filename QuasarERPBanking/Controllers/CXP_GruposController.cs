using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class CXP_GruposController : ParentController
    {
        // GET: CXP_Maestro
        public ActionResult Index()
        {
            return View();
        }

        //PARA BUSCAR EL CODIGO Y NOMBRE
        public JsonResult getCodigos(string term)
        {
            List<string> lstCodigos = new CXP_GRUPOS().GetAutocGrupos(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }

        //AL CREAR BUSCA SI EL CODIGO EXISTE O NO 
        public JsonResult getCodigo(string term)
        {
            string valor = "";
            CXP_GRUPOS cxp_grupos = new CXP_GRUPOS();
            cxp_grupos.CXPCOD = term;
            if (cxp_grupos.Existe())
                valor = "1";
            else
                valor = "0";
            return Json(valor, JsonRequestBehavior.AllowGet);

        }

        // PARA LLENAR EL FORMULARIO
        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('-');

            CXP_GRUPOS lstCodigos = (CXP_GRUPOS)new CXP_GRUPOS().Buscar(codigo[0]);
            //List<CXP_GRUPOS> lstCodigos = (List <CXP_GRUPOS> ) new CXP_GRUPOS().Buscar(codigo[0]);

            return Json(lstCodigos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create(CXP_GRUPOS cxp_grupos)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = cxp_grupos.Create();


                return View("Edit", cxp_grupos);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", cxp_grupos);
            }

        }

        public ActionResult Edit(CXP_GRUPOS cxp_grupos)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_grupos.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", cxp_grupos);

        }

        public ActionResult Delete(string cxpcod)
        {
            string message = string.Empty;
            CXP_GRUPOS cxp_grupos = new CXP_GRUPOS();
            cxp_grupos.CXPCOD = cxpcod;
            message = cxp_grupos.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }

    }
}