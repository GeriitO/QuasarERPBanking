using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;
using System.Reflection;
using System.Collections;
using Rows = System.Collections.Generic.List<object>;
using Resources;


namespace QuasarERPBanking.Controllers
{
    public class ActivoFijoController : ParentController
    {
        // GET: AF_ASIGNA
        public ActionResult Index()
        {
            return View();
        }

        //para el autocomplete de etiquetas
        public JsonResult getActivo(string term)
        {
            List<string> lstActivos = new ActivoFijo().GetCampos(term);
            return Json(lstActivos.ToList(), JsonRequestBehavior.AllowGet);

        }


        //para el autocomplete de codigos
        public JsonResult getCodigo(string term)
        {
            List<string> lstCodigos = new ActivoFijo().GetCamposCod(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }




        //// para multiple etiqueta
        //    public JsonResult getCantidad(string af_codact, string cantidad)
        //    {
        //        List<ActivoFijo> lstActivos = ActivoFijo.MultipleEtiqueta(af_codact, cantidad);
        //        return Json(lstActivos.ToList(), JsonRequestBehavior.AllowGet);

        //    }


        public JsonResult getActivos(string afcod)
        {
            List<ActivoFijo> lstActivos = new ActivoFijo().ProductosPorAFCODACT(afcod);
            return Json(lstActivos.ToList(), JsonRequestBehavior.AllowGet);

        }


        //para llenar el grid en af_maestro
        public JsonResult getProductos(string af_codact)
        {
            List<ActivoFijo> lstProductos = new ActivoFijo().ProductosPorAFCODACT(af_codact);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                "AF_SERIAL",
                "AF_CCTO",
                "strAFFECH",
                "strAFFECHREAL",
            };
            int cont = 0;
            foreach (ActivoFijo producto in lstProductos)
            {
                //producto.ToString();
                table.Append("<tr>");
                table.Append("<td style='text-align:center'>");
                table.Append("<a href='/ActivoFijo/Buscar?codigo=" + producto.AF_ETIQ + "'>");
                //table.Append("@Html.ActionLink(\"" + producto.AF_ETIQ + "\", \"Buscar\", \"ActivoFijo\", new { id = \"123\" }, null)");              
                table.Append(producto.AF_ETIQ);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td style='text-align:center'>");
                    table.Append(producto.GetType().GetProperty(campo).GetValue(producto));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }








        //PARA BUSCAR ETIQUETA
        public JsonResult getActAutocom(string term)

        {
            clsAS400 cn = new clsAS400();
            //ActivoFijo lstCodigos = (ActivoFijo)new ActivoFijo().Buscar(term);
            ActivoFijo reader = (ActivoFijo)new ActivoFijo().AfAsigna(term);
            return Json(reader, JsonRequestBehavior.AllowGet);

        }


        //PARA BUSCAR CODIGO DE PRODUCTO EN CREACION MULTIPLE DE ETIQUETA
        public JsonResult getProdAutocom(string term)
        {
            string[] codigo = term.Split('-');

            ActivoFijo lstCodigos = (ActivoFijo)new ActivoFijo().BuscarCOD(codigo[0]);                         
            return Json(lstCodigos, JsonRequestBehavior.AllowGet);

        }

        //public JsonResult getProdAutocom(string term)
        //{
        //    string[] codigo = term.Split('-');

        //    ActivoFijo lstCodigos = (ActivoFijo)new ActivoFijo().Buscar(codigo[0]);
        //    return Json(lstCodigos, JsonRequestBehavior.AllowGet);

        //}



        [HttpPost]
        public ActionResult Create(ActivoFijo ActivoFijo)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = ActivoFijo.Create();


                return View("Edit", ActivoFijo);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", ActivoFijo);
            }

        }

