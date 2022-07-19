using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.ActivoFijoGrupos;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using QuasarERPBanking.Models;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class ActivoFijoGrupos : ConectDB
    {
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
        public string AFGRUCOD { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDesc")]
        public string AFGRUDES { get; set; }

        [Display(Name = "lblCtaActivo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCtaActivo")]
        public string AFGRUCTAACT { get; set; }

        [Display(Name = "lblCtaDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCtaDep")]
        public string AFGRUCTADEP { get; set; }

        [Display(Name = "lblCtaGasDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCtaGasDep")]
        public string AFGRUCTAGTODEP { get; set; }

        [Display(Name = "lblMesesDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMesesDep")]
        public int AFGRUMESES { get; set; }

        public string AFCTAACTTD { get; set; }
        public string AFCTADEPTD { get; set; }
        public string AFCTAGTOTD { get; set; }


        static string sp_getAll = "SP_ALL_AF_GRUPOS";



        public ActivoFijoGrupos()
        {
            Campo_Clave = "AFGRUCOD";
            tabla = "ActivoFijoGrupos";

        }

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<ActivoFijoGrupos> GetGrupo()
        {


            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ActivoFijoGrupos> grupos = new List<ActivoFijoGrupos>();
            string consulta = "SELECT  AFGRUCOD ,  AFGRUDES  FROM " + ParametrosGlobales.bd + " ActivoFijoGrupos";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];



            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActivoFijoGrupos grupo = new ActivoFijoGrupos();
                    grupo.AFGRUCOD = item["AFGRUCOD"].ToString();
                    grupo.AFGRUDES = item["AFGRUDES"].ToString();
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }



        //#region AUTOCOMPLETE BUSCA POR CODIGO GRUPO


        //public List<string> GetCampos(string codigo)
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
        //    parametros.Add(codigo);

        //    Rows reader = cn.execProc("SP_Q_AF_GRUPO", parametros, false);
        //    List<string> grupos = new List<string>();

        //    if (reader != null)
        //    {
        //        foreach (Row item in reader)
        //        {
        //            string grupo = "";
        //            grupo = item["AF_ETIQ"].ToString();
        //            grupos.Add(grupo);
        //        }
        //    }

        //    return grupos;
        //}
        //#endregion


        //llenar la lista completa de todos los usuarios en el grid
        public static List<ActivoFijoGrupos> GetGrupos()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<ActivoFijoGrupos> lstGrupos = new List<ActivoFijoGrupos>();
            string consulta = "SELECT AFGRUCOD , AFGRUDES , AFGRUCTAACT , AFGRUCTADEP , AFGRUCTAGTODEP , AFGRUMESES FROM " + ParametrosGlobales.bd + " ActivoFijoGrupos";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    lstGrupos.Add(setGrupos(item));
                }
            }
            return lstGrupos;
        }


        static ActivoFijoGrupos setGrupos(DataRow item)
        {
            ActivoFijoGrupos grupo = new ActivoFijoGrupos
            {
                AFGRUCOD = item["AFGRUCOD"].ToString(),
                AFGRUDES = item["AFGRUDES"].ToString(),
                AFGRUCTAACT = item["AFGRUCTAACT"].ToString(),
                AFGRUCTADEP = item["AFGRUCTADEP"].ToString(),
                AFGRUCTAGTODEP = item["AFGRUCTAGTODEP"].ToString(),
                AFGRUMESES = int.Parse(item["AFGRUMESES"].ToString()),
                //AFCTAACTTD = item["AFCTAACTTD"].ToString(),
                //AFCTADEPTD = item["AFCTADEPTD"].ToString(),
                //AFCTAGTOTD = item["AFCTAGTOTD"].ToString(),
            };
            return grupo;
        }

        //cargar datos de un grupo
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                AFGRUCOD = item["AFGRUCOD"].ToString();
                AFGRUDES = item["AFGRUDES"].ToString();
                AFGRUCTAACT = item["AFGRUCTAACT"].ToString();
                AFGRUCTADEP = item["AFGRUCTADEP"].ToString();
                AFGRUCTAGTODEP = item["AFGRUCTAGTODEP"].ToString();
                AFGRUMESES = int.Parse(item["AFGRUMESES"].ToString());
                //AFCTAACTTD      = item["AFCTAACTTD"].ToString();
                //AFCTADEPTD      = item["AFCTADEPTD"].ToString();
                //AFCTAGTOTD      = item["AFCTAGTOTD"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(6);

            parametros.Add(AFGRUCOD);
            parametros.Add(AFGRUDES);
            parametros.Add(AFGRUCTAACT);
            parametros.Add(AFGRUCTADEP);
            parametros.Add(AFGRUCTAGTODEP);
            parametros.Add(AFGRUMESES);
            //parametros.Add(AFCTAACTTD);
            //parametros.Add(AFCTADEPTD);
            //parametros.Add(AFCTAGTOTD);
            return parametros;
        }



        public int Insert()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            //AFCODOLD = "0"; 

            parametros.Add(AFGRUCOD);
            parametros.Add(AFGRUDES);
            parametros.Add(AFGRUCTAACT);
            parametros.Add(AFGRUCTADEP);
            parametros.Add(AFGRUCTAGTODEP);
            parametros.Add(AFGRUMESES);

            cn.Insert2(tabla, parametros, true, CAMPOS());
            return cn.afectados;

        }

        protected override string CAMPOS()
        {
            string CAMPOS = "AFGRUCOD,AFGRUDES,AFGRUCTAACT,AFGRUCTADEP,AFGRUCTAGTODEP,AFGRUMESES";
            return CAMPOS;
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
        public int getParametersUpd()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);
            


            parametros.Add("AFGRUCOD =" + "'" + AFGRUCOD + "'");
            parametros.Add("AFGRUDES=" + "'" + AFGRUDES + "'");
            parametros.Add("AFGRUCTAACT=" + "'" + AFGRUCTAACT + "'");
            parametros.Add("AFGRUCTADEP=" + "'" + AFGRUCTADEP + "'");
            parametros.Add("AFGRUCTAGTODEP=" + "'" + AFGRUCTAGTODEP + "'");
            parametros.Add("AFGRUMESES=" + "'" + AFGRUMESES + "'");


            cn.Update2(tabla, parametros, whereParam(), true);
            return cn.afectados;

        }


        protected override object whereParam()
        {
            string where = ""+Campo_Clave+"='" + AFGRUCOD + "'" + "";


            return where;
        }

    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_ALL_AF_GRUPOS
//SP_Q_GRUPOS
//SP_INS_AF_GRUPOS
//SP_DEL_AF_GRUPOS
//SP_UPD_AF_GRUPOS
//SP_Q_AF_GRUPOS   