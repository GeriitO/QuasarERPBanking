using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections;
using Resources.INV_PARAMETROS;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_PARAMETROS
    {
        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        //[Display(Name = "Nombre del Servidor:")]
        //[StringLength(40, ErrorMessage = "Error")]
        public string INVPARNOMBRE { get; set; }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errLote")]
        //[Display(Name = "N° de Lote:")]
        //[StringLength(10, ErrorMessage = "Error")]
        public string INVPARLOTE { get; set; }

        [Display(Name = "lblTrans", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTrans")]
        //[Display(Name = "N° de Transacción:")]
        //[StringLength(10, ErrorMessage = "Error")]
        public string INVPARIDTRANS { get; set; }

        [Display(Name = "lblInv", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errInv")]
        //[Display(Name = "Inventario:")]
        //[StringLength(15, ErrorMessage = "Error")]
        public string INVCTAINV { get; set; }

        [Display(Name = "lblGastos", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errGastos")]
        //[Display(Name = "Gastos:")]
        //[StringLength(15, ErrorMessage = "Error")]
        public string INVCTAGTOINV { get; set; }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        //[Display(Name = "N° de Lote:")]
        //[StringLength(10, ErrorMessage = "Error")]
        public string NEPARLOTE { get; set; }

        [Display(Name = "lblNE", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNE")]
        //[Display(Name = "N° N/E:")]
        //[StringLength(10, ErrorMessage = "Error")]
        public string NEPARCODELAB { get; set; }

        [Display(Name = "lblDetalles", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDetalles")]
        //[Display(Name = "N° de Detalles:")]
        //[StringLength(10, ErrorMessage = "Error")]
        public string NEPARIDTRANS { get; set; }

        [Display(Name = "lblExist", ResourceType = typeof(StringResources))]
        //[Display(Name = "Existencia")]
        public bool INVPARNEGAT { get; set; }

        [Display(Name = "lblDispon", ResourceType = typeof(StringResources))]
        //[Display(Name = "Cantidad Disponible")]
        public bool INVPARNEGDISP { get; set; }

        [Display(Name = "lblReserv", ResourceType = typeof(StringResources))]
        //[Display(Name = "Sin tomar en cuenta la reservación")]
        public bool NEPARSINRES { get; set; }

        [Display(Name = "lblCcosto", ResourceType = typeof(StringResources))]
        //[Display(Name = "Centros de Costos")]
        public bool INVPARINTCC { get; set; }

        [Display(Name = "lblDirCcosto", ResourceType = typeof(StringResources))]
        //[Display(Name = "Direc. de Centro de Costo:")]
        //[StringLength(50, ErrorMessage = "Error")]
        public string INVPARDIRCC { get; set; }

        [Display(Name = "lblMayGral", ResourceType = typeof(StringResources))]
        //[Display(Name = "Mayor General")]
        public bool INVPARINTMG { get; set; }

        [Display(Name = "lblDirMayGral", ResourceType = typeof(StringResources))]
        //[Display(Name = "Direc. Mayor General:")]
        //[StringLength(50, ErrorMessage = "Error")]
        public string INVPARDIRMG { get; set; }


        //[StringLength(1, ErrorMessage = "Error")]
        public TipoCosto INVPARSISCTO_ { get; set; }
        public string INVPARSISCTO { get; set; }

        //[StringLength(10, ErrorMessage = "Error")]
        public string INVPAROUTS { get; set; }
        //[StringLength(1, ErrorMessage = "Error")]
        public string INVPROCPAR { get; set; }
        //[StringLength(20, ErrorMessage = "Error")]
        public string INVPROCUSUARIO { get; set; }


        public static List<TipoCosto> lstINVPARSISCTO = new List<TipoCosto>
                {
                    new TipoCosto{ID="1" , Type = "Promedio"},
                    new TipoCosto{ID="2" , Type = "FIFO"},
                    new TipoCosto{ID="0" , Type = "Último"}



                };
        public INV_PARAMETROS Buscar()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT INVPARNOMBRE ,  INVPARLOTE ,  INVPARIDTRANS , INVCTAINV ,   INVCTAGTOINV , NEPARLOTE , NEPARCODELAB , NEPARIDTRANS , INVPARNEGAT ,  INVPARNEGDISP , NEPARSINRES , INVPARINTCC , INVPARDIRCC , INVPARINTMG , INVPARDIRMG ,  INVPARSISCTO , INVPAROUTS ,  INVPROCPAR , INVPROCUSUARIO FROM " + ParametrosGlobales.bd + "INV_PARAMETROS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    INVPARNOMBRE = item["INVPARNOMBRE"].ToString();
                    INVPARLOTE = item["INVPARLOTE"].ToString().Replace("LM", "");
                    INVPARIDTRANS = item["INVPARIDTRANS"].ToString().Replace("LM", "");
                    INVCTAINV = item["INVCTAINV"].ToString();
                    INVCTAGTOINV = item["INVCTAGTOINV"].ToString();
                    NEPARLOTE = item["NEPARLOTE"].ToString().Replace("LM", "");
                    NEPARCODELAB = item["NEPARCODELAB"].ToString().Replace("LM", "");
                    NEPARIDTRANS = item["NEPARIDTRANS"].ToString().Replace("LM", "");
                    INVPARNEGAT = (item["INVPARNEGAT"].ToString() == "0" ? false : true);
                    INVPARNEGDISP = (item["INVPARNEGDISP"].ToString() == "0" ? false : true);
                    NEPARSINRES = (item["NEPARSINRES"].ToString() == "0" ? false : true);
                    INVPARINTCC = (item["INVPARINTCC"].ToString() == "0" ? false : true);
                    INVPARDIRCC = item["INVPARDIRCC"].ToString();
                    INVPARINTMG = (item["INVPARINTMG"].ToString() == "0" ? false : true);
                    INVPARDIRMG = item["INVPARDIRMG"].ToString();
                    INVPARSISCTO_ = new TipoCosto
                    {
                        ID = item["INVPARSISCTO"].ToString()
                    };
                    INVPARSISCTO = item["INVPARSISCTO"].ToString();
                    INVPAROUTS = item["INVPAROUTS"].ToString();
                    INVPROCPAR = item["INVPROCPAR"].ToString();
                    INVPROCUSUARIO = item["INVPROCUSUARIO"].ToString();


                }

            }
            return this;
        }

        public bool Existe()
        {
            return true;
        }

        public int Update()
        {
            int resultado = 0;
            Models.clsAS400 cn = new Models.clsAS400();
            //INVPARSISCTO = "0";
            INVPAROUTS = "";
            INVPROCPAR = "";
            INVPROCUSUARIO = "";

            ArrayList parametros = new ArrayList(19);
            parametros.Add(INVPARNOMBRE);
            parametros.Add(INVPARLOTE);
            parametros.Add(INVPARIDTRANS);
            parametros.Add(INVCTAINV);
            parametros.Add(INVCTAGTOINV);
            parametros.Add(NEPARLOTE);
            parametros.Add(NEPARCODELAB);
            parametros.Add(NEPARIDTRANS);
            parametros.Add(INVPARNEGAT ? "1" : "0");
            parametros.Add(INVPARNEGDISP ? "1" : "0");
            parametros.Add(NEPARSINRES ? "1" : "0");
            parametros.Add(INVPARINTCC ? "1" : "0");
            parametros.Add(INVPARDIRCC);
            parametros.Add(INVPARINTMG ? "1" : "0");
            parametros.Add(INVPARDIRMG);
            parametros.Add(INVPARSISCTO);
            parametros.Add(INVPAROUTS);
            parametros.Add(INVPROCPAR);
            parametros.Add(INVPROCUSUARIO);

            cn.execProc("SP_UPD_INV_PARAMETROS", parametros, true);

            //return cn.afectados;
            return resultado;
        }



        public static string TCosto()
        {
            string tcosto = "";
            string INVPARSISCTO = "";
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT    INVPARSISCTO FROM " + ParametrosGlobales.bd + "  INV_PARAMETROS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    INVPARSISCTO = item["INVPARSISCTO"].ToString();
                }

                tcosto = (lstINVPARSISCTO.Find(x => x.ID.Contains(INVPARSISCTO))).Type;
            }
            return tcosto;
        }
    }


    public class TipoCosto
    {
        public string ID { get; set; }
        public string Type { get; set; }
    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_Q_INV_PARAMETROS
//SP_UPD_INV_PARAMETROS
//SP_GET_TCOSTO
