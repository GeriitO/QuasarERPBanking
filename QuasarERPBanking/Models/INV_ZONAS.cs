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
    public class INV_ZONAS : ConectDB
    {
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string INVZONDES { get; set; }
        [Display(Name = "lblClase", ResourceType = typeof(StringResources))]
        public string INVZONCLASE { get; set; }
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string INVZONCOD { get; set; }



        static string sp_getAll = "SP_ALL_INV_ZONAS";



        public INV_ZONAS()
        {
            Campo_Clave = "INVZONCOD";
        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<INV_ZONAS> GetZonas()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_ZONAS> zonas = new List<INV_ZONAS>();
            string consulta = "SELECT  INVZONCOD ,  INVZONDES ,  INVZONCLASE  FROM " + ParametrosGlobales.bd + "  INV_ZONAS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    INV_ZONAS zona = new INV_ZONAS();
                    zona.INVZONDES = item["INVZONDES"].ToString();
                    zona.INVZONCLASE = item["INVZONCLASE"].ToString();
                    zona.INVZONCOD = item["INVZONCOD"].ToString();
                    zonas.Add(zona);
                }
            }
            return zonas;
        }

        //llenar la lista completa de todos los zonas en el grid para mantenimiento de la tabla
        public static List<INV_ZONAS> GetListZona()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_ZONAS> lstZonas = new List<INV_ZONAS>();
            string consulta = "SELECT  INVZONCOD ,  INVZONDES ,  INVZONCLASE  FROM "+ParametrosGlobales.bd+"  INV_ZONAS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstZonas.Add(setZonas(item));
                }
            }
            return lstZonas;
        }


        static INV_ZONAS setZonas(Row item)
        {
            INV_ZONAS zona = new INV_ZONAS
            {
                INVZONCOD = item["INVZONCOD"].ToString(),
                INVZONDES = item["INVZONDES"].ToString(),
                INVZONCLASE = item["INVZONCLASE"].ToString(),

            };
            return zona;
        }
        static INV_ZONAS setZonas(DataRow item)
        {
            INV_ZONAS zona = new INV_ZONAS
            {
                INVZONCOD = item["INVZONCOD"].ToString(),
                INVZONDES = item["INVZONDES"].ToString(),
                INVZONCLASE = item["INVZONCLASE"].ToString(),

            };
            return zona;
        }

        //cargar datos de un zona para mantenimiento
        protected override ConectDB LoadObject(Rows reader)
        {
            foreach (Row item in reader)
            {
                INVZONDES = item["INVZONDES"].ToString();
                INVZONCLASE = item["INVZONCLASE"].ToString();
                INVZONCOD = item["INVZONCOD"].ToString();
            }
            return this;
        }

        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(3);
            parametros.Add(INVZONCLASE);
            parametros.Add(INVZONCOD);
            parametros.Add(INVZONDES);


            return parametros;
        }



    }
}