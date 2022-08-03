using Resources.INV_TRANSACCIONES;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_TRANSACCIONES :ConectDB
    {
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]       
        public string INVTRANSCOD { get; set; }

        [Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime INVTRANSFECHA { get; set; }
        public string strINVTRANSFECHA      // se utiliza en el autocomplete de la vista
        {
            get
            {

                return INVTRANSFECHA.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblFechMov", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFechMov")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime INVTRANSFECHAMOV { get; set; }
        public string strINVTRANSFECHAMOV       // se utiliza en el autocomplete de la vista
        {
            get
            {

                return INVTRANSFECHAMOV.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblTipo", ResourceType = typeof(StringResources))]
        public string INVTRANSTIPO { get; set; }

        [Display(Name = "lblCant", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCant")]
        public decimal INVTRANSCANT { get; set; }

        [Display(Name = "lblCosto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCosto")]
        public decimal INVTRANSCOSTO { get; set; }

        //public decimal COSTOCAL { get; set; }

        public decimal INVTRANSPRECIO { get; set; }

        [Display(Name = "lblRef", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrRef")]
        public string INVTRANSREF { get; set; }

        [Display(Name = "lblIva", ResourceType = typeof(StringResources))]
        public decimal INVTRANSIVA { get; set; }

        public int INVTRANSDEP { get; set; }        //PASA SIEMPRE CON 1

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrLote")]
        public string INVTRANSLOTE { get; set; }

        [Display(Name = "lblUsuario", ResourceType = typeof(StringResources))]
        public string USUARIO { get; set; }

        [Display(Name = "lblId", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrId")]
        public string INVTRANSID { get; set; }

        [Display(Name = "lblDoc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDoc")]
        public string INVTRANSDOC { get; set; }     
        public string INVTRANSCC { get; set; }

        // public int INV_TR_AUTOID { get; set; }
        public Inventario inv_maestro { get; set; }


        public INV_TRANSACCIONES()
        {
            //sp_Insert = "SP_INS_INVTRANSACC";
            //sp_Find = "SP_Q_INVTRANSACC";
            Campo_Clave = "INVTRANSCOD";
        }

        //SE UTILIZA PARA INSERT Y UPDATE
        protected override ArrayList getParameters()
        {


            USUARIO = Util.ContactoActual();
            INVTRANSIVA = 0;
            INVTRANSPRECIO = 0;
            //INVTRANSDEP = 1;

            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(15);

            parametros.Add(INVTRANSCOD);
            parametros.Add(INVTRANSFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(INVTRANSFECHAMOV.ToString("yyyy-MM-dd"));
            parametros.Add(INVTRANSTIPO);
            //parametros.Add(TRANS_MONTO.ToString("#.####"));   
            //parametros.Add(TRANS_MONTO.ToString().Replace(".", ","));
            parametros.Add(INVTRANSCOSTO.ToString().Replace(",", "."));
            parametros.Add(INVTRANSPRECIO);
            parametros.Add(INVTRANSREF);
            parametros.Add(INVTRANSIVA);
            parametros.Add(INVTRANSDEP);
            parametros.Add(INVTRANSLOTE);
            parametros.Add(USUARIO);
            parametros.Add(INVTRANSID);
            parametros.Add(INVTRANSCANT);
            parametros.Add(INVTRANSDOC);
            parametros.Add(INVTRANSCC);




            return parametros;
        }


        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                INVTRANSCOD = item["INVTRANSCOD"].ToString();
                INVTRANSFECHA = DateTime.Parse(item["INVTRANSFECHA"].ToString());
                INVTRANSFECHAMOV = DateTime.Parse(item["INVTRANSFECHAMOV"].ToString());
                INVTRANSTIPO = item["INVTRANSTIPO"].ToString();
                INVTRANSCOSTO = decimal.Parse(item["INVTRANSCOSTO"].ToString());
                INVTRANSPRECIO = decimal.Parse(item["INVTRANSPRECIO"].ToString());
                INVTRANSREF = item["INVTRANSREF"].ToString();
                INVTRANSIVA = decimal.Parse(item["INVTRANSIVA"].ToString());
                INVTRANSDEP = int.Parse(item["INVTRANSDEP"].ToString());
                INVTRANSLOTE = item["INVTRANSLOTE"].ToString();
                USUARIO = item["USUARIO"].ToString();
                INVTRANSID = item["INVTRANSID"].ToString();
                INVTRANSCANT = decimal.Parse(item["INVTRANSCANT"].ToString());
                INVTRANSDOC = item["INVTRANSDOC"].ToString();
                INVTRANSCC = item["INVTRANSCC"].ToString();
            }

            return this;
        }


        public static string getLote()
        {
            clsAS400 cn = new clsAS400();
            string prefijo = "LM";
            string lote = "";
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(2);
            parametros.Add(prefijo);
            parametros.Add(lote);
            cn.Execute2(ref prefijo, "BICADM01W", "SP_Q_INVTRANS_LOTE", 100);
            return prefijo;
        }

        #region AUTOCOMPLETE BUSCA POR PRODUCTO


        public List<string> GetCampos(string codigo)        //<<-- NO TIENE NINGUN PROCESO
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);

            Rows reader = cn.execProc("SP_Q_AUTCOMINVPROD", parametros, false);
            List<string> productos = new List<string>();

            if (reader != null)
            {
                foreach (Row item in reader)
                {
                    string producto = "";
                    producto = item["AFCOD"].ToString();
                    productos.Add(producto);
                }
            }

            return productos;
        }
        #endregion








    }
}