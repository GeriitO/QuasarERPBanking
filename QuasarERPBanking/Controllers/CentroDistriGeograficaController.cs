using QuasarERPBanking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace QuasarERPBanking.Controllers
{
    public class CentroDistriGeograficaController : ParentController
    {
        // GET: CC_Distgeog
        public ActionResult Index()
        {
            return View();
        }

        // GET: CC_Distgeog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CC_Distgeog/Create
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: CC_Distgeog/Create
        [HttpPost]
        public ActionResult Create(CentroDistriGeografica cc_distgeog)
        {
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste= "Select * from " + ParametrosGlobales.bd + "cc_distgeog where cc_cod='" + cc_distgeog.CC_COD + "'";
                ConectDB.QueryInsert = "Insert into " + ParametrosGlobales.bd + "cc_distgeog(cc_cod, cc_des, cc_pert_geo) values('" + cc_distgeog.CC_COD.ToString() + "' , '" + cc_distgeog.CC_DES.ToString() + "' , '" + cc_distgeog.CC_PERT_GEO.ToString() + "')";
                ViewBag.message = cc_distgeog.Create();


                return View("Edit", cc_distgeog);
            }
            else
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                return View("Create", cc_distgeog);
            }

        }

        // GET: CC_Distgeog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CC_Distgeog/Edit/5
        [HttpPost]
        public ActionResult Edit(CentroDistriGeografica cc_distgeog)
        {
            cc_distgeog.CC_COD = cc_distgeog.CODEDIT;
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                ConectDB.QueryExiste = "Select * from " + ParametrosGlobales.bd + "cc_distgeog where cc_cod='" + cc_distgeog.CC_COD + "'";
                ConectDB.QueryInsert = "update " + ParametrosGlobales.bd + "cc_distgeog set  cc_des='" + cc_distgeog.CC_DES.ToString() + "' , cc_pert_geo='" + cc_distgeog.CC_PERT_GEO.ToString() + "' where cc_cod='" + cc_distgeog.CC_COD.ToString() + "'" ;
                message = cc_distgeog.Edit();
                //string [] sts = Request.UserHostAddressUserLanguages;
            }
            else
            {
                message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
            }

            ViewBag.message = message;
            return View("Index", cc_distgeog);

        }

        //[HttpPost]
        public ActionResult Get_Geog()
        {
            var codigo = Request["codigo"];
            List<CentroDistriGeografica> afectado = CentroDistriGeografica.getree(codigo);
            return Json(afectado.ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult getGrid(string codigo)
        {
            List<CentroDistriGeografica> lstFilas = CentroDistriGeografica.ListHijos(codigo);
            StringBuilder table = new StringBuilder();
            string[] arCampos = {
                "CC_DES",
               
            };
            int cont = 0;
            foreach (CentroDistriGeografica Filas in lstFilas)
            {
                //contacto.ToString();
                table.Append("<tr>");
                table.Append("<td style='width:80px'>");
                table.Append("<a id='To'  style = 'cursor:pointer;' onclick=\"editargeo('" + Filas.CC_COD + "');\">");
                //table.Append("<a id='To' style = 'cursor:pointer;' onclick='editargeo(\''");
                ////table.Append(Filas.CC_COD);
                //table.Append("\'');'>");
                table.Append(Filas.CC_COD);
                table.Append("</a>");
                table.Append("</td>");

                foreach (string campo in arCampos)
                {
                    table.Append("<td>");
                    table.Append(Filas.GetType().GetProperty(campo).GetValue(Filas));
                    table.Append("</td>");
                }
                table.Append("</tr>");
                cont++;
            }
            return Json(table.ToString(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getCodAutocom(string term)
        {
            string[] codigo = term.Split('_');
            //CC_MAESTRO lstCodigos = new CC_MAESTRO().getDescri(codigo[0].Trim());
            List<CentroDistriGeografica> lstCodigos = CentroDistriGeografica.getDescri(codigo[0].Trim());
            return Json(lstCodigos[0], JsonRequestBehavior.AllowGet);

        }


        public JsonResult getCodigos(string term)
        {
            List<string> lstCodigos = new CentroDistriGeografica().GetDistGeo(term);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public JsonResult Exist(string cod)
        {
            string lstCodigos = new CentroDistriGeografica().GetExist(cod);
            return Json(lstCodigos.ToList(), JsonRequestBehavior.AllowGet);

        }


        public ActionResult Delete(string ccod)
        {
            string message = string.Empty;
            CentroDistriGeografica cc_distgeog = new CentroDistriGeografica();
            cc_distgeog.CC_COD = ccod;

            string lstCodigos = new CentroDistriGeografica().GetExist2(ccod);

            if(lstCodigos == "1")
            {
                message = "No se puede eliminar este centro de costo, ya que NO se encuentra en el ultimo nivel";
            }
            else
            {
                message = cc_distgeog.Eliminar();
            }
           
            ViewBag.message = message;
            return View("Index");

        }



        public ActionResult Delete2(string ccod)
        {
            string message = string.Empty;
            CentroDistriGeografica cc_distgeog = new CentroDistriGeografica();
            cc_distgeog.CC_COD = ccod;

            bool resultado = cc_distgeog.Existe("Select * from " + ParametrosGlobales.bd + "cc_distgeog where cc_pert_geo='" + ccod + "'");

            if (resultado == true )
            {
                message = "No se puede eliminar este centro de costo, ya que existen registros asociados a el";
            }
            else
            {
                ConectDB.QueryInsert = "Delete from " + ParametrosGlobales.bd + "cc_distgeog where cc_cod='" + ccod.ToString() + "'";
                message = cc_distgeog.Eliminar();
            }

            ViewBag.message = message;
            return View("Index");

        }

    }
}
