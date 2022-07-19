using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.MANTENIMIENTO;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Models
{
    public class ActivoFijoDepositos : ConectDB
    {

        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string AFDEPCOD { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string AFDEPDES { get; set; }
        [Display(Name = "lblClase", ResourceType = typeof(StringResources))]
        public string AFDEPCLASE { get; set; }
        [Display(Name = "lblRespo", ResourceType = typeof(StringResources))]
        public string AFDEPRESP { get; set; }
        [Display(Name = "lblEmail", ResourceType = typeof(StringResources))]
        public string AFDEPMAIL { get; set; }


        static string sp_getAll = "SP_ALL_AF_DEPOSITOS";



        public ActivoFijoDepositos()
        {
            Campo_Clave = "AFDEPCOD";
            tabla = "ActivoFijoDepositos";
        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<ActivoFijoDepositos> GetDepositos()
        {


            List<ActivoFijoDepositos> depositos = new List<ActivoFijoDepositos>();
            DataSet lista = new DataSet();
            OleDbConnection cnx = new OleDbConnection(ConectDB.CnStr);
            string consulta = "";

            consulta = "SELECT AFDEPCOD , AFDEPDES FROM " + ParametrosGlobales.bd + "  ActivoFijoDepositos";
            cnx.Open();
            OleDbCommand DA = new OleDbCommand(consulta, cnx);
            OleDbDataReader reader = DA.ExecuteReader();


            if (reader != null)
            {
                while (reader.Read())
                {
                    ActivoFijoDepositos deposito = new ActivoFijoDepositos();
                    deposito.AFDEPCOD = reader["AFDEPCOD"].ToString();
                    deposito.AFDEPDES = reader["AFDEPDES"].ToString();
                    depositos.Add(deposito);
                }
            }

            return depositos;
        }


        //llenar la lista completa de todos los depositos en el grid para mantenimiento de la tabla
        //public static List<ActivoFijoDepositos> GetDeposito()
        //{
        //    List<ActivoFijoDepositos> lstDepositos = new List<ActivoFijoDepositos>();
        //    ConectDB cn = new ConectDB();


        //    List<CXP_ORDENPAGO> listafinal = new List<CXP_ORDENPAGO>();
        //    DataSet lista = new DataSet();
        //    OleDbConnection cnx = new OleDbConnection(ConectDB.CnStr);
        //    string consulta = "";


        //    consulta = "SELECT AFDEPCOD , AFDEPDES , AFDEPMAIL  FROM " + ParametrosGlobales.bd + " ActivoFijoDepositos";

        //    OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cnx);
        //    DA.Fill(lista);
        //    DataTable dt = lista.Tables[0];


        //    if (dt.Rows != null)
        //    {
        //        foreach (DataRow item in dt.Rows)

        //        {
        //            lstDepositos.Add(setDepositos(item));
        //        }
        //    }
        //    return lstDepositos;
        //}


        static ActivoFijoDepositos setDepositos(DataRow item)
        {
            ActivoFijoDepositos tipo = new ActivoFijoDepositos
            {
                AFDEPCOD = item["AFDEPCOD"].ToString(),
                AFDEPDES = item["AFDEPDES"].ToString(),
                AFDEPMAIL = item["AFDEPMAIL"].ToString(),

            };
            return tipo;
        }

        //cargar datos de un deposito para mantenimiento
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                AFDEPCOD = item["AFDEPCOD"].ToString();
                AFDEPDES = item["AFDEPDES"].ToString();
                AFDEPMAIL = item["AFDEPMAIL"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(3);

            parametros.Add(AFDEPCOD);
            parametros.Add(AFDEPDES);
            parametros.Add(AFDEPMAIL);

            return parametros;
        }


        public int Insert()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            //AFCODOLD = "0"; 
            parametros.Add(AFDEPCOD);
            parametros.Add(AFDEPDES);
            parametros.Add(AFDEPMAIL);

            cn.Insert2(tabla, parametros, true, CAMPOS());
            return cn.afectados;

        }

        protected override object whereParam()
        {
            string where = "" + Campo_Clave + "='" + AFDEPCOD + "'" + "";


            return where;
        }

        protected override string CAMPOS()
        {
            string CAMPOS = "AFDEPCOD,AFDEPDES,AFDEPMAIL";
            return CAMPOS;
        }


        public int getParametersUpd()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);
            parametros.Add("AFDEPCOD=" + "'" + AFDEPCOD + "'");
            parametros.Add("AFDEPDES=" + "'" + AFDEPDES + "'");
            parametros.Add("AFDEPMAIL=" + "'" + AFDEPMAIL + "'");
            cn.Update2(tabla, parametros, whereParam(), true);
            return cn.afectados;

        }
        protected override object whereParam(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {
                QUERY = "" + Campo_Clave + "='" + elemento + "'";
            }

            return QUERY;
        }

    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_ALL_AF_DEPOSITOS
//SP_Q_DEPOSITOS
//SP_INS_AF_DEPOSITOS
//SP_DEL_AF_DEPOSITOS
//SP_UPD_AF_DEPOSITOS
//SP_Q_AF_DEPOSITOS   