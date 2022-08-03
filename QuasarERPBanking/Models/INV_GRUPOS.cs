using Resources.MANTENIMIENTO;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;


namespace QuasarERPBanking.Models
{
    public class INV_GRUPOS : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int INVGRUCOD { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string INVGRUDES { get; set; }
        [Display(Name = "lblCtainv", ResourceType = typeof(StringResources))]
        public string INVCTAINV { get; set; }
        [Display(Name = "lblCtagto", ResourceType = typeof(StringResources))]
        public string INVCTAGTO { get; set; }
        public bool ACTIVO { get; set; }
        public string USUARIO { get; set; }
        public DateTime FECHA { get; set; }

        static string sp_getAll = "SP_ALL_INV_GRUPOS";
        static string sp_getGrupo = "SP_INV_GRUPOS";

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<INV_GRUPOS> getGrupos_()
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_GRUPOS> grupos = new List<INV_GRUPOS>();
            string consulta = "SELECT  INVGRUCOD ,  INVGRUDES ,  INVCTAINV ,  INVCTAGTO  FROM " + ParametrosGlobales.bd + "  INV_GRUPOS  WHERE ACTIVO = '1'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    INV_GRUPOS grupo = new INV_GRUPOS();

                    //int.TryParse(item["INVGRUCOD"].ToString(), out codigo);

                    //int c = int.Parse(item["INVGRUCOD"].ToString());
                    //grupo.INVGRUCOD = codigo;
                    grupo.INVGRUCOD = int.Parse(item["INVGRUCOD"].ToString());
                    grupo.INVGRUDES = item["INVGRUDES"].ToString();
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }

        //public List<INV_GRUPOS> getGrupos(string term)
        public List<string> getGrupos(string term)
        {
            clsAS400 cn = new clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(term);
            List<object> reader = cn.execProc(sp_getGrupo, parametros, false);
            List<string> grupos = new List<string>();
            //List<INV_GRUPOS> grupos = new List<INV_GRUPOS>();
            //int codigo = 0;                    
            string grupo = "";
            //INV_GRUPOS grupo = new INV_GRUPOS();
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {

                    //int.TryParse(item["INVGRUCOD"].ToString(), out codigo);
                    //grupo.INVGRUCOD = codigo;
                    //grupo.INVGRUDES = item["INVGRUDES"].ToString();
                    grupo = item["INVGRUDES"].ToString();
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }

        public INV_GRUPOS()
        {
            Campo_Clave = "INVGRUCOD";
        }

        //llenar la lista completa de grupos para el mantenimiento
        public static List<INV_GRUPOS> GetGrupo()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_GRUPOS> lstGrupos = new List<INV_GRUPOS>();
            string consulta = "SELECT INVGRUCOD, INVGRUDES, INVCTAINV, INVCTAGTO, ACTIVO    FROM " + ParametrosGlobales.bd + "INV_GRUPOS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstGrupos.Add(setGrupo(item));
                }
            }
            return lstGrupos;
        }

        static INV_GRUPOS setGrupo(Row item)
        {
            INV_GRUPOS group = new INV_GRUPOS
            {

                INVGRUCOD = int.Parse(item["INVGRUCOD"].ToString()),
                INVGRUDES = item["INVGRUDES"].ToString(),
                INVCTAGTO = item["INVCTAGTO"].ToString(),
                INVCTAINV = item["INVCTAGTO"].ToString(),
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false,


            };
            return group;
        }
        static INV_GRUPOS setGrupo(DataRow item)
        {
            INV_GRUPOS group = new INV_GRUPOS
            {

                INVGRUCOD = int.Parse(item["INVGRUCOD"].ToString()),
                INVGRUDES = item["INVGRUDES"].ToString(),
                INVCTAGTO = item["INVCTAGTO"].ToString(),
                INVCTAINV = item["INVCTAGTO"].ToString(),
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false,


            };
            return group;
        }

        //cargar los datos de un grupo
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                INVGRUCOD = int.Parse(item["INVGRUCOD"].ToString());
                INVGRUDES = item["INVGRUDES"].ToString();
                INVCTAGTO = item["INVCTAGTO"].ToString();
                INVCTAINV = item["INVCTAGTO"].ToString();
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false;

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(5);
            parametros.Add(INVGRUCOD);
            parametros.Add(INVGRUDES);
            parametros.Add(INVCTAINV);
            parametros.Add(INVCTAGTO);
            parametros.Add(ACTIVO ? "1" : "0");
            parametros.Add(Util.ContactoActual());
            return parametros;
        }




    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_INV_GRUPOS
//SP_INV_GRUPOS
//SP_ALL_INVGRUPMANT
//SP_DEL_INV_GRUPOS 
//SP_INS_INV_GRUPOS 
//SP_Q_INV_GRUPOS 
//SP_UPD_INV_GRUPOS