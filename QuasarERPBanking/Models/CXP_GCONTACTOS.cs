using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources.CXP_GRUPOS;
using System.Collections;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CXP_GCONTACTOS : clsMain
    {
        [Display(Name = "lblNomb", ResourceType = typeof(StringResources))]
        public string CXPCONTNOM { get; set; }

        [Display(Name = "lblTlf", ResourceType = typeof(StringResources))]
        public string CXPCONTTEL { get; set; }

        [Display(Name = "lblFax", ResourceType = typeof(StringResources))]
        public string CXPCONTFAX { get; set; }

        [Display(Name = "lblDir", ResourceType = typeof(StringResources))]
        public string CXPCONTDIR { get; set; }

        [Display(Name = "lblEmail", ResourceType = typeof(StringResources))]
        //[DataType(DataType.EmailAddress)]
        public string CXPCONTEMAIL { get; set; }

        public string CXPCONTEXT { get; set; }

        [Display(Name = "lblContCod", ResourceType = typeof(StringResources))]
        public string CXPCONTCOD { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        public string CXPCOD { get; set; }

        public int CXPCONTSEL { get; set; }

        private static string sp_FindCXPCOD { get; set; }
        public CXP_GCONTACTOS()
        {
            sp_Insert = "SP_INS_GCONTACTOS";
            sp_Update = "SP_UPD_GCONTACTOS";
            sp_Delete = "SP_DEL_GCONTACTOS";
            sp_Find = "SP_Q_CXP_GCONTACTOS";
            sp_FindCXPCOD = "SP_Q_GCONTCXPCOD";
            Campo_Clave = "CXPCONTCOD";
        }

        protected override ArrayList getParameters()
        {

            CXPCONTEXT = "-";
            CXPCONTSEL = 0;

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

                CXPCONTNOM = item["CXPCONTNOM"].ToString();
                CXPCONTTEL = item["CXPCONTTEL"].ToString();
                CXPCONTFAX = item["CXPCONTFAX"].ToString();
                CXPCONTDIR = item["CXPCONTDIR"].ToString();
                CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString();
                CXPCONTEXT = item["CXPCONTEXT"].ToString();
                CXPCONTCOD = item["CXPCONTCOD"].ToString();
                CXPCOD = item["CXPCOD"].ToString();
                CXPCONTSEL = int.Parse(item["CXPCONTSEL"].ToString());                     
            }

            return this;
        }

        //para rellenar el grid

        public static List<CXP_GCONTACTOS> ContactosPorCXPCOD(string CXPCOD)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_GCONTACTOS> lista = new List<CXP_GCONTACTOS>();
            string consulta = "SELECT CXPCONTCOD , CXPCONTNOM , CXPCONTTEL , CXPCONTFAX , CXPCONTDIR , CXPCONTEMAIL , CXPCONTEXT , CXPCOD , CXPCONTSEL FROM BICADM01W . CXP_GCONTACTOS WHERE CXPCOD = '" + CXPCOD + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CXP_GCONTACTOS cxp_gcontactos = new CXP_GCONTACTOS
                        {
                            CXPCONTNOM = item["CXPCONTNOM"].ToString(),
                            CXPCONTTEL = item["CXPCONTTEL"].ToString(),
                            CXPCONTFAX = item["CXPCONTFAX"].ToString(),                              	
                            CXPCONTDIR = item["CXPCONTDIR"].ToString(),
                            CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString(),                        
                            CXPCONTCOD = item["CXPCONTCOD"].ToString(),
                            CXPCOD = item["CXPCOD"].ToString(),                        
                                                                                            
                        };
                        lista.Add(cxp_gcontactos);
                    }
                }
            }
            return lista;
        }
       
    }


}




//////////////// SP UTILIZADOS ////////////////
//SP_INS_GCONTACTOS
//SP_UPD_GCONTACTOS
//SP_DEL_GCONTACTOS
//SP_Q_CXP_GCONTACTOS
//SP_Q_GCONTCXPCOD
                       