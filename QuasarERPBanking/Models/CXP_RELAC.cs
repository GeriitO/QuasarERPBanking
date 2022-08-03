using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.MANTENIMIENTO;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CXP_RELAC : clsMain
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int CXPRELACID { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string CXPRELAC { get; set; }
        public string USUARIO { get; set; }
        public bool ACTIVO { get; set; }
        public DateTime FECHA { get; set; }

      
        static string sp_getAll = "SP_ALL_CXP_RELAC";       //PARA EL DROPDOWNLIST
        static string sp_getAllM = "SP_ALL_CXP_RELACMANT";  //PARA EL MANTENIMIENTO

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<CXP_RELAC> GetRelaciones()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_RELAC> relaciones = new List<CXP_RELAC>();
            string consulta = "SELECT  CXPRELACID ,  CXPRELAC ,  ACTIVO FROM BICADM01W.CXP_RELAC WHERE ACTIVO = '1' ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CXP_RELAC relacion = new CXP_RELAC();

                    //int.TryParse( item["CXPRELACID"].ToString(), out codigo);
                    //relacion.CXPRELACID = codigo;
                    relacion.CXPRELAC = item["CXPRELAC"].ToString();
                    relaciones.Add(relacion);
                }
            }

            return relaciones;
        }

        public CXP_RELAC()
        {
            Campo_Clave = "CXPRELACID";
        }

        //llenar la lista completa de formas de pago para el mantenimiento
        public static List<CXP_RELAC> GetRelacion()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_RELAC> lstRelacion = new List<CXP_RELAC>();
            string consulta = "SELECT  CXPRELACID ,  CXPRELAC ,  ACTIVO  FROM BICADM01W . CXP_RELAC ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstRelacion.Add(setRelacion(item));
                }
            }
            return lstRelacion;
        }

        static CXP_RELAC setRelacion(Row item)
        {
            CXP_RELAC Relacion = new CXP_RELAC
            {
                CXPRELACID = int.Parse(item["CXPRELACID"].ToString()),
                CXPRELAC = item["CXPRELAC"].ToString(),
                //USUARIO = item["USUARIO"].ToString(),
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false,
                //FECHA = DateTime.Parse(item["FECHA"].ToString()),



            };
            return Relacion;
        }

        static CXP_RELAC setRelacion(DataRow item)
        {
            CXP_RELAC Relacion = new CXP_RELAC
            {
                CXPRELACID = int.Parse(item["CXPRELACID"].ToString()),
                CXPRELAC = item["CXPRELAC"].ToString(),
                //USUARIO = item["USUARIO"].ToString(),
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false,
                //FECHA = DateTime.Parse(item["FECHA"].ToString()),



            };
            return Relacion;
        }

        //cargar los datos de una sola forma de pago
        protected override clsMain LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                CXPRELACID = int.Parse(item["CXPRELACID"].ToString());
                CXPRELAC = item["CXPRELAC"].ToString();
                //USUARIO = item["USUARIO"].ToString();
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false;
                //FECHA = DateTime.Parse(item["FECHA"].ToString());

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(4);
            parametros.Add(CXPRELACID);
            parametros.Add(CXPRELAC);
            parametros.Add(ACTIVO ? "1" : "0");
            parametros.Add(Util.ContactoActual());
     
            return parametros;
        }




    }

}

//////////////// SP UTILIZADOS ////////////////
//SP_ALL_CXP_RELAC   
//SP_ALL_CXP_RELACMANT
//SP_DEL_CXP_RELAC
//SP_INS_CXP_RELAC
//SP_Q_CXP_RELAC
//SP_UPD_CXP_RELAC