        public ActionResult Edit(ActivoFijo ActivoFijo)
        {
            string message = string.Empty;
            //if (ModelState.IsValid== true)
            ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "af_Asigna where AF_ETIQ='" + ActivoFijo.AF_ETIQ + "'";
            ConectDB.QueryInsert = "UPDATE	" + ParametrosGlobales.bd + "  AF_ASIGNA SET AF_CTAGTODEP = '" + ActivoFijo.AF_CTAGTODEP + "', AF_CTADEP = '" + ActivoFijo.AF_CTADEP + "', AF_VALACT = '" + ActivoFijo.AF_VALACT + "', AF_VALRES = '" + ActivoFijo.AF_VALRES + "', AF_VALORI = '" + ActivoFijo.AF_VALORI + "', AF_DEPACC = '" + ActivoFijo.AF_DEPACC + "', AF_DEPSEM = '" + ActivoFijo.AF_DEPSEM + "', AF_ALICUOTA = '" + ActivoFijo.AF_ALICUOTA + "', AF_VALLIB = '" + ActivoFijo.AF_VALLIB + "', AF_CANT = '" + ActivoFijo.AF_CANT + "', AF_MODIFUSUA = '" + ActivoFijo.AF_MODIFUSUA + "', AF_MEJORA = '" + ActivoFijo.AF_MEJORA + "', AF_PVP = '" + ActivoFijo.AF_PVP + "', AF_CTOUNIT = '" + ActivoFijo.AF_CTOUNIT + "', AF_TPOGAR = '" + ActivoFijo.AF_TPOGAR + "', AF_NROGAR = '" + ActivoFijo.AF_NROGAR + "', AF_PROVGAR = '" + ActivoFijo.AF_PROVGAR + "', AF_TELPROVGAR = '" + ActivoFijo.AF_TELPROVGAR + "', AF_DIRGAR = '" + ActivoFijo.AF_DIRGAR + "', AF_MARCA = '" + ActivoFijo.AF_MARCA + "', AF_MOD = '" + ActivoFijo.AF_MOD + "', AF_OBS = '" + ActivoFijo.AF_OBS + "', AF_UBICACION = '" + ActivoFijo.AF_UBICACION + "', AF_CODPERT = '" + ActivoFijo.AF_CODPERT + "', AF_DEPREC = '" + (ActivoFijo.AF_DEPREC ? "1" : "0") + "', AF_DEPANT = '" + ActivoFijo.AF_DEPANT + "', AF_ULTDEP = '" + ActivoFijo.AF_ULTDEP + "', AF_BIENNAC = '" + ActivoFijo.AF_BIENNAC + "', AF_PARAM1 = '" + ActivoFijo.AF_PARAM1 + "', AF_DESCBIEN = '" + ActivoFijo.AF_DESCBIEN + "', AF_CLASBIEN = '" + ActivoFijo.AF_CLASBIEN + "', AF_ETIQ_IMPRE = '" + ActivoFijo.AF_ETIQ_IMPRE + "', AFCTAACTOLD = '" + ActivoFijo.AFCTAACTOLD + "', AFCTADEPOLD = '" + ActivoFijo.AFCTADEPOLD + "', AFCTAGTOOLD = '" + ActivoFijo.AFCTAGTOOLD + "', AF_ETIQ_CONCILIA= '" + ActivoFijo.AF_ETIQ_CONCILIA + "', AF_COLOR = '" + ActivoFijo.AF_COLOR + "', AF_MATERIAL = '" + ActivoFijo.AF_MATERIAL + "' WHERE AF_ETIQ  = '" + ActivoFijo.AF_ETIQ + "' ";





            //af_maestro.AFNOM 
            //af_maestro.AFUNIDAD
            //af_grupos.AFGRUDES
            //    af_grupos.AFGRUCTAACT
            //    af_grupos.AFGRUCTAGTODEP
            //    af_grupos.AFGRUCTADEP


            if (ActivoFijo.AF_ETIQ != "")
            {
                message = ActivoFijo.Edit();
                //string [] sts = Request.UserHostAddressUserLanguages;
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", ActivoFijo);


        }

        public ActionResult Delete(string afetiq)
        {
            string message = string.Empty;
            ActivoFijo ActivoFijo = new ActivoFijo();
            ActivoFijo.AF_ETIQ = afetiq;
            message = ActivoFijo.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }
        public override ActionResult Buscar(string codigo)
        {
            ActivoFijo modelo = new ActivoFijo();
            PropertyInfo propertyInfo = modelo.GetType().GetProperty(modelo.Campo_Clave);
            propertyInfo.SetValue(modelo, codigo, null);
            modelo.Buscar2(codigo);

            return View("Edit", modelo);

        }


        [HttpPost]
        public ActionResult insertmultiple()
        {
            var fechacrea = Request["fechacreacio"];
            var depreciar = Request["depreciacion"];
            int cantidad = int.Parse(Request["cantidades"]);                ////
            var costo = Request["costos"].Replace(",", ".");
            var valres = Request["valresidu"];
            var codact = Request["codactivo"];
            var modelo = Request["modelos"];
            var marca = Request["marcas"];
            var fechareal = Request["fecharealsi"];
            var codemp = Request["codemplea"];
            var ubicacion = Request["ubicacione"];
            var clientin = Request["clieinter"];
            var fecadqu = Request["fecadqui"];
            var nfact = Request["nfactura"];
            var ordecom = Request["ordecompra"];
            var comproban = Request["comprobante"];
            var proveedor = Request["proveedores"];
            var depini = Request["depinicial"];
            var depmes = Request["depremes"];
            var mesedepr = Request["mesedeprecia"] == "" ? "0" : Request["mesedeprecia"];
            var ultdep = Request["ultdepre"];
            var ctaact = Request["ctaactivo"];
            var ctagtodep = Request["ctagtodepre"];
            var ctadep = Request["ctadepre"];
            //int contador = 1;



            ActivoFijo asigna = new ActivoFijo
            {
                AF_FECH = DateTime.Parse(fechacrea),
                AF_DEPREC = bool.Parse(depreciar),
                cantidad = cantidad.ToString(),                         ////        
                AF_CTOUNIT = double.Parse(costo, System.Globalization.CultureInfo.InvariantCulture),
                AF_VALRES = double.Parse(valres),
                AF_CODACT = codact,
                AF_MOD = modelo,
                AF_MARCA = marca,
                AF_FECHREAL = DateTime.Parse(fechareal),
                AF_CODEMPL = codemp,
                AF_UBICACION = ubicacion,
                AF_CCTO = clientin,
                AF_ADQFECHA = DateTime.Parse(fecadqu),
                AF_ADQFACT = nfact,
                AF_ADQOC = ordecom,
                AF_ADQCOMP = comproban,
                AF_ADQPROV = proveedor,
                AF_DEPINI = DateTime.Parse(depini),
                AF_DEPMESES = int.Parse(depmes),
                AF_DEPMESESYA = int.Parse(mesedepr),
                //AF_DEPMESESYA = 0,
                AF_DEPULT = DateTime.Parse(ultdep),
                AF_CTAACT = ctaact,
                AF_CTAGTODEP = ctagtodep,
                AF_CTADEP = ctadep,
                AF_ORGCOSTO = 0,
                AF_ORGMESESDEP = 0,
                AF_DEPSEM = 0,
                AF_CANT = 1,
                AF_USUARIO = Util.ContactoActual(),
                AF_MODIFUSUA = Util.ContactoActual(),
                AF_TPOGAR = 0,
                AF_DEPANT = 0,
                AF_ULTDEP = 0,
                AF_ETIQ_IMPRE = 1,
                AF_DES = "Asignado",
                AF_SERIAL = "ss",
                AF_ORGFECHA = DateTime.Parse(fechacrea),
                AF_ADQVIGD = DateTime.Parse(fechacrea),
                AF_ADQVIGH = DateTime.Parse(fechacrea),
                AF_VALACT = double.Parse(costo, System.Globalization.CultureInfo.InvariantCulture),
                AF_VALORI = double.Parse(costo, System.Globalization.CultureInfo.InvariantCulture),
                AF_DEPACC = 1,
                AF_ALICUOTA = 1,
                AF_VALLIB = 1,
                AF_MODIFFECHA = DateTime.Parse(fechacrea),
                AF_DESFECHA = DateTime.Parse(fechacrea),
                AF_MEJORA = 0,
                AF_PVP = double.Parse(costo, System.Globalization.CultureInfo.InvariantCulture),
                AF_FEVENC = DateTime.Parse(fechacrea),
                AF_CODPERT = "-",

            };
            asigna.Insertmultiple();


            var resultado = StringResources.msgInsertOK; // Los datos que quieres devolver

            return Json(resultado);

        }


    }
}
