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

namespace QuasarERPBanking.Models
{
    public class ESTADOS: ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int CODIGO { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string ESTADO { get; set; }

        static string sp_getAll = "SP_ALL_ESTADOS";


        public ESTADOS()
        {
            Campo_Clave = "CODIGO";
        }
        
        
        
        //para cargar el dropdownlist de los formularios
        public static IEnumerable<ESTADOS> GetEstados()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ESTADOS> listafinal = new List<ESTADOS>();
            string consulta = "SELECT CODIGO , 	ESTADO    FROM BICADM01W.ESTADOS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.ESTADOS listas = new Models.ESTADOS();
                    
                    listas.ESTADO = item["ESTADO"].ToString();
                    listafinal.Add(listas);
                }
            }
            return listafinal;
        }

        //llenar la lista completa de estados para el mantenimiento
        public static List<ESTADOS> GetStates()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ESTADOS> lstStates = new List<ESTADOS>();
            string consulta = "SELECT CODIGO ,  ESTADO  FROM BICADM01W . ESTADOS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstStates.Add(setState(item));
                }
            }
            return lstStates;
        }

        static ESTADOS setState(Row item)
        {
            ESTADOS state = new ESTADOS
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                ESTADO = item["ESTADO"].ToString(),
               
            };
            return state;
        }

        static ESTADOS setState(DataRow item)
        {
            ESTADOS state = new ESTADOS
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                ESTADO = item["ESTADO"].ToString(),

            };
            return state;
        }

        //cargar los datos de un estado
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                CODIGO = int.Parse(item["CODIGO"].ToString());
                ESTADO = item["ESTADO"].ToString();

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);
            parametros.Add(CODIGO);
            parametros.Add(ESTADO);         
            return parametros;
        }





    }
}






//////////////// SP UTILIZADOS ////////////////
//SP_ALL_ESTADOS  
//SP_DEL_ESTADOS  
//SP_INS_ESTADOS  
//SP_Q_ESTADOS  
//SP_UPD_ESTADOS 


//public static IEnumerable<ESTADOS> GetEstados()
//{
//    clsAS400 cn = new clsAS400();
//    System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
//    List<object> reader = cn.execProc(sp_getAll, parametros, false);
//    List<ESTADOS> estados = new List<ESTADOS>();

//    if (reader != null)
//    {
//        foreach (Dictionary<string, object> item in reader)
//        {
//            ESTADOS estado = new ESTADOS();



//            estado.ESTADO = item["ESTADO"].ToString();
//            estados.Add(estado);
//        }
//    }

//    return estados;
//} 