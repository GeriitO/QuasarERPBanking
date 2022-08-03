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
    public class FORMAPAGO : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int CODIGO { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string DESCRIPCION { get; set; }
        [Display(Name = "lblDespe", ResourceType = typeof(StringResources))]
        public string DESPEQ { get; set; }


        static string sp_getAll = "SP_ALL_FORMAPAGO";


        //para cargar el dropdownlist de los formularios
        public static IEnumerable<FORMAPAGO> GetFormaPago()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<FORMAPAGO> FormaPagos = new List<FORMAPAGO>();
            string consulta = "SELECT CODIGO ,  DESCRIPCION , DESPEQ  FROM BICADM01W . FORMAPAGO";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    FORMAPAGO FormaPago = new FORMAPAGO();

                    FormaPago.DESCRIPCION = item["DESCRIPCION"].ToString();
                    FormaPagos.Add(FormaPago);
                }
            }
            return FormaPagos;
        }

        public FORMAPAGO()
        {
            Campo_Clave = "CODIGO";
        }

        //llenar la lista completa de formas de pago para el mantenimiento
        public static List<FORMAPAGO> GetFrPago()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<FORMAPAGO> lstFrPago = new List<FORMAPAGO>();
            string consulta = "SELECT CODIGO ,  DESCRIPCION , DESPEQ  FROM BICADM01W . FORMAPAGO";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstFrPago.Add(setFrPago(item));
                }
            }
            return lstFrPago;
        }

        static FORMAPAGO setFrPago(Row item)
        {
            FORMAPAGO frpago = new FORMAPAGO
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                DESCRIPCION = item["DESCRIPCION"].ToString(),
                DESPEQ = item["DESPEQ"].ToString(),

            };
            return frpago;
        }

        static FORMAPAGO setFrPago(DataRow item)
        {
            FORMAPAGO frpago = new FORMAPAGO
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                DESCRIPCION = item["DESCRIPCION"].ToString(),
                DESPEQ = item["DESPEQ"].ToString(),

            };
            return frpago;
        }

        //cargar los datos de una sola forma de pago
        protected override ConectDB LoadObject(Rows reader)
        {
            foreach (Row item in reader)
            {
                CODIGO = int.Parse(item["CODIGO"].ToString());
                DESCRIPCION = item["DESCRIPCION"].ToString();
                DESPEQ = item["DESPEQ"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(3);
            parametros.Add(CODIGO);
            parametros.Add(DESCRIPCION);
            parametros.Add(DESPEQ);
            return parametros;
        }


        public List<FORMAPAGO> GETFORMAPAGO()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<FORMAPAGO> listafinal = new List<FORMAPAGO>();
            string consulta = "select * from bicadm01w.formapago ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.FORMAPAGO listas = new Models.FORMAPAGO();

                    listas.DESCRIPCION = item["DESCRIPCION"].ToString();
                    listafinal.Add(listas);
                }
            }
            return listafinal;
        }
    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_FORMAPAGO 
//SP_DEL_FORMAPAGO 
//SP_INS_FORMAPAGO  
//SP_Q_FORMAPAGO  
//SP_UPD_FORMAPAGO  