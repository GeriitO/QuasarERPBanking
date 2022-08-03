using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CXP_TIPODOC
    {

       
        public int CODIGO { get; set; }    
        public string DESCRIPCION { get; set; } 
        public bool ACTIVO { get; set; }


        static string sp_getAll = "SP_ALL_CXP_TIPODOC";


        //para cargar el dropdownlist de los formularios
        public static IEnumerable<CXP_TIPODOC> GetTipo()
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_TIPODOC> grupos = new List<CXP_TIPODOC>();
            string consulta = "SELECT * FROM BICADM01W . CXP_TIPODOC WHERE ACTIVO = '1' ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CXP_TIPODOC grupo = new CXP_TIPODOC();
                    grupo.CODIGO = int.Parse(item["CODIGO"].ToString());
                    grupo.DESCRIPCION = item["DESCRIPCION"].ToString();
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }

    }
}