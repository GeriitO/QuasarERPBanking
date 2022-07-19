using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Resources.CentrodeCostoInterno;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CentrodeCostoInterno : ConectDB 
    {
        [Display(Name = "lblCond", Prompt = "placEdoCXP", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCond")]
        public string CTECOND { get; set; }

        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        public string CTENOMBRE { get; set; }

        [Display(Name = "lblDirec", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirec")]
        public string CTEDIR { get; set; }

        //[Display(Name = "lblWeb", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errWeb")]
        public string CTEWEB { get; set; }

        //[Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFecha")]
        public DateTime CTERELACDESDE { get; set; }
        public string strCTERELACDESDE
        {
            get
            {
                return CTERELACDESDE.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }
        //[Display(Name = "lblIva", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVA")]
        //[Range(0.00, 100, ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVArango")]
        public double CTERETENC { get; set; }

        [Display(Name = "lblComp", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errComp")]
        public string CTECOMPRADOR { get; set; }

        [Display(Name = "lblEdo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errEdo")]
        public string CTEEDO { get; set; }

        [Display(Name = "lblCiu", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCiu")]
        public string CTECIUDAD { get; set; }

        [Display(Name = "lblCoPos", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCoPos")]
        public string CTECODPOSTAL { get; set; }

        [Display(Name = "lblFax", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFax")]
        public string CTEFAX { get; set; }

        [Display(Name = "lblTlf", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTlf")]
        public string CTETELF { get; set; }


        public int CTEDIASCRED { get; set; }


        public double CTEBALACT { get; set; }

        [Display(Name = "lblResena", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errResena")]
        public string CTERESENIA { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCod")]
        public string CTECOD { get; set; }

        static string sp_get = "SP_Q_CC_CINTERNOS";   //AUTOCOMPLETE
        static string sp_getAll = "SP_ALL_CC_CTESINT";  //DROPDOWN

         
     

        protected CentrodeCostoInterno ReadObject(DataSet Dtset)
        {

            foreach  (DataRow item in Dtset.Tables[0].Rows)
            {
                CTECOND = item["CTECOND"].ToString();
                CTENOMBRE = item["CTENOMBRE"].ToString();
                CTEDIR = item["CTEDIR"].ToString();
                CTEWEB = item["CTEWEB"].ToString();
                CTERELACDESDE = DateTime.Parse(item["CTERELACDESDE"].ToString() == "" ? strCTERELACDESDE : item["CTERELACDESDE"].ToString()); //, new CultureInfo ("es-VE", false));                  
                //CTERELACDESDE = CTERELACDESDE.Date;
                CTERETENC = double.Parse(item["CTERETENC"].ToString() == "" ? "0" : item["CTERETENC"].ToString());
                CTECOMPRADOR = item["CTECOMPRADOR"].ToString();
                CTEEDO = item["CTEEDO"].ToString();
                CTECIUDAD = item["CTECIUDAD"].ToString();
                CTECODPOSTAL = item["CTECODPOSTAL"].ToString();
                CTEFAX = item["CTEFAX"].ToString();
                CTETELF = item["CTETELF"].ToString();
                CTEDIASCRED = int.Parse(item["CTEDIASCRED"].ToString() == "" ? "0" : item["CTEDIASCRED"].ToString());
                CTEBALACT = double.Parse(item["CTEBALACT"].ToString());
                CTERESENIA = item["CTERESENIA"].ToString();
                CTECOD = item["CTECOD"].ToString();
            }

            return this;
        }

        //SE UTILIZA DROPDOWMLIST 

        public static IEnumerable<CentrodeCostoInterno> GetDropdownlist()
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT CTENOMBRE , CTECOD  FROM " + ParametrosGlobales.bd +  "  CC_CTESINT";
            List<CentrodeCostoInterno> clientes = new List<CentrodeCostoInterno>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentrodeCostoInterno cliente = new Models.CentrodeCostoInterno();
                    cliente.CTECOD = item["CTECOD"].ToString();
                    cliente.CTENOMBRE = item["CTENOMBRE"].ToString();

                    clientes.Add(cliente);
                }
            }
            return clientes;
        }

        public CentrodeCostoInterno Buscarinfo(string parametro)
        {
            CentrodeCostoInterno Objeto = new CentrodeCostoInterno();
            DataSet DRCtesint = ConectDB.Datos_Retorno("SELECT CTECOND, CTENOMBRE, CTEDIR, CTEWEB, CTERELACDESDE, CTERETENC, CTECOMPRADOR, CTEEDO, CTECIUDAD, CTECODPOSTAL, CTEFAX, CTETELF, CTEDIASCRED, CTEBALACT, CTERESENIA, CTECOD FROM " + ParametrosGlobales.bd + "CC_CTESINT WHERE CTECOD = '" + parametro.Trim() + "'");
            Objeto=this.ReadObject(DRCtesint);
            return Objeto;
        }

        // PARA EL AUTOCOMPLETE
        #region Autocomplete
        public List<string> GetAutocClientes(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT CONCAT ( CONCAT ( CTECOD , ' - ' ) , CTENOMBRE ) AS NOMBRE FROM " + ParametrosGlobales.bd + "CC_CTESINT WHERE(UPPER(CTENOMBRE) LIKE CONCAT(CONCAT('%', UPPER('"+ codigo + "')), '%')) OR(UPPER(CTECOD) LIKE CONCAT(CONCAT('%', UPPER('"+ codigo + "')), '%')) ";
            List<string> clientes = new List<string>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    string cliente = "";
                    cliente = item["NOMBRE"].ToString();  // ESTE ES EL ALIAS DEL SP                 
                    clientes.Add(cliente);
                }
            }

            return clientes;
        }
        #endregion






    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_Q_CC_CINTERNOS
//SP_ALL_CC_CTESINT
//SP_DEL_CC_CTESINT
//SP_INS_CC_CTESINT
//SP_UPD_CC_CTESINT
//SP_Q_CC_CTESINT