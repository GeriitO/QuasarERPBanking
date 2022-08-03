using Resources.MANTENIMIENTO;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_DEPOSITOS : ConectDB
    {
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string INVDEPDES { get; set; }
        [Display(Name = "lblClase", ResourceType = typeof(StringResources))]
        public string INVDEPCLASE { get; set; }
        [Display(Name = "lblRespo", ResourceType = typeof(StringResources))]
        public string INVDEPRESP { get; set; }
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string INVDEPCOD { get; set; }
        [Display(Name = "lblEmail", ResourceType = typeof(StringResources))]
        public string INVDEPMAIL { get; set; }


    

        static string sp_getAll = "SP_ALL_INV_DEPOSITOS";



        public INV_DEPOSITOS()
        {
            Campo_Clave = "INVDEPCOD";
        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<INV_DEPOSITOS> GetDepositos()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<INV_DEPOSITOS> depositos = new List<INV_DEPOSITOS>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    INV_DEPOSITOS deposito = new INV_DEPOSITOS();
                    deposito.INVDEPCOD = item["INVDEPCOD"].ToString();
                    deposito.INVDEPDES = item["INVDEPDES"].ToString();
                    depositos.Add(deposito);
                }
            }

            return depositos;
        }

        //llenar la lista completa de todos los depositos en el grid para mantenimiento de la tabla
        public static List<INV_DEPOSITOS> GetDeposito()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_DEPOSITOS> lstDepositos = new List<INV_DEPOSITOS>();
            string consulta = "SELECT * FROM " + ParametrosGlobales.bd + "INV_DEPOSITOS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstDepositos.Add(setDepositos(item));
                }
            }
            return lstDepositos;
        }


        static INV_DEPOSITOS setDepositos(Row item)
        {
            INV_DEPOSITOS deposito = new INV_DEPOSITOS
            {
                INVDEPDES = item["INVDEPDES"].ToString(),
                INVDEPCLASE = item["INVDEPCLASE"].ToString(),
                INVDEPRESP = item["INVDEPRESP"].ToString(),
                INVDEPMAIL = item["INVDEPMAIL"].ToString(),
                INVDEPCOD = item["INVDEPCOD"].ToString(),


            };
            return deposito;
        }

        static INV_DEPOSITOS setDepositos(DataRow item)
        {
            INV_DEPOSITOS deposito = new INV_DEPOSITOS
            {
                INVDEPDES = item["INVDEPDES"].ToString(),
                INVDEPCLASE = item["INVDEPCLASE"].ToString(),
                INVDEPRESP = item["INVDEPRESP"].ToString(),
                INVDEPMAIL = item["INVDEPMAIL"].ToString(),
                INVDEPCOD = item["INVDEPCOD"].ToString(),


            };
            return deposito;
        }
        //cargar datos de un deposito para mantenimiento
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                INVDEPDES = item["INVDEPDES"].ToString();
                INVDEPCLASE = item["INVDEPCLASE"].ToString();
                INVDEPRESP = item["INVDEPRESP"].ToString();
                INVDEPMAIL = item["INVDEPMAIL"].ToString();
                INVDEPCOD = item["INVDEPCOD"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(5);
            
            parametros.Add(INVDEPDES);
            parametros.Add(INVDEPCLASE);
            parametros.Add(INVDEPRESP);
            parametros.Add(INVDEPCOD);
            parametros.Add(INVDEPMAIL);
            

            return parametros;
        }

        #region AUTOCOMPLETE BUSCA POR CODIGO
        public List<string> GetCampos(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> activos = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( INVDEPCOD , ' - ' ) , INVDEPDES ) AS DEPOS FROM " + ParametrosGlobales.bd + "INV_DEPOSITOS WHERE(UPPER(INVDEPDES) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) OR(UPPER(INVDEPCOD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string activo = "";
                    activo = item["DEPOS"].ToString();
                    activos.Add(activo);
                }
            }

            return activos;
        }
        #endregion


    }
}