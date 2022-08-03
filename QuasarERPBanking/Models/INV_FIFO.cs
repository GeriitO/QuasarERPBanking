using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources.INV_TRANSACCIONES;
using System.Collections;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Globalization;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_FIFO : ConectDB
    {

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
        public string INVCOD { get; set; }

        [Display(Name = "lblCant", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCant")]
        public decimal INVCANT { get; set; }

        [Display(Name = "lblCosto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCosto")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n}")]
        public string INVCOSTO { get; set; }
        public string INVTPTRANS { get; set; }

        [Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime INVFECHATRANS { get; set; }
        public string strINVFECHATRANS      // se utiliza en el autocomplete de la vista
        {
            get
            {

                return INVFECHATRANS.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }
        public TimeSpan INVHORATRANS { get; set; }
        public DateTime INVFECHAMOV { get; set; }
        public TimeSpan INVHORAMOV { get; set; }
        public string INVUSUARIO { get; set; }
        public string INVNLOTE { get; set; }

        [Display(Name = "Depósito")]
        public string INVDEP { get; set; }

        [Display(Name = "lblRef", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrRef")]
        public string INVREF { get; set; }

        [Display(Name = "lblDoc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDoc")]
        public string INVDOC { get; set; }

        public string VALORACION { get; set; }


        public INV_FIFO()
        {
            Campo_Clave = "INVCOD";
        }





        //para el grid inv_maestro
        public static List<INV_FIFO> FifosDeINVCOD(string INVCOD)
        {


            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_FIFO> lista = new List<INV_FIFO>();
            string consulta = "SELECT INVCANT , INVCOSTO , INVFECHATRANS , INVDEP FROM " + ParametrosGlobales.bd + "  INV_FIFO WHERE INVCOD = '" + INVCOD + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        INV_FIFO inv_fifo = new INV_FIFO
                        {

                            INVCANT = decimal.Parse(item["INVCANT"].ToString()),
                            INVCOSTO = string.Format("{0:n}", decimal.Parse(item["INVCOSTO"].ToString())),  // para darle formato al numero     https://www.campusmvp.es/recursos/post/Chuleta-Formato-Cadenas-Texto-NET.aspx
                            //VALORACION = decimal.Parse(item["VALORACION"].ToString()),            ///invcosto y valoracion estaban decimal antes de aplicarles el formato
                            //VALORACION = INVCANT * INVCOSTO,
                            INVFECHATRANS = DateTime.Parse(item["INVFECHATRANS"].ToString()),
                            INVDEP = item["INVDEP"].ToString(),

                        };

                        string cantidad = (inv_fifo.INVCANT * decimal.Parse(inv_fifo.INVCOSTO)).ToString();
                        inv_fifo.VALORACION = string.Format("{0:n}", decimal.Parse(cantidad));
                        //inv_fifo.VALORACION = (inv_fifo.INVCANT * decimal.Parse(inv_fifo.INVCOSTO));
                        lista.Add(inv_fifo);
                    }
                }
            }
            return lista;
        }


        #region AUTOCOMPLETE BUSCA POR CODIGO
        public List<INV_FIFO> GetAutocom(string codigo, string depo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_FIFO> cantidades = new List<INV_FIFO>();
            string consulta = "SELECT ( CAST ( SUM ( INVCANT ) AS INTEGER ) ) - ( SELECT CASE WHEN CAST ( SUM ( INVCANT ) AS INTEGER ) IS NULL THEN 0 ELSE CAST (SUM(INVCANT) AS INTEGER ) END FROM " + ParametrosGlobales.bd + ".INV_TEMP WHERE TRIM(INVCOD) = '" + codigo + "' ) AS CANT FROM " + ParametrosGlobales.bd + "INV_FIFO WHERE TRIM(INVCOD) = '" + codigo + "' AND INVDEP = '" + depo + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.INV_FIFO cantidad = new Models.INV_FIFO();
                    cantidad.INVCANT = item["CANT"].ToString() == "" ? decimal.Parse("0") : decimal.Parse(item["CANT"].ToString());
                    cantidades.Add(cantidad);
                }
            }

            return cantidades;
        }
        #endregion

        //public static string getLote(string codigo, string depo)
        //{
        //    clsAS400 cn = new clsAS400();          
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(2);
        //    parametros.Add(codigo);
        //    parametros.Add(depo);
        //    cn.Execute2(ref codigo, "BICADM01W", "SP_Q_INVFIFOS", 100);
        //    return depo;
        //}




    }
}