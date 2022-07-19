using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources.CC_ACTIVIDADES;
//using Resources.CXP_ACTIVIDADES;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CC_ACTIVIDADES : ConectDB
    {
        //[HiddenInput(DisplayValue = false)]
        [Display(Name = "lblAutor", ResourceType = typeof(StringResources))]
        public string ACT_AUTOR { get; set; }

        [Display(Name = "lblDateCre", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDateCre")]
        public DateTime ACT_FECHCREA { get; set; }
        public string strACTFECHCREA
        {
            get
            {
                return ACT_FECHCREA.ToString("dd/MM/yyyy");
            }
        }


        public string ACT_PROVCOD { get; set; }
        [Display(Name = "lblDate", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDate")]
        public DateTime ACT_FECHREAL { get; set; }
        public string strACTFECHREAL
        {
            get
            {
                return ACT_FECHREAL.ToString("dd/MM/yyyy");
            }
        }

        [Display(Name = "lblAsunto", ResourceType = typeof(StringResources))]
        public string ACT_ASUNTO { get; set; }

        [Display(Name = "lblCuerpo", ResourceType = typeof(StringResources))]
        public string ACT_CUERPO { get; set; }
        public string ACT_PERSOUR { get; set; }

        [Display(Name = "lblAsistentes", ResourceType = typeof(StringResources))]
        public string ACT_PERSCOMP { get; set; }
        public string ACT_DESC { get; set; }
        public string ACT_CORSRES { get; set; }
        public string ACT_CORATN { get; set; }
        public string ACT_CORTPO { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        public string ACTID { get; set; }
        public int ACT_CANT { get; set; }
        public string ACT_ADJ { get; set; }




        private static string sp_FindCTECOD { get; set; }
        //public CC_ACTIVIDADES()
        //{
        //    sp_Insert = "SP_INS_CCACTIVIDADES";
        //    sp_Update = "SP_UPD_CCACTIVIDAD";
        //    sp_Delete = "SP_DEL_CCACTIVD";
        //    sp_Find = "SP_Q_CC_ACTIVIDADES";
        //    sp_FindCTECOD = "SP_Q_ACTCTECOD";
        //    Campo_Clave = "ACTID";
        //    sp_Exist = "";
        //}

        //protected override ArrayList getParameters()
        //{
        //    ArrayList parametros = new ArrayList(15);

        //    parametros.Add(ACT_AUTOR);
        //    parametros.Add(ACT_FECHCREA.ToString("yyyy-MM-dd"));  //.Add(strACTFECHCREA);      
        //    parametros.Add(ACT_PROVCOD);
        //    parametros.Add(ACT_FECHREAL.ToString("yyyy-MM-dd")); // (strACTFECHREAL);          
        //    parametros.Add(ACT_ASUNTO);
        //    parametros.Add(ACT_CUERPO);
        //    parametros.Add(ACT_PERSOUR);
        //    parametros.Add(ACT_PERSCOMP);
        //    parametros.Add(ACT_DESC);
        //    parametros.Add(ACT_CORSRES);
        //    parametros.Add(ACT_CORATN);
        //    parametros.Add(ACT_CORTPO);
        //    parametros.Add(ACTID);
        //    parametros.Add(ACT_CANT);
        //    parametros.Add(ACT_ADJ);

        //    return parametros;
        //}



        //para buscar el contacto al editar
        //protected override clsMain LoadObject(Rows reader)
        //{

        //    foreach (Row item in reader)
        //    {

        //        ACT_AUTOR = item["ACT_AUTOR"].ToString();
        //        ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date;
        //        ACT_PROVCOD = item["ACT_PROVCOD"].ToString();
        //        ACT_FECHREAL = DateTime.Parse(item["ACT_FECHREAL"].ToString()).Date;
        //        ACT_ASUNTO = item["ACT_ASUNTO"].ToString();
        //        ACT_CUERPO = item["ACT_CUERPO"].ToString();
        //        ACT_PERSOUR = item["ACT_PERSOUR"].ToString();
        //        ACT_PERSCOMP = item["ACT_PERSCOMP"].ToString();
        //        ACT_DESC = item["ACT_DESC"].ToString();
        //        ACT_CORSRES = item["ACT_CORSRES"].ToString();
        //        ACT_CORATN = item["ACT_CORATN"].ToString();
        //        ACT_CORTPO = item["ACT_CORTPO"].ToString();
        //        ACTID = item["ACTID"].ToString();
        //        ACT_CANT = int.Parse(item["ACT_CANT"].ToString());
        //        ACT_ADJ = item["ACT_ADJ"].ToString();
        //    }
        //    return this;
        //}

        protected CC_ACTIVIDADES  LoadObject(DataSet reader)
        {

            foreach (DataRow item in reader.Tables[0].Rows )
            {

                ACT_AUTOR = item["ACT_AUTOR"].ToString();
                ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date;
                ACT_PROVCOD = item["ACT_PROVCOD"].ToString();
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
                ACT_CANT = int.Parse(item["ACT_CANT"].ToString());
                ACT_ADJ = item["ACT_ADJ"].ToString();
            }
            return this;
        }

        protected static List<CC_ACTIVIDADES> LoadListObject(DataTable dt)
        {
            List<CC_ACTIVIDADES> lista = new List<CC_ACTIVIDADES>();
            foreach (DataRow item in dt.Rows)
            {



                CC_ACTIVIDADES cc_actividad = new CC_ACTIVIDADES
                {


                    ACT_AUTOR = item["ACT_AUTOR"].ToString(),
                    ACT_FECHCREA = DateTime.Parse(item["ACT_FECHCREA"].ToString()).Date,
                    ACT_PROVCOD = item["ACT_PROVCOD"].ToString(),
                    ACT_FECHREAL = DateTime.Parse(item["ACT_FECHREAL"].ToString()).Date,
                    ACT_ASUNTO = item["ACT_ASUNTO"].ToString(),
                    ACT_CUERPO = item["ACT_CUERPO"].ToString(),
                    ACT_PERSOUR = item["ACT_PERSOUR"].ToString(),
                    ACT_PERSCOMP = item["ACT_PERSCOMP"].ToString(),
                    ACT_DESC = item["ACT_DESC"].ToString(),
                    ACT_CORSRES = item["ACT_CORSRES"].ToString(),
                    //ACT_CORATN = item["ACT_CORATN"].ToString(),
                    ACT_CORATN = ConvString(item["ACT_CORATN"].ToString()),
                    //ACT_CORTPO = item["ACT_CORTPO"].ToString(),
                    ACT_CORTPO = ConvString(item["ACT_CORTPO"].ToString()),
                    ACTID = item["ACTID"].ToString(),
                    //ACT_CANT = int.Parse(item["ACT_CANT"].ToString()),  
                    ACT_CANT = ConvInt(item["ACT_CANT"].ToString()),
                    //ACT_ADJ = item["ACT_ADJ"].ToString(),
                    ACT_ADJ = ConvString(item["ACT_ADJ"].ToString()),


                };
                lista.Add(cc_actividad);
            }
            return lista;
        }

        //CONVERTIR INT QUE VENGAN VACIOS
        static int ConvInt(string item)
        {
            int aux = 0;
            int.TryParse(item, out aux);
            return aux;
        }

        //CONVERTIR DATETIME QUE VENGAN VACIOS
        DateTime ConvDate(string item)
        {
            DateTime aux = ACT_FECHCREA;
            DateTime.TryParse(item, out aux);

            return aux;
        }

        static string ConvString(string item)
        {
            if (string.IsNullOrEmpty(item))
                return "-";
            else
                return string.Format(item);
        }



        public static List<CC_ACTIVIDADES> ActividadesPorCTECOD(string CTECOD)
        {


            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CC_ACTIVIDADES> listaActiv = new List<CC_ACTIVIDADES>();
            string consulta = "SELECT ACT_AUTOR , ACT_FECHCREA ,  ACT_PROVCOD ,  ACT_FECHREAL , ACT_ASUNTO , ACT_CUERPO ,  ACT_PERSOUR , ACT_PERSCOMP ,  ACT_DESC , ACT_CORSRES , ACT_CORATN , ACT_CORTPO , ACTID ,  ACT_CANT ,    ACT_ADJ  FROM " + ParametrosGlobales.bd + "CC_CTE_ACTIVIDADES WHERE ACT_PROVCOD = '" + CTECOD + "' ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {

                    listaActiv = LoadListObject(dt);
                }
            }
            return listaActiv;
        }
    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_INS_CCACTIVIDADES
//SP_UPD_CCACTIVIDAD
//SP_DEL_CCACTIVD
//SP_Q_CC_ACTIVIDADES
//SP_Q_ACTCTECOD