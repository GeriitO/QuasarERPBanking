using Resources.CC_PARAMETROS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CentroCostoParametros
    {
        

         [Display(Name = "lblNom",  ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        public string CTEPARNOMB { get; set; }

        [Display(Name = "lblAct",  ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errAct")]       
        public string CTEPARACTID { get; set; }

        [Display(Name = "lblTrans",  ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTrans")]        
        public string CC_IDTRANS { get; set; }

    
        public string CCPARNBCOMP { get; set; }

        [Display(Name = "lblMayor", ResourceType = typeof(StringResources))]         
        public bool CCPARINTMG { get; set; }

        [Display(Name = "lblDir",  ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDir")]
        public string CCPARDIRMG { get; set; }
        public string CCPARRIFCOMP { get; set; }



        //busca en la bd 
        public CentroCostoParametros Buscar()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);

            string consulta = "SELECT CTEPARNOMB , CTEPARACTID , CC_IDTRANS , CCPARNBCOMP , CCPARINTMG , CCPARDIRMG , CCPARRIFCOMP FROM " + ParametrosGlobales.bd + "CC_PARAMETROS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CTEPARNOMB = item["CTEPARNOMB"].ToString();
                    CTEPARACTID = item["CTEPARACTID"].ToString();
                    CC_IDTRANS = item["CC_IDTRANS"].ToString();
                    CCPARNBCOMP = item["CCPARNBCOMP"].ToString();
                    CCPARINTMG = (item["CCPARINTMG"].ToString() == "0" ? false : true);              
                    CCPARDIRMG = (item["CCPARDIRMG"].ToString() == null ? "-": item["CCPARDIRMG"].ToString());
                    CCPARRIFCOMP = item["CCPARRIFCOMP"].ToString();



                //CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "true 0" : false item["CODEMP"].ToString()),
                //FECHACREACION = (item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(item["FECHACREACION"].ToString())),

                }

            }
            return this;
        }

        public bool Existe()
        {
            return true;
        }

        public int Update()
        {
            int resultado =0;
            string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            OleDbConnection CN = new OleDbConnection(strconnect);
            CN.Open();
            OleDbCommand CMD = new OleDbCommand(ConectDB.QueryInsert , CN);
            CMD.ExecuteNonQuery();
            CN.Close();
            return resultado;
            //CCPARNBCOMP = "-";
            //CCPARRIFCOMP = "-";



            //int resultado = 0;
            //Models.clsAS400 cn = new Models.clsAS400();
            //ArrayList parametros = new ArrayList(7);


            //parametros.Add(CTEPARNOMB);
            //parametros.Add(CTEPARACTID);
            //parametros.Add(CC_IDTRANS);
            //parametros.Add(CCPARNBCOMP);
            //parametros.Add(CCPARINTMG ? "1" : "0");
            //parametros.Add(CCPARDIRMG);          
            //parametros.Add(CCPARRIFCOMP);


            //cn.execProc("SP_UPD_CC_PARAMETROS", parametros, true);

            ////return cn.afectados;                                             
            //return resultado;
        }


    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_Q_CC_PARAMETROS
//SP_UPD_CC_PARAMETROS
