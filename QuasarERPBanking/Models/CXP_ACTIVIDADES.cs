using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Resources.CXP_ACTIVIDADES;
using System.Collections;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;



namespace QuasarERPBanking.Models
{
    public class CXP_ACTIVIDADES : clsMain
    {
        [Display(Name = "lblAutor", ResourceType = typeof(StringResources))]
        //[StringLength(30, MinimumLength = 0)]
        public string ACT_AUTOR { get; set; }

        [Display(Name = "lblFechaCre", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFECHCREA")]
        public DateTime ACT_FECHCREA { get; set; }
        public string strACTFECHCREA
        {
            get
            {
                return ACT_FECHCREA.ToString("dd/MM/yyyy");
            }
        }

        //[Display(Name = "lblACT_CXPCOD", ResourceType = typeof(StringResources))]
        //[StringLength(10, MinimumLength = 0)]
        public string ACT_CXPCOD { get; set; }

        [Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
       // [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFECHREAL")]
        public DateTime ACT_FECHREAL { get; set; }
        public string strACTFECHREAL
        {
            get
            {
                return ACT_FECHREAL.ToString("dd/MM/yyyy");
            }
        }
        [Display(Name = "lblAsunto", ResourceType = typeof(StringResources))]
        //[StringLength(100, MinimumLength = 0)]
        public string ACT_ASUNTO { get; set; }

       [Display(Name = "lblCuerpo", ResourceType = typeof(StringResources))]
        //[StringLength(200, MinimumLength = 0)]
        public string ACT_CUERPO { get; set; }

        //[Display(Name = "lblACT_PERSOUR", ResourceType = typeof(StringResources))]
        //[StringLength(50, MinimumLength = 0)]
        public string ACT_PERSOUR { get; set; }

        [Display(Name = "lblAsistentes", ResourceType = typeof(StringResources))]
        //[StringLength(100, MinimumLength = 0)]
        public string ACT_PERSCOMP { get; set; }

        //[Display(Name = "lblACT_DESC", ResourceType = typeof(StringResources))]
        //[StringLength(30, MinimumLength = 0)]
        public string ACT_DESC { get; set; }

        //[Display(Name = "lblACT_CORSRE", ResourceType = typeof(StringResources))]
        //[StringLength(30, MinimumLength = 0)]
        public string ACT_CORSRES { get; set; }

        //[Display(Name = "lblACT_CORATN", ResourceType = typeof(StringResources))]
        //[StringLength(30, MinimumLength = 0)]
        public string ACT_CORATN { get; set; }

        //[Display(Name = "lblACT_CORTPO", ResourceType = typeof(StringResources))]
        //[StringLength(20, MinimumLength = 0)]
        public string ACT_CORTPO { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        //[StringLength(10, MinimumLength = 0)]
        public string ACTID { get; set; }

        //[Display(Name = "lblACT_AD", ResourceType = typeof(StringResources))]
        //[StringLength(70, MinimumLength = 0)]
        public string ACT_ADJ { get; set; }

        private static string sp_FindCXPCOD { get; set; }
        public CXP_ACTIVIDADES()
        {
            sp_Insert = "SP_INS_CXPACTIVIDADES";
            sp_Update = "SP_UPD_CXPACTIVIDAD";
            sp_Delete = "SP_DEL_ACTIVIDADES";
            sp_Find = "SP_Q_CXP_ACTIVIDADES";
            sp_FindCXPCOD = "SP_Q_ACTCXPCOD";
            Campo_Clave = "ACTID";
            sp_Exist = "";
        }

        protected override ArrayList getParameters()
        {
            //ACT_AUTOR = Util.ContactoActual();


            ArrayList parametros = new ArrayList(14);

            parametros.Add(ACT_AUTOR);
            parametros.Add(ACT_FECHCREA.ToString("yyyy-MM-dd"));  //.Add(strACTFECHCREA);
            parametros.Add(ACT_CXPCOD);
            parametros.Add(ACT_FECHREAL.ToString("yyyy-MM-dd")); // (strACTFECHREAL);
            parametros.Add(ACT_ASUNTO);
            parametros.Add(ACT_CUERPO);
            parametros.Add(ACT_PERSOUR);
            parametros.Add(ACT_PERSCOMP);
            parametros.Add(ACT_DESC);
            parametros.Add(ACT_CORSRES);
            parametros.Add(ACT_CORATN);
            parametros.Add(ACT_CORTPO);
            parametros.Add(ACTID);
            parametros.Add(ACT_ADJ);


            return parametros;
        }
        /// <summary>      ACT_ADJ 	
        /// /////////////////////////////////////////////////////
        /// 
        /// CXPCONTNOM
        /// CXPCONTTEL
        /// CXPCONTFAX
        /// CXPCONTDIR          
        /// CXPCONTEMAIL
        /// CXPCONTEXT
        /// CXPCONTCOD
        /// CXPCOD
        /// CXPCONTSEL


        //para buscar el contacto al editar
        protected override clsMain LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {

                ACT_AUTOR = item["ACT_AUTOR"].ToString();
                ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date;
                ACT_CXPCOD = item["ACT_CXPCOD"].ToString();
                ACT_FECHREAL = DateTime.Parse(item["ACT_FECHREAL"].ToString()).Date;
                ACT_ASUNTO = item["ACT_ASUNTO"].ToString();
                ACT_CUERPO = item["ACT_CUERPO"].ToString();
                ACT_PERSOUR = item["ACT_PERSOUR"].ToString();
                ACT_PERSCOMP = item["ACT_PERSCOMP"].ToString();
                ACT_DESC = item["ACT_DESC"].ToString();
                ACT_CORSRES = item["ACT_CORSRES"].ToString();
                ACT_CORATN = item["ACT_CORATN"].ToString();
                ACT_CORTPO = item["ACT_CORTPO"].ToString();
                ACTID = item["ACTID"].ToString();
                ACT_ADJ = item["ACT_ADJ"].ToString();
            }
            return this;
        }

        protected static List<CXP_ACTIVIDADES> LoadListObject(Rows reader)
        {
            List<CXP_ACTIVIDADES> lista = new List<CXP_ACTIVIDADES>();
            foreach (Row item in reader)
            {

                CXP_ACTIVIDADES cxp_actividad = new CXP_ACTIVIDADES
                {
                    ACT_AUTOR = item["ACT_AUTOR"].ToString(),
                    ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date,
                    ACT_CXPCOD = item["ACT_CXPCOD"].ToString(),
                    ACT_FECHREAL = DateTime.Parse(item["ACT_FECHREAL"].ToString()).Date,
                    ACT_ASUNTO = item["ACT_ASUNTO"].ToString(),
                    ACT_CUERPO = item["ACT_CUERPO"].ToString(),
                    ACT_PERSOUR = item["ACT_PERSOUR"].ToString(),
                    ACT_PERSCOMP = item["ACT_PERSCOMP"].ToString(),
                    ACT_DESC = item["ACT_DESC"].ToString(),
                    ACT_CORSRES = item["ACT_CORSRES"].ToString(),
                    ACT_CORATN = item["ACT_CORATN"].ToString(),
                    ACT_CORTPO = item["ACT_CORTPO"].ToString(),
                    ACTID = item["ACTID"].ToString(),
                    ACT_ADJ = item["ACT_ADJ"].ToString(),
                };
                lista.Add(cxp_actividad);
            }
            return lista;
        }


        #region SIN PROCEDURE
        protected static List<CXP_ACTIVIDADES> LoadListObject(DataTable dt)
        {
            List<CXP_ACTIVIDADES> lista = new List<CXP_ACTIVIDADES>();
            foreach (DataRow item in dt.Rows)
            {

                CXP_ACTIVIDADES cxp_actividad = new CXP_ACTIVIDADES
                {
                    ACT_AUTOR = item["ACT_AUTOR"].ToString(),
                    ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date,
                    ACT_CXPCOD = item["ACT_CXPCOD"].ToString(),
                    ACT_FECHREAL = DateTime.Parse(item["ACT_FECHREAL"].ToString()).Date,
                    ACT_ASUNTO = item["ACT_ASUNTO"].ToString(),
                    ACT_CUERPO = item["ACT_CUERPO"].ToString(),
                    ACT_PERSOUR = item["ACT_PERSOUR"].ToString(),
                    ACT_PERSCOMP = item["ACT_PERSCOMP"].ToString(),
                    ACT_DESC = item["ACT_DESC"].ToString(),
                    ACT_CORSRES = item["ACT_CORSRES"].ToString(),
                    ACT_CORATN = item["ACT_CORATN"].ToString(),
                    ACT_CORTPO = item["ACT_CORTPO"].ToString(),
                    ACTID = item["ACTID"].ToString(),
                    ACT_ADJ = item["ACT_ADJ"].ToString(),
                };
                lista.Add(cxp_actividad);
            }
            return lista;
        }

        #endregion
        public static List<CXP_ACTIVIDADES> ActividadesPorCXPCOD(string CXPCOD)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_ACTIVIDADES> lista = new List<CXP_ACTIVIDADES>();
            string consulta = "SELECT ACT_AUTOR ,ACT_FECHCREA , ACT_CXPCOD , ACT_FECHREAL , ACT_ASUNTO , ACT_CUERPO , ACT_PERSOUR , ACT_PERSCOMP , ACT_DESC ,	 ACT_CORSRES , ACT_CORATN ,	 ACT_CORTPO , ACTID , ACT_ADJ FROM BICADM01W.CXP_ACTIVIDADES WHERE ACT_CXPCOD = '" + CXPCOD + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {

                    lista = LoadListObject(dt);
                }
            }
            return lista;
        }
    }

}




//////////////// SP UTILIZADOS ////////////////
//SP_INS_CXPACTIVIDADES
//SP_UPD_CXPACTIVIDAD
//SP_DEL_ACTIVIDADES
//SP_Q_CXP_ACTIVIDADES
//SP_Q_ACTCXPCOD