using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using Resources.CXP_CONTACTOS;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Text;
using System.Data.OleDb;
using System.Data;



namespace QuasarERPBanking.Models
{
    public class CXP_CONTACTOS : clsMain
    {
        [Display(Name = "lblNombCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNombContacto")]
        public string CXPCONTNOM { get; set; }                /*VARCHAR(60) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblTlfCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTlfContacto")]
        public string CXPCONTTEL { get; set; }                /*VARCHAR(70) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblFaxCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFaxContacto")]
        public string CXPCONTFAX { get; set; }                /*VARCHAR(70) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblDirCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirContacto")]
        public string CXPCONTDIR { get; set; }                /*VARCHAR(170) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblEmailCxpContacto", ResourceType = typeof(StringResources))]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errEmailContacto")]
        public string CXPCONTEMAIL { get; set; }                /*FOR COLUMN CXPCO00001 VARCHAR(70) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblContxCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errContxContacto")]
        public string CXPCONTEXT { get; set; }                /*VARCHAR(5) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblContCodCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errContCodContacto")]
        public string CXPCONTCOD { get; set; }                /*VARCHAR(10) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblCodCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCodContacto")]
        public string CXPCOD { get; set; }                /*VARCHAR(10) CCSID 284 DEFAULT NULL,*/

        [Display(Name = "lblSelCxpContacto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errSelContacto")]
        public int CXPCONTSEL { get; set; }

        //public int Insert()
        //{
        //    Models.clsAS400 cn = new Models.clsAS400();
        //    cn.execProc("SP_INS_CXPCONTACTOS", getParameters(), true);
        //    return cn.afectados;

        //}
        private static string sp_FindCXPCOD { get; set; }
        public CXP_CONTACTOS()
        {
            sp_Insert = "SP_INS_CXPCONTACTOS";
            sp_Update = "SP_UPD_CXPCONTACTOS";
            sp_Delete = "SP_DEL_CXPCONTACTOS";
            sp_Find = "SP_Q_CXP_CONTACTOS";
            sp_FindCXPCOD = "SP_Q_CONTCXPCOD";
            Campo_Clave = "CXPCONTCOD";
        }

        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(9);

            parametros.Add(CXPCONTNOM);
            parametros.Add(CXPCONTTEL);
            parametros.Add(CXPCONTFAX);
            parametros.Add(CXPCONTDIR);
            parametros.Add(CXPCONTEMAIL);
            parametros.Add(CXPCONTEXT);
            parametros.Add(CXPCONTCOD);
            parametros.Add(CXPCOD);
            parametros.Add(CXPCONTSEL);

            return parametros;
        }

       //para buscar el contacto al editar
        protected override clsMain LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {

                CXPCONTCOD = item["CXPCONTCOD"].ToString();
                CXPCONTNOM = item["CXPCONTNOM"].ToString();
                CXPCOD = item["CXPCOD"].ToString();
                CXPCONTDIR = item["CXPCONTDIR"].ToString();
                CXPCONTEXT = item["CXPCONTEXT"].ToString();
                CXPCONTFAX = item["CXPCONTFAX"].ToString();
                CXPCONTTEL = item["CXPCONTTEL"].ToString();
                CXPCONTSEL = int.Parse(item["CXPCONTSEL"].ToString());
                CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString();
            }

            return this;
        }

        //para rellenar el grid

        public static List<CXP_CONTACTOS> ContactosPorCXPCOD(string CXPCOD)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_CONTACTOS> lista = new List<CXP_CONTACTOS>();
            string consulta = "SELECT CXPCONTCOD , CXPCONTNOM , CXPCONTTEL , CXPCONTFAX , CXPCONTDIR , CXPCONTEMAIL , CXPCONTEXT , CXPCOD , CXPCONTSEL FROM BICADM01W . CXP_CONTACTOS WHERE CXPCOD = '" + CXPCOD + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CXP_CONTACTOS cxp_contactos = new CXP_CONTACTOS
                        {
                            CXPCONTCOD = item["CXPCONTCOD"].ToString(),
                            CXPCONTNOM = item["CXPCONTNOM"].ToString(),
                            CXPCOD = item["CXPCOD"].ToString(),
                            CXPCONTDIR = item["CXPCONTDIR"].ToString(),
                            CXPCONTEXT = item["CXPCONTEXT"].ToString(),
                            CXPCONTFAX = item["CXPCONTFAX"].ToString(),
                            CXPCONTTEL = item["CXPCONTTEL"].ToString(),
                            CXPCONTSEL = int.Parse(item["CXPCONTSEL"].ToString()),
                            CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString(),
                        };
                        lista.Add(cxp_contactos);
                    }
                }
            }
            return lista;
        }
        public new string ToString()
        {
            //StringBuilder json = new StringBuilder();
            //json.Append("{");
            //json.Append("'CXPCONTCOD':'" + this.CXPCONTCOD + "',");
            //json.Append("'CXPCONTNOM':'" + this.CXPCONTNOM + "'");
            //json.Append("}");
            //return json.ToString();


            return "a";

        }
    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_INS_CXPCONTACTOS
//SP_UPD_CXPCONTACTOS
//SP_DEL_CXPCONTACTOS
//SP_Q_CXP_CONTACTOS
//SP_Q_CONTCXPCOD 
