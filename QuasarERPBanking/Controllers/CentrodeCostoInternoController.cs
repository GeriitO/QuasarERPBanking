using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;
using System.Text;

namespace QuasarERPBanking.Controllers
{
    public class CentrodeCostoInternoController : ParentController
    {
        // GET: CXP_Maestro
        public ActionResult Index()
        {
            return View();
        }

        //PARA BUSCAR EL CODIGO Y NOMBRE AUTOCOMPLETE
        public JsonResult getCodigos(string term)
        {
            List<string> lstCodigos = new CentrodeCostoInterno().GetAutocClientes(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }

        //AL CREAR BUSCA SI EL CODIGO EXISTE O NO 
        public JsonResult getCodigo(string term)
        {
            string valor = "";
            CentrodeCostoInterno cc_ctesint = new CentrodeCostoInterno();
            ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_ctesint where ctecod='" + term  + "'";
            cc_ctesint.CTECOD = term;
            if (cc_ctesint.Existe(ConectDB.QueryExiste))
                valor = "1";
            else
                valor = "0";
            return Json(valor, JsonRequestBehavior.AllowGet);

        }

        // PARA LLENAR EL FORMULARIO
        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('-');

            ////CC_CTESINT lstCodigos = (CC_CTESINT)new CC_CTESINT().Buscar(codigo[0]);
            //List<CC_CTESINT> lstCodigos = (List <CC_CTESINT> ) new CC_CTESINT().Buscar(codigo[0]);

            CentrodeCostoInterno lstCodigos = (CentrodeCostoInterno)new CentrodeCostoInterno().Buscarinfo(codigo[0]);

            return Json(lstCodigos, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult Create(CentrodeCostoInterno cc_ctesint)
        {

            cc_ctesint.CTEWEB = "-";
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_ctesint where ctecod='" + cc_ctesint.CTECOD.ToString() + "'";
                ConectDB.QueryInsert = "Insert into " + ParametrosGlobales.bd + "cc_ctesint(CTECOND,CTENOMBRE,CTEDIR,CTEWEB,CTERELACDESDE,CTERETENC,CTECOMPRADOR,CTEEDO,CTECIUDAD,CTECODPOSTAL,CTEFAX,CTETELF,CTEDIASCRED,CTEBALACT,CTERESENIA,CTECOD) values('" + cc_ctesint.CTECOND.ToString() + "' , '" + cc_ctesint.CTENOMBRE.ToString() + "' , '" + cc_ctesint.CTEDIR.ToString() + "' , '-' , '" + DateTime.Now.ToString(ParametrosGlobales.dfmt) + "' , '" + cc_ctesint.CTERETENC.ToString() + "' , '" + cc_ctesint.CTECOMPRADOR.ToString() + "' , '" + cc_ctesint.CTEEDO.ToString() + "' , '" + cc_ctesint.CTECIUDAD.ToString() + "' , '" + cc_ctesint.CTECODPOSTAL.ToString() + "' , '" + cc_ctesint.CTEFAX.ToString() + "' , '" + cc_ctesint.CTETELF.ToString() + "' , " + int.Parse(cc_ctesint.CTEDIASCRED.ToString()) + " , 0 , '" + cc_ctesint.CTERESENIA.ToString() + "' , '" + cc_ctesint.CTECOD.ToString() + "')";
                ViewBag.message = cc_ctesint.Create();


                return View("Edit", cc_ctesint);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", cc_ctesint);
            }

        }

        public ActionResult Edit(CentrodeCostoInterno cc_ctesint)
        {
            cc_ctesint.CTEWEB = "-";
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_ctesint where ctecod='" + cc_ctesint.CTECOD.ToString() + "'";
                ConectDB.QueryInsert = "Update " + ParametrosGlobales.bd + "cc_ctesint set CTECOND='" + cc_ctesint.CTECOND.ToString() + "' , CTENOMBRE='" + cc_ctesint.CTENOMBRE.ToString() + "' , CTEDIR='" + cc_ctesint.CTEDIR.ToString() + "' , CTEWEB='-' ,CTERELACDESDE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' , CTERETENC='0' , CTECOMPRADOR='" + cc_ctesint.CTECOMPRADOR.ToString() + "' , CTEEDO= '" + cc_ctesint.CTEEDO.ToString() + "' , CTECIUDAD='" + cc_ctesint.CTECIUDAD.ToString() + "' , CTECODPOSTAL= '" + cc_ctesint.CTECODPOSTAL.ToString() + "' , CTEFAX= '" + cc_ctesint.CTEFAX.ToString() + "' , CTETELF= '" + cc_ctesint.CTETELF.ToString() + "' , CTEDIASCRED= " + int.Parse(cc_ctesint.CTEDIASCRED.ToString()) + " , CTEBALACT=0 ,CTERESENIA= '" + cc_ctesint.CTERESENIA.ToString() + "' where CTECOD='"+ cc_ctesint.CTECOD.ToString() + "'";
                message = cc_ctesint.Edit();
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Edit", cc_ctesint);

        }

        public ActionResult Delete(string ctecod)
        {
            string message = string.Empty;
            CentrodeCostoInterno cc_ctesint = new CentrodeCostoInterno();
            ConectDB.QueryInsert = "Delete from " + ParametrosGlobales.bd + "cc_ctesint where ctecod='" + ctecod + "'";
            
            message = cc_ctesint.Eliminar();
            ViewBag.message = message;
            return View("Index");

        }

        public JsonResult GridCC()
        {           
            IEnumerable<CentrodeCostoInterno> lstFilas = CentrodeCostoInterno.GetDropdownlist();
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                //"CTECOD",
                "CTENOMBRE",             

            };
            int cont = 0;
            foreach (CentrodeCostoInterno Filas in lstFilas)
            {
                //contacto.ToString();
                table.Append("<tr>");
                table.Append("<td>");
                table.Append("<a style = 'cursor:pointer;' onclick='filaselect(" + Filas.CTECOD + ");'>");
                table.Append(Filas.CTECOD);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td id=nom" + Filas.CTECOD + ">");
                    table.Append(Filas.GetType().GetProperty(campo).GetValue(Filas));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }



    }
}
