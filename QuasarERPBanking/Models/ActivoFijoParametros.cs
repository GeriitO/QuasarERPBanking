using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using QuasarERPBanking.Models;
using Resources;

namespace QuasarERPBanking.Models
{
    public class ActivoFijoParametros
    {
        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNom")]
        public string AFPARNOMBRE { get; set; }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrLote")]
        public string AFPARLOTE { get; set; }

        [Display(Name = "lblTrans", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrTrans")]
        public string AFPARIDTRANS { get; set; }
        public string AFPARIDASIG { get; set; }


        [Display(Name = "lblMejora", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMejora")]
        public string AFPARMEJID { get; set; }
        public string AFPARCOMPNB { get; set; }
        public string AFPARCOMPRIF { get; set; }

        [Display(Name = "lblMayor", ResourceType = typeof(StringResources))]
        public bool AFPARINTMG { get; set; }

        [Display(Name = "lblDir", ResourceType = typeof(StringResources))]
        public string AFPARDIRMG { get; set; }

        [Display(Name = "lblCCosto", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCCosto")]
        public bool AFPARINTCC { get; set; }

        [Display(Name = "lblDir", ResourceType = typeof(StringResources))]
        public string AFPARDIRCC { get; set; }

        [Display(Name = "lblCuePagar", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCuePagar")]
        public bool AFPARINTCXP { get; set; }

        [Display(Name = "lblDir", ResourceType = typeof(StringResources))]
        public string AFPARDIRCXP { get; set; }



        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrLote")]
        public string AFPARLOTE_PROD { get; set; }



        [Display(Name = "lblTrans", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrTrans")]
        public string AFPARIDTRANS_PROD { get; set; }


        [Display(Name = "lblMes", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMes")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AFPAR_ULTFECHADEP { get; set; }
        public string strAFPARULTFECHADEP       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AFPAR_ULTFECHADEP.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrLote")]
        public string AFPAR_LOTEDEP { get; set; }

        [Display(Name = "lblNE", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNE")]
        public string NEPARCODELAB { get; set; }

        [Display(Name = "lblDetalle", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDetalle")]
        public string NEPARIDTRANS { get; set; }

        [Display(Name = "lblActivo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrActivo")]
        public string AFPARCTAACT { get; set; }

        [Display(Name = "lblDepre", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDepre")]
        public string AFPARCTADEP { get; set; }

        [Display(Name = "lblGastos", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrGastos")]
        public string AFPARCTAGTO { get; set; }
        public string AFDEPSW { get; set; }





        public ActivoFijoParametros Buscar()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            ArrayList parametros = new ArrayList(0);
            string consulta = "SELECT AFPARNOMBRE , AFPARLOTE , AFPARIDTRANS , AFPARIDASIG , AFPARMEJID , AFPARCOMPNB , AFPARCOMPRIF , AFPARINTMG , AFPARDIRMG , AFPARINTCC , AFPARDIRCC , AFPARINTCXP , AFPARDIRCXP , AFPARLOTE_PROD , AFPARIDTRANS_PROD , AFPAR_ULTFECHADEP , AFPAR_LOTEDEP , NEPARCODELAB , NEPARIDTRANS , AFPARCTAACT , AFPARCTADEP , AFPARCTAGTO , AFDEPSW FROM " + ParametrosGlobales.bd + " ActivoFijoParametros";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    AFPARNOMBRE = item["AFPARNOMBRE"].ToString();
                    AFPARLOTE = item["AFPARLOTE"].ToString();
                    AFPARIDTRANS = item["AFPARIDTRANS"].ToString();
                    AFPARIDASIG = item["AFPARIDASIG"].ToString();
                    AFPARMEJID = item["AFPARMEJID"].ToString();
                    AFPARCOMPNB = item["AFPARCOMPNB"].ToString();
                    AFPARCOMPRIF = item["AFPARCOMPRIF"].ToString();
                    AFPARINTMG = (item["AFPARINTMG"].ToString() == "0" ? false : true);
                    AFPARDIRMG = item["AFPARDIRMG"].ToString();
                    AFPARINTCC = (item["AFPARINTCC"].ToString() == "0" ? false : true);
                    AFPARDIRCC = item["AFPARDIRCC"].ToString();
                    AFPARINTCXP = (item["AFPARINTCXP"].ToString() == "0" ? false : true);
                    AFPARDIRCXP = item["AFPARDIRCXP"].ToString();
                    AFPARLOTE_PROD = item["AFPARLOTE_PROD"].ToString();
                    AFPARIDTRANS_PROD = item["AFPARIDTRANS_PROD"].ToString();
                    AFPAR_ULTFECHADEP = DateTime.Parse(item["AFPAR_ULTFECHADEP"].ToString());
                    AFPAR_LOTEDEP = item["AFPAR_LOTEDEP"].ToString();
                    NEPARCODELAB = item["NEPARCODELAB"].ToString();
                    NEPARIDTRANS = item["NEPARIDTRANS"].ToString();
                    AFPARCTAACT = item["AFPARCTAACT"].ToString();
                    AFPARCTADEP = item["AFPARCTADEP"].ToString();
                    AFPARCTAGTO = item["AFPARCTAGTO"].ToString();
                    AFDEPSW = item["AFDEPSW"].ToString();



                }

            }
            return this;
        }

        public bool Existe()
        {
            return true;
        }

        public int Update() //<<<<<<<<<<<<<<<------- REVISAR ESTE CODIGO 
        {
            int resultado = 0;
            Models.clsAS400 cn = new Models.clsAS400();

            AFPARDIRMG = "";
            AFPARDIRCXP = "";
            AFPARIDASIG = "";
            AFPARCOMPNB = "";
            AFPARCOMPRIF = "";
            AFPARCTAGTO = "";
            AFDEPSW = "";


            ArrayList parametros = new ArrayList(23);


            parametros.Add(AFPARNOMBRE);
            parametros.Add(AFPARLOTE);
            parametros.Add(AFPARIDTRANS);
            parametros.Add(AFPARIDASIG);
            parametros.Add(AFPARMEJID);
            parametros.Add(AFPARCOMPNB);
            parametros.Add(AFPARCOMPRIF);
            parametros.Add(AFPARINTMG ? "1" : "0");
            parametros.Add(AFPARDIRMG);
            parametros.Add(AFPARINTCC ? "1" : "0");
            parametros.Add(AFPARDIRCC);
            parametros.Add(AFPARINTCXP ? "1" : "0");
            parametros.Add(AFPARDIRCXP);
            parametros.Add(AFPARLOTE_PROD);
            parametros.Add(AFPARIDTRANS_PROD);
            parametros.Add(AFPAR_ULTFECHADEP.ToString("yyyy-MM-dd"));
            parametros.Add(AFPAR_LOTEDEP);
            parametros.Add(NEPARCODELAB);
            parametros.Add(NEPARIDTRANS);
            parametros.Add(AFPARCTAACT);
            parametros.Add(AFPARCTADEP);
            parametros.Add(AFPARCTAGTO);
            parametros.Add(AFDEPSW);

            cn.execProc("SP_UPD_AF_PARAMETROS", parametros, true);

            //return cn.afectados;                                             
            return resultado;
        }






    }
}


//////////// CAMPOS DE LA TABLA //////////
//AFPARNOMBRE
//AFPARLOTE
//AFPARIDTRANS
//AFPARIDASIG
//AFPARMEJID
//AFPARCOMPNB
//AFPARCOMPRIF
//AFPARINTMG
//AFPARDIRMG
//AFPARINTCC
//AFPARDIRCC
//AFPARINTCXP
//AFPARDIRCXP
//AFPARLOTE_PROD
//AFPARIDTRANS_PROD
//AFPAR_ULTFECHADEP
//AFPAR_LOTEDEP
//NEPARCODELAB
//NEPARIDTRANS
//AFPARCTAACT
//AFPARCTADEP
//AFPARCTAGTO
//AFDEPSW




//////////////// SP UTILIZADOS ////////////////
//SP_Q_AF_PARAMETROS
//SP_UPD_AF_PARAMETROS
