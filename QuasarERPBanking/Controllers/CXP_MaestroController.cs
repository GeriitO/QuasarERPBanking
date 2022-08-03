using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Reflection;

namespace QuasarERPBanking.Controllers
{

    public class CXP_MaestroController : ParentController
    {
        // GET: CXP_Maestro
        public ActionResult Index()
        {
            return View();
        }
        
        //PARA BUSCAR EL CODIGO Y NOMBRE
        public ActionResult getCodigos(string term)
        {
            List<CXP_MAESTRO> lstCodigos = new CXP_MAESTRO().GetProveedores2(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult getCodigos2(string term)
        {
            List<string> lstCodigos = new CXP_MAESTRO().GetProveedores3(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }

        //AL CREAR BUSCA SI EL CODIGO EXISTE O NO 
        public JsonResult getCodigo(string term)
        {
            string valor = "";
            CXP_MAESTRO cxp_maestro = new CXP_MAESTRO();
            cxp_maestro.CXPCOD = term;
            if (cxp_maestro.Existe())
                valor = "1";
            else
                valor = "0";
            return Json(valor, JsonRequestBehavior.AllowGet);

        }

            // PARA LLENAR EL FORMULARIO
        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('-');

            CXP_MAESTRO lstCodigos = (CXP_MAESTRO)new CXP_MAESTRO().Buscar(codigo[0]);
            //List<CXP_MAESTRO> lstCodigos = (List <CXP_MAESTRO> ) new CXP_MAESTRO().Buscar(codigo[0]);

            return Json(lstCodigos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create(CXP_MAESTRO cxp_maestro)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = cxp_maestro.Create();


                return View("Edit", cxp_maestro);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", cxp_maestro);
            }

        }

        public  ActionResult Edit(CXP_MAESTRO cxp_maestro)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                message = cxp_maestro.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", cxp_maestro);
           
        }

        public ActionResult Delete(string cxpcod)
        {
            string message = string.Empty;
            CXP_MAESTRO cxp_maestro = new CXP_MAESTRO();
            cxp_maestro.CXPCOD = cxpcod;
            message = cxp_maestro.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }

        public override ActionResult Buscar(string codigo)
        {
            CXP_MAESTRO modelo = new CXP_MAESTRO();
            PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
            propertyInfo.SetValue(modelo, codigo, null);
            modelo.BuscarOrig(codigo);

            return View("Edit", modelo);

        }

        //public override ActionResult BuscarCot(string codigo)
        //{

        //    //List<CXP_CONTACTOS> lstContactos = CXP_CONTACTOS.ContactosPorCXPCOD(cxpcod);
        //    //List<COT_MAESTRO> Provee = new List<COT_MAESTRO>();
        //    //string[] codigos = codigo.Split(',');
        //    List<string> codigos = codigo.Split(',').ToList();
        //    //foreach (string[] elemento in codigos )
        //    //{
        //    //    Provee = elemento;
        //    //}


        //    COT_MAESTRO modelo = new COT_MAESTRO();
        //    PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
        //    propertyInfo.SetValue(modelo, codigos, null);
        //    modelo.BuscarCot(codigos);

        //    return View("EditCot", modelo);

        //}



    }
}