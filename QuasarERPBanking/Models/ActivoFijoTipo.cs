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

    public class ActivoFijoTipo : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public string AFTIPCOD { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string AFTIPDES { get; set; }



        static string sp_getAll = "SP_ALL_AF_TIPO";



        public ActivoFijoTipo()
        {
            Campo_Clave = "AFTIPCOD";
            tabla = "AF_TIPO";
        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<ActivoFijoTipo> GetTipos()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ActivoFijoTipo> tipos = new List<ActivoFijoTipo>();
            string consulta = "SELECT AFTIPCOD , AFTIPDES FROM " + ParametrosGlobales.bd + " ActivoFijoTipo";

            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActivoFijoTipo tipo = new ActivoFijoTipo();
                    tipo.AFTIPCOD = item["AFTIPCOD"].ToString();
                    tipo.AFTIPDES = item["AFTIPDES"].ToString();
                    tipos.Add(tipo);
                }
            }

            return tipos;
        }

        //llenar la lista completa de todos los tipos en el grid para mantenimiento de la tabla
        public static List<ActivoFijoTipo> GetTipo()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ActivoFijoTipo> lstTipos = new List<ActivoFijoTipo>();
            string consulta = "SELECT AFTIPCOD , AFTIPDES FROM " + ParametrosGlobales.bd + " ActivoFijoTipo";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstTipos.Add(setTipos(item));
                }
            }
            return lstTipos;
        }



        static ActivoFijoTipo setTipos(DataRow item)
        {
            ActivoFijoTipo tipo = new ActivoFijoTipo
            {
                AFTIPCOD = item["AFTIPCOD"].ToString(),
                AFTIPDES = item["AFTIPDES"].ToString(),

            };
            return tipo;
        }
        //static ActivoFijoTipo setTipos(Row item)
        //{
        //    ActivoFijoTipo tipo = new ActivoFijoTipo
        //    {
        //        AFTIPCOD = item["AFTIPCOD"].ToString(),
        //        AFTIPDES = item["AFTIPDES"].ToString(),

        //    };
        //    return tipo;
        //}

        //cargar datos de un grupo
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                AFTIPCOD = item["AFTIPCOD"].ToString();
                AFTIPDES = item["AFTIPDES"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);

            parametros.Add(AFTIPCOD);
            parametros.Add(AFTIPDES);

            return parametros;
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

        //public bool Existe()
        //{
        //    bool existe = false;
        //    DataSet ds = new DataSet();
        //    OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
        //    List<INV_GRUPOS> lstGrupos = new List<INV_GRUPOS>();
        //    string consulta = "SELECT AFTIPCOD , AFTIPDES  FROM " + ParametrosGlobales.bd + "   ActivoFijoTipo WHERE " + Campo_Clave + "='" + AFTIPCOD + "' ";
        //    OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
        //    DA.Fill(ds);
        //    DataTable dt = ds.Tables[0];

        //    if (dt.Rows != null)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            if (item["AFTIPCOD"].ToString() != null)
        //            {
        //                existe = true;
        //            }
        //        }
        //    }
        //    return existe;
        //}


        public int Insert()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            //AFCODOLD = "0"; 
            parametros.Add(AFTIPCOD);
            parametros.Add(AFTIPDES);

            cn.Insert2(tabla, parametros, true, CAMPOS());
            return cn.afectados;

        }




        public int getParametersUpd()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);
            parametros.Add("AFTIPCOD=" + "'" + AFTIPCOD + "'");
            parametros.Add("AFTIPDES=" + "'" + AFTIPDES + "'");
            cn.Update2(tabla, parametros, whereParam(), true);
            return cn.afectados;

        }


        protected override object whereParam()
        {
            string where = "AFTIPCOD='" + AFTIPCOD + "'" + "";


            return where;
        }

        protected override string CAMPOS()
        {
            string CAMPOS = "AFTIPCOD,AFTIPDES";
            return CAMPOS;   
        }


    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_AF_TIPO
//SP_Q_TIPO
//SP_INS_AF_TIPO
//SP_DEL_AF_TIPO
//SP_UPD_AF_TIPO
//SP_Q_AF_TIPO   