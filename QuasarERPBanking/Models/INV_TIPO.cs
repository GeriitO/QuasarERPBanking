using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources.MANTENIMIENTO;
using System.ComponentModel.DataAnnotations;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_TIPO : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string INVTIPCOD { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string INVTIPDES { get; set; }


        static string sp_getAll = "SP_ALL_INV_TIPO";



        public INV_TIPO()
        {
            Campo_Clave = "INVTIPCOD";
        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<INV_TIPO> GetTipos()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_TIPO> tipos = new List<INV_TIPO>();
            string consulta = "SELECT  INVTIPCOD ,  INVTIPDES    FROM " + ParametrosGlobales.bd + "  INV_TIPO";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    INV_TIPO tipo = new INV_TIPO();
                    tipo.INVTIPCOD = item["INVTIPCOD"].ToString();
                    tipo.INVTIPDES = item["INVTIPDES"].ToString();
                    tipos.Add(tipo);
                }
            }

            return tipos;
        }

        //llenar la lista completa de todos los tipos en el grid para mantenimiento de la tabla
        public static List<INV_TIPO> GetListTipo()
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_TIPO> lstTipo = new List<INV_TIPO>();
            string consulta = "SELECT  INVTIPCOD ,  INVTIPDES    FROM " + ParametrosGlobales.bd + "INV_TIPO";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstTipo.Add(setTipos(item));
                }
            }
            return lstTipo;
        }


        static INV_TIPO setTipos(Row item)
        {
            INV_TIPO tipo = new INV_TIPO
            {
                INVTIPCOD = item["INVTIPCOD"].ToString(),
                INVTIPDES = item["INVTIPDES"].ToString(),


            };
            return tipo;
        }
        static INV_TIPO setTipos(DataRow item)
        {
            INV_TIPO tipo = new INV_TIPO
            {
                INVTIPCOD = item["INVTIPCOD"].ToString(),
                INVTIPDES = item["INVTIPDES"].ToString(),


            };
            return tipo;
        }

        //cargar datos de un tipo para mantenimiento
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                INVTIPCOD = item["INVTIPCOD"].ToString();
                INVTIPDES = item["INVTIPDES"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);

            parametros.Add(INVTIPCOD);
            parametros.Add(INVTIPDES);


            return parametros;
        }




    }
}