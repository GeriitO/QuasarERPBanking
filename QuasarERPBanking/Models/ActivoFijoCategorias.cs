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
    public class ActivoFijoCategorias : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string AFGRUCOD { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string AFGRUDES { get; set; }
        [Display(Name = "lblCtacti", ResourceType = typeof(StringResources))]
        public string AFGRUCTAACT { get; set; }
        [Display(Name = "lblCtadep", ResourceType = typeof(StringResources))]
        public string AFGRUCTADEP { get; set; }
        [Display(Name = "lblCtagast", ResourceType = typeof(StringResources))]
        public string AFGRUCTAGTODEP { get; set; }
        [Display(Name = "lblmeses", ResourceType = typeof(StringResources))]
        public int AFGRUMESES { get; set; }



        static string sp_getAll = "SP_ALL_AF_CATEGORIAS";



        //public AF_CATEGORIAS()
        //{
        //    Campo_Clave = "AFGRUCOD";
        //}

        #region <.-PARA CARGAR EL DROPDOWNLIST DE LOS FORMULARIOS-.>
        public static IEnumerable<ActivoFijoCategorias> GetCategorias()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT  AFGRUCOD ,  AFGRUDES FROM " + ParametrosGlobales.bd + "  AF_CATESPEC";
            List<ActivoFijoCategorias> categorias = new List<ActivoFijoCategorias>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActivoFijoCategorias categoria = new ActivoFijoCategorias();
                    categoria.AFGRUCOD = item["AFGRUCOD"].ToString();
                    categoria.AFGRUDES = item["AFGRUDES"].ToString();
                    categorias.Add(categoria);
                }
            }

            return categorias;
        }
        #endregion



        #region <-. llenar la lista completa de todos las categorias en el grid para mantenimiento de la tabla .->
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ActivoFijoCategorias> GetCategoria()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT AFGRUCOD , AFGRUDES , AFGRUCTAACT FROM " + ParametrosGlobales.bd + " AF_CATESPEC";
            List<ActivoFijoCategorias> lstCategorias = new List<ActivoFijoCategorias>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    lstCategorias.Add(setCategorias(item));
                }
            }
            return lstCategorias;
        }
        #endregion



        static ActivoFijoCategorias setCategorias(DataRow item)
        {
            ActivoFijoCategorias categoria = new ActivoFijoCategorias
            {
                AFGRUCOD = item["AFGRUCOD"].ToString(),
                AFGRUDES = item["AFGRUDES"].ToString(),
                AFGRUCTAACT = item["AFGRUCTAACT"].ToString(),

            };
            return categoria;
        }

        //cargar datos de una categoria para mantenimiento
        protected override ConectDB LoadObject(Rows reader)   //<<<<----------- PENDIENTE AL CARGAR UN OBJETO 
        {

            foreach (Row item in reader)
            {
                AFGRUCOD = item["AFGRUCOD"].ToString();
                AFGRUDES = item["AFGRUDES"].ToString();
                AFGRUCTAACT = item["AFGRUCTAACT"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()  //<<<<----------- PENDIENTE AL CARGAR UN OBJETO 
        {
            ArrayList parametros = new ArrayList(3);

            parametros.Add(AFGRUCOD);
            parametros.Add(AFGRUDES);
            parametros.Add(AFGRUCTAACT);

            return parametros;
        }


    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_ActivoFijoCategorias
//SP_Q_CATEGORIAS
//SP_INS_ActivoFijoCategorias
//SP_DEL_ActivoFijoCategorias
//SP_UPD_ActivoFijoCategorias
//SP_Q_ActivoFijoCategorias    