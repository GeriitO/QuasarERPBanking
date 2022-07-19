using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources.SERIALES;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using QuasarERPBanking.Models;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class SERIALES : ConectDB
    {

        public string AF_SERIAL { get; set; }
        public string AF_ETIQ { get; set; }
        static string sp_FindAFSERIAL = "SP_Q_ETIQUETA";


        //public AF_SERIALES()
        //{

        //    Campo_Clave = "AF_SERIAL";
        //}

        //para el grid al consultar por el serial
        public static List<SERIALES> EtiquetasPorSerial(string AF_SERIAL)
        {
           
            ArrayList parametros = new ArrayList(1);
            parametros.Add(AF_SERIAL);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<SERIALES> listaSerial = new List<SERIALES>();
            string consulta = "SELECT  AF_ETIQ , AF_SERIAL FROM "+ParametrosGlobales.bd+ "  AF_ASIGNA WHERE ( UPPER ( AF_SERIAL ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + AF_SERIAL + "' ) ) , '%' ) ) ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        SERIALES af_asigna = new SERIALES();
                        {
                            af_asigna.AF_ETIQ = item["AF_ETIQ"].ToString();
                            af_asigna.AF_SERIAL = item["AF_SERIAL"].ToString();
                        };

                        listaSerial.Add(af_asigna);
                    }
                }
            }
            return listaSerial;
        }







    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_Q_ETIQUETA
