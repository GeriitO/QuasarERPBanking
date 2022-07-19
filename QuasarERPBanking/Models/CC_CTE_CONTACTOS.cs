using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
//using Resources.CC_CTE_CONTACTOS;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.CC_CONTACTOS;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CC_CTE_CONTACTOS : ConectDB
    {
        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        public string CTECONTNOM { get; set; }
        
        [Display(Name = "lblTel", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTel")]
        public string CTECONTTEL { get; set; }

        [Display(Name = "lblFax", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFax")]
        public string CTECONTFAX { get; set; }

        [Display(Name = "lblDir", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDir")]
        public string CTECONTDIR { get; set; }

        [Display(Name = "lblMail", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errMail")]
        public string CTECONTEMAIL { get; set; }
        public string CTEEXT { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCod")]
        public string CTECONTCOD { get; set; }
        public string CTECOD { get; set; }
        public string CTELOGIN { get; set; }
        public string CTECLAVE { get; set; }


        private static string sp_FindCXPCOD { get; set; }
        //public CC_CTE_CONTACTOS()
        //{
        //    sp_Insert = "SP_INS_CTECONTACTOS"; //
        //    sp_Update = "SP_UPD_CTECONTACTOS";  //
        //    sp_Delete = "SP_DEL_CTECONTACTOS"; //
        //    sp_Find = "SP_Q_CC_CONTACTOS";
        //    sp_FindCXPCOD = "SP_Q_CONTCTECOD";

        //    Campo_Clave = "CTECONTCOD";

        //}

        //protected override ArrayList getParameters()
        //{
        //    ArrayList parametros = new ArrayList(10);

        //    parametros.Add(CTECONTNOM);
        //    parametros.Add(CTECONTTEL);
        //    parametros.Add(CTECONTFAX);
        //    parametros.Add(CTECONTDIR);
        //    parametros.Add(CTECONTEMAIL);
        //    parametros.Add(CTEEXT);
        //    parametros.Add(CTECONTCOD);
        //    parametros.Add(CTECOD);
        //    parametros.Add(CTELOGIN);
        //    parametros.Add(CTECLAVE);
        //    return parametros;
        //}




        //para buscar el contacto al editar
        //protected override clsMain LoadObject(Rows reader)
        //{

        //    foreach (Row item in reader)
        //    {

        //        CTECONTNOM = item["CTECONTNOM"].ToString();
        //        CTECONTTEL = item["CTECONTTEL"].ToString();
        //        CTECONTFAX = item["CTECONTFAX"].ToString();
        //        CTECONTDIR = item["CTECONTDIR"].ToString();
        //        CTECONTEMAIL = item["CTECONTEMAIL"].ToString();
        //        CTEEXT = item["CTEEXT"].ToString();
        //        CTECONTCOD = item["CTECONTCOD"].ToString();
        //        CTECOD = item["CTECOD"].ToString();
        //        CTELOGIN = item["CTELOGIN"].ToString();
        //        CTECLAVE = item["CTECLAVE"].ToString();
        //    }

        //    return this;
        //}


        protected CC_CTE_CONTACTOS LoadObject(DataSet reader)
        {

            foreach (DataRow item in reader.Tables[0].Rows )
            {

                CTECONTNOM = item["CTECONTNOM"].ToString();
                CTECONTTEL = item["CTECONTTEL"].ToString();
                CTECONTFAX = item["CTECONTFAX"].ToString();
                CTECONTDIR = item["CTECONTDIR"].ToString();
                CTECONTEMAIL = item["CTECONTEMAIL"].ToString();
                CTEEXT = item["CTEEXT"].ToString();
                CTECONTCOD = item["CTECONTCOD"].ToString();
                CTECOD = item["CTECOD"].ToString();
                CTELOGIN = item["CTELOGIN"].ToString();
                CTECLAVE = item["CTECLAVE"].ToString();
            }

            return this;
        }
        //para rellenar el grid

        public static List<CC_CTE_CONTACTOS> ContactosPorCTECOD(string CTECOD)
        {
           
            ArrayList parametros = new ArrayList(1);
            parametros.Add(CTECOD);

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CC_CTE_CONTACTOS> listaGrid = new List<CC_CTE_CONTACTOS>();
            string consulta = "SELECT CTECONTCOD , CTECONTNOM , CTECONTTEL , CTECONTFAX , CTECONTDIR , CTECONTEMAIL , CTEEXT , CTECOD , CTELOGIN , CTECLAVE FROM " + ParametrosGlobales.bd + "CC_CTE_CONTACTOS WHERE CTECOD = '" + CTECOD + "' ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CC_CTE_CONTACTOS cte_contactos = new CC_CTE_CONTACTOS
                        {
                            CTECONTNOM = item["CTECONTNOM"].ToString(),
                            CTECONTTEL = item["CTECONTTEL"].ToString(),
                            CTECONTFAX = item["CTECONTFAX"].ToString(),
                            CTECONTDIR = item["CTECONTDIR"].ToString(),
                            CTECONTEMAIL = item["CTECONTEMAIL"].ToString(),
                            CTEEXT = item["CTEEXT"].ToString(),
                            CTECONTCOD = item["CTECONTCOD"].ToString(),
                            CTECOD = item["CTECOD"].ToString(),
                            CTELOGIN = item["CTELOGIN"].ToString(),
                            CTECLAVE = item["CTECLAVE"].ToString(),

                        };
                        listaGrid.Add(cte_contactos);
                    }
                }
            }
            return listaGrid;
        }




        // para el dropdownlist de nota de entrega
        public static List<CC_CTE_CONTACTOS> ContactosPorCTECOD_(string ctecod)

        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(ctecod);
            Rows reader = cn.execProc("SP_Q_CONTCTECOD", parametros, false);
            List<CC_CTE_CONTACTOS> proveedores = new List<CC_CTE_CONTACTOS>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Models.CC_CTE_CONTACTOS proveedor = new Models.CC_CTE_CONTACTOS();
                    proveedor.CTECONTNOM = item["CTECONTNOM"].ToString();                  
                    proveedor.CTECONTEMAIL = item["CTECONTEMAIL"].ToString();                 
                    proveedor.CTECONTCOD = item["CTECONTCOD"].ToString();
                    proveedor.CTECOD = item["CTECOD"].ToString();
                   

                    //pre.DET_PROY = "ABC";
                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }


        //public new string ToString()
        //{
        //    //StringBuilder json = new StringBuilder();
        //    //json.Append("{");
        //    //json.Append("'CXPCONTCOD':'" + this.CXPCONTCOD + "',");
        //    //json.Append("'CXPCONTNOM':'" + this.CXPCONTNOM + "'");
        //    //json.Append("}");
        //    //return json.ToString();


        //    return "a";

        //}

    }
}


///// CAMPOS DE LA TABLA ////////////
//CTECONTNOM
//CTECONTTEL
//CTECONTFAX
//CTECONTDIR
//CTECONTEMAIL
//CTEEXT
//CTECONTCOD
//CTECOD
//CTELOGIN
//CTECLAVE



//////////////// SP UTILIZADOS ////////////////
//SP_INS_CTECONTACTOS
//SP_UPD_CTECONTACTOS
//SP_DEL_CTECONTACTOS
//SP_Q_CC_CONTACTOS
//SP_Q_CONTCTECOD 