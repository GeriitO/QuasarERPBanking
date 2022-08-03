using Resources.ActivoFijoTransacciones;
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
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Models
{
    public class ActivoFijoTransacciones : ConectDB
    {

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
        public string TRANS_PROD { get; set; }

        [Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime TRANS_FECHA { get; set; }
        public string strAFTRANSFECHA      // se utiliza en el autocomplete de la vista
        {
            get
            {

                return TRANS_FECHA.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblFechMov", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFechMov")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime TRANS_FECHAMOV { get; set; }
        public string strAFTRANSFECHAMOV       // se utiliza en el autocomplete de la vista
        {
            get
            {

                return TRANS_FECHAMOV.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }
        [Display(Name = "lblTipo", ResourceType = typeof(StringResources))]
        public string TRANS_TIPO { get; set; }

        [Display(Name = "lblCosto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCosto")]
        public decimal TRANS_MONTO { get; set; }

        [Display(Name = "lblRef", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrRef")]
        public string TRANS_REF { get; set; }

        [Display(Name = "lblIva", ResourceType = typeof(StringResources))]
        public double TRANS_IVA { get; set; }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrLote")]
        public string TRANS_LOTE { get; set; }

        [Display(Name = "lblUsuario", ResourceType = typeof(StringResources))]
        public string TRANS_USUARIO { get; set; }

        [Display(Name = "lblId", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrId")]
        public string TRANS_ID { get; set; }

        [Display(Name = "lblCant", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCant")]
        public int TRANS_CANT { get; set; }

        [Display(Name = "lblDoc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDoc")]
        public string TRANS_DOC { get; set; }


        public string AFPARLOTE_PROD { get; set; }

        public Productos Productos { get; set; }


        //SP_INS_TRANSACCIONES
        //SP_Q_AF_MAESTRO

        public ActivoFijoTransacciones()
        {
            tabla = "AF_PROD_TRANSACCIONES";
            Campo_Clave = "TRANS_PROD";
            CampoLote = "AFPARLOTE_PROD";
            tablalote = "AF_PARAMETROS";
        }

        //SE UTILIZA PARA INSERT Y UPDATE
        protected override ArrayList getParameters()
        {


            TRANS_USUARIO = Util.ContactoActual();
            TRANS_IVA = 0;

            ConectDB cn = new ConectDB();
            ArrayList parametros = new ArrayList(12);

            parametros.Add(TRANS_PROD);
            parametros.Add(TRANS_FECHA.ToString("dd-MM-yyyy"));
            parametros.Add(TRANS_FECHAMOV.ToString("dd-MM-yyyy"));
            parametros.Add(TRANS_TIPO);
            parametros.Add(TRANS_MONTO.ToString().Replace(",", "."));
            parametros.Add(TRANS_REF);
            parametros.Add(TRANS_IVA);
            parametros.Add(TRANS_LOTE);
            parametros.Add(TRANS_USUARIO);
            parametros.Add(TRANS_ID);
            parametros.Add(TRANS_CANT);
            parametros.Add(TRANS_DOC);




            return parametros;
        }


        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                TRANS_PROD = item["TRANS_PROD"].ToString();
                TRANS_FECHA = DateTime.Parse(item["TRANS_FECHA"].ToString());
                TRANS_FECHAMOV = DateTime.Parse(item["TRANS_FECHAMOV"].ToString());
                TRANS_TIPO = item["TRANS_TIPO"].ToString();
                TRANS_MONTO = decimal.Parse(item["TRANS_MONTO"].ToString());
                TRANS_REF = item["TRANS_REF"].ToString();
                TRANS_IVA = double.Parse(item["TRANS_IVA"].ToString());
                TRANS_LOTE = item["TRANS_LOTE"].ToString();
                TRANS_USUARIO = item["TRANS_USUARIO"].ToString();
                TRANS_ID = item["TRANS_ID"].ToString();
                TRANS_CANT = int.Parse(item["TRANS_CANT"].ToString());
                TRANS_DOC = item["TRANS_DOC"].ToString();

            }

            return this;
        }





        public ActivoFijoTransacciones getLote()  //<--- ESTE PROCESO LO PUEDE REVISAR A VER COMO QUEDO SI ESTA BIEN PUEDE QUITAR ESTE COMENTARIO.
        {
            ConectDB cn = new ConectDB();
            ConectDB objeto = null;
            Rows reader = cn.NumeroLote(tablalote, CampoLote);
            if (reader != null)
            {
                objeto = CargarObjeto(reader);
            }
            return this;
        }

        protected override ConectDB CargarObjeto(Rows dt)
        {
            foreach (Row item in dt)
            {


                AFPARLOTE_PROD = item["AFPARLOTE_PROD"].ToString();

            }
            return this;

        }


        #region AUTOCOMPLETE BUSCA POR PRODUCTO


        public List<string> GetCampos(string codigo)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> productos = new List<string>();
            string consulta = "SELECT AFCOD FROM " + ParametrosGlobales.bd + "AF_MAESTRO WHERE ( UPPER ( AFCOD ) LIKE CONCAT ( UPPER ( '" + codigo + "' ) , '%' ) ) ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string producto = "";
                    producto = item["AFCOD"].ToString();
                    productos.Add(producto);
                }
            }

            return productos;
        }
        #endregion




        protected override ConectDB LoadLote(OleDbDataReader reader)
        {

            while (reader.Read())
            {

                AFPARLOTE_PROD = reader["AFPARLOTE_PROD"].ToString();



            }

            return this;
        }

        protected override ConectDB LoadObject54(OleDbDataReader reader)
        {

            while (reader.Read())
            {
                AFPARLOTE_PROD = reader["AFPARLOTE_PROD"].ToString();

            }

            return this;
        }


        protected override string CAMPOS()
        {
            string CAMPOS = "TRANS_PROD ,TRANS_FECHA ,TRANS_FECHAMOV ,TRANS_TIPO ,TRANS_MONTO ,TRANS_REF ,TRANS_IVA ,TRANS_LOTE ,TRANS_USUARIO ,TRANS_ID ,TRANS_CANT ,TRANS_DOC";
            return CAMPOS;
        }

    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_INS_TRANSACCIONES
//SP_Q_TRANSACCIONES
//SP_Q_AUTCOMAFMA
