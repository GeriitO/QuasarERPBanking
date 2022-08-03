using Resources.CXP_PARAMETROS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;


namespace QuasarERPBanking.Models
{
    public class CXP_PARAMETROS : ConectDB
    {
        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        public string CPPPARNOMB { get; set; }

        [Display(Name = "lblId", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errId")]
        public string CPPPARACTID { get; set; }

        [Display(Name = "lblLote", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errLote")]
        public string CPPPARLOTE { get; set; }

        [Display(Name = "lblTrId", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTrId")]
        public string CPPPARTRANSID { get; set; }

        [Display(Name = "lblCC", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCC")]
        public bool CPPPARINTCC { get; set; }

        [Display(Name = "lblDirCC", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirCC")]
        public string CPPPARDIRCC { get; set; }

        [Display(Name = "lblMG", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errMG")]
        public bool CPPPARINTMG { get; set; }

        [Display(Name = "lblDirMG", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirMG")]
        public string CPPPARDIRMG { get; set; }

        [Display(Name = "lblImp", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errImp")]
        public bool CPPPARINTIMP { get; set; }

        [Display(Name = "lblDirImp", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirImp")]
        public string CPPPARDIRIMP { get; set; }

        //[Display(Name = "lblComp", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errComp")]
        public string CPPPARCOMP { get; set; }

        //[Display(Name = "lblNId", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNId")]
        public string CPPPARNROID { get; set; }

        //[Display(Name = "lblComOp", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errComOp")]
        public string CPPPARCOMPOP { get; set; }

        //[Display(Name = "lblNOp", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNOp")]
        public string CPPPARNROOP { get; set; }

        [Display(Name = "lblUnid", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errUnid")]
        public float CPPUNIDTRIB { get; set; }

        public string CPPSWICHE { get; set; }

        public string CPPSWICHE2 { get; set; }


        //[Display(Name = "lblIva", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIva")]
        public int CPPPARRETIVA { get; set; }


        //[Display(Name = "lblCta", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCta")]
        public string CPPPARCTASG { get; set; }


        //[Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDesc")]
        public string CPPPARDESC { get; set; }

        public string CPPPARPROC1 { get; set; }

        public string CPPPARPROC2 { get; set; }


        //[Display(Name = "lblCtaCh", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCtaCh")]
        public string CPPPARCTACH { get; set; }

        public string CPPPARCTAEF { get; set; }

        public string CPPPARCTAAN { get; set; }


        //[Display(Name = "lblUser", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errUser")]
        public string CPPPUSERPROC1 { get; set; }

        public string CPPPUSERPROC2 { get; set; }


        //[Display(Name = "lblRsMin", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRsMin")]
        public float CPPRSUTMIN { get; set; }


        //[Display(Name = "lblRsCta", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRsCta")]
        public string CPPRSCTA { get; set; }


        public string MAYORGENR { get; set; }
        public string CENTROCOST { get; set; }
        public string IMPUESTO { get; set; }

        public bool Existe()
        {
            return true;
        }


        //busca en la bd 
        public CXP_PARAMETROS Buscar()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT * FROM BICADM01W . CXP_PARAMETROS";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CPPPARNOMB = item["CPPPARNOMB"].ToString();
                    CPPPARACTID = item["CPPPARACTID"].ToString();
                    CPPPARLOTE = item["CPPPARLOTE"].ToString();
                    CPPPARTRANSID = item["CPPPARTRANSID"].ToString();
                    CPPPARINTCC = (item["CPPPARINTCC"].ToString() == "0" ? false : true);
                    //CPPPARINTCC = (((item["CPPPARINTCC"] == "0" ? false : true) == true ? "SI" : "NO").ToString())
                    CPPPARDIRCC = item["CPPPARDIRCC"].ToString();
                    CPPPARINTMG = (item["CPPPARINTMG"].ToString() == "0" ? false : true);
                    CPPPARDIRMG = item["CPPPARDIRMG"].ToString();
                    CPPPARINTIMP = (item["CPPPARINTIMP"].ToString() == "0" ? false : true);
                    CPPPARDIRIMP = item["CPPPARDIRIMP"].ToString();
                    CPPPARCOMP = item["CPPPARCOMP"].ToString();
                    CPPPARNROID = item["CPPPARNROID"].ToString();
                    CPPPARCOMPOP = item["CPPPARCOMPOP"].ToString();
                    CPPPARNROOP = item["CPPPARNROOP"].ToString();
                    CPPUNIDTRIB = float.Parse(item["CPPUNIDTRIB"].ToString());
                    CPPSWICHE = item["CPPSWICHE"].ToString();
                    CPPSWICHE2 = item["CPPSWICHE2"].ToString();
                    CPPPARRETIVA = int.Parse(item["CPPPARRETIVA"].ToString());
                    CPPPARCTASG = item["CPPPARCTASG"].ToString();
                    CPPPARDESC = item["CPPPARDESC"].ToString();
                    CPPPARPROC1 = item["CPPPARPROC1"].ToString();
                    CPPPARPROC2 = item["CPPPARPROC2"].ToString();
                    CPPPARCTACH = item["CPPPARCTACH"].ToString();
                    CPPPARCTAEF = item["CPPPARCTAEF"].ToString();
                    CPPPARCTAAN = item["CPPPARCTAAN"].ToString();
                    CPPPUSERPROC1 = item["CPPPUSERPROC1"].ToString();
                    CPPPUSERPROC2 = item["CPPPUSERPROC2"].ToString();
                    CPPRSUTMIN = float.Parse(item["CPPRSUTMIN"].ToString());
                    CPPRSCTA = item["CPPRSCTA"].ToString();


                    //CTEPARNOMB = item["CTEPARNOMB"].ToString();
                    //CTEPARACTID = item["CTEPARACTID"].ToString();
                    //CC_IDTRANS = item["CC_IDTRANS"].ToString();
                    //CCPARNBCOMP = item["CCPARNBCOMP"].ToString();
                    //CCPARINTMG = (item["CCPARINTMG"].ToString() == "0" ? false : true);
                    //CCPARDIRMG = (item["CCPARDIRMG"].ToString() == null ? "-" : item["CCPARDIRMG"].ToString());
                    //CCPARRIFCOMP = item["CCPARRIFCOMP"].ToString();
                    //CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "true 0" : false item["CODEMP"].ToString()),
                    //FECHACREACION = (item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(item["FECHACREACION"].ToString())),

                }

            }
            return this;
        }


        public int Update()
        {
            int resultado = 0;
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(11);
            parametros.Add(CPPPARNOMB);
            parametros.Add(CPPPARACTID);
            parametros.Add(CPPPARLOTE);
            parametros.Add(CPPPARTRANSID);
            parametros.Add(CPPPARINTCC ? "1" : "0");
            parametros.Add(CPPPARDIRCC);
            parametros.Add(CPPPARINTMG ? "1" : "0");
            parametros.Add(CPPPARDIRMG);
            parametros.Add(CPPPARINTIMP ? "1" : "0");
            parametros.Add(CPPPARDIRIMP);
            parametros.Add(CPPUNIDTRIB);








            //parametros.Add(CTEPARNOMB);
            //parametros.Add(CTEPARACTID);
            //parametros.Add(CC_IDTRANS);
            //parametros.Add(CCPARNBCOMP);
            //parametros.Add(CCPARINTMG ? "1" : "0");
            //parametros.Add(CCPARDIRMG);
            //parametros.Add(CCPARRIFCOMP);


            cn.execProc("SP_UPD_CXP_PARAMETROS", parametros, true);

            //return cn.afectados;                                             
            return resultado;
        }


        public static List<CXP_PARAMETROS> llenarGrid()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_PARAMETROS> lista = new List<CXP_PARAMETROS>();
            string consulta = "SELECT CPPPARNOMB , CPPPARLOTE , CPPPARTRANSID , CPPPARACTID , CPPPARINTMG , CPPPARDIRMG , CPPPARINTCC , CPPPARDIRCC , CPPPARINTIMP , CPPPARDIRIMP , CPPUNIDTRIB FROM BICADM01W.CXP_PARAMETROS ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        CXP_PARAMETROS cxp_Grid = new CXP_PARAMETROS
                        {

                            CPPPARNOMB = item["CPPPARNOMB"].ToString(),
                            CPPPARACTID = item["CPPPARACTID"].ToString(),
                            CPPPARTRANSID = item["CPPPARTRANSID"].ToString(),
                            CPPPARLOTE = item["CPPPARLOTE"].ToString(),
                            MAYORGENR = item["CPPPARINTMG"].ToString() == "0" ? "NO" : "SI",
                            //CPPPARINTMG=  (item["CPPPARINTMG"].ToString() == "0" ? bool.ToString("NO") : "si"),
                            CPPPARDIRIMP = item["CPPPARDIRIMP"].ToString(),
                            CENTROCOST = item["CPPPARINTCC"].ToString() == "0" ? "NO" : "SI",
                            CPPPARDIRCC = item["CPPPARDIRCC"].ToString(),
                            IMPUESTO = item["CPPPARINTIMP"].ToString() == "0" ? "NO" : "SI",
                            CPPPARDIRMG = item["CPPPARDIRMG"].ToString(),
                            CPPUNIDTRIB = float.Parse(item["CPPUNIDTRIB"].ToString()),
                        };

                        lista.Add(cxp_Grid);
                    }
                }
            }
            return lista;
        }

    }
}