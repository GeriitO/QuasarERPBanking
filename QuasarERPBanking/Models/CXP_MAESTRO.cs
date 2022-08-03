using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Globalization;
using Resources.CXP_MAESTRO;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Web.Mvc;
using System.Data.OleDb;
using System.Data;


namespace QuasarERPBanking.Models
{
    public class CXP_MAESTRO : clsMain
    {
        [Display(Name = "lblCondicCxpMaestro", Prompt = "placEdoCXP", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCondCxpMaestro")]
        public string CXPCOND { get; set; }

        [Display(Name = "lblNombreCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNomCxpMaestro")]
        public string CXPNOMBRE { get; set; }

        [Display(Name = "lblDirecciónCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDirCxpMaestro")]
        public string CXPDIR { get; set; }

        [Display(Name = "lblWebCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errWebCxpMaestro")]
        public string CXPWEB { get; set; }

        [Display(Name = "lblFechaCxpMaestro", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFechCxpMaestro")]
        public DateTime CXPRELACDESDE { get; set; }
        public string strCXPRELACDESDE
        {
            get
            {
                return CXPRELACDESDE.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }
        //formatos varios de condiciones:
        //[Format(short, "El valor debe ser numérico")] 
        //[Range(0, short.MaxValue, ErrorMessage = "El valor {0} debe ser numérico.")]
        //[RegularExpression(@"^\$?\d+(\.(\d{2}))?$g", ErrorMessage = "El campo debe contener sólo números.")]
        [Display(Name = "lblIvaCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVACxpMaestro")]
        //[Range(0.00, 100, ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVAranCxpMaestro")]
        public decimal CXPRETENC { get; set; }
        public string CXPCOMPRADOR { get; set; }

        // EL NIT PARA EN-US ES Taxpayer Identification Number "TIN"  O Tax ID number
        [Display(Name = "lblSncCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errSNCCxpMaestro")]
        public string CXPNIT { get; set; }

        // SSN (Social Security Number o Número de Seguro Social)
        [Display(Name = "lblRifCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRIFCxpMaestro")]
        public string CXPRIF { get; set; }

        [Display(Name = "lblEdoCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errEdoCxpMaestro")]
        public string CXPEDO { get; set; }

        [Display(Name = "lblCiuCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCiuCxpMaestro")]
        public string CXPCIUDAD { get; set; }

        [Display(Name = "lblCoPosCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCoPosCxpMaestro")]
        public string CXPCODPOSTAL { get; set; }

        [Display(Name = "lblRamoCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRamoCxpMaestro")]
        public string CXPRAMO { get; set; }

        [Display(Name = "lblFaxCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFaxCxpMaestro")]
        public string CXPFAX { get; set; }

        [Display(Name = "lblTlfCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTlfCxpMaestro")]
        public string CXPTELF { get; set; }

        [Display(Name = "lblPlazoCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errPlazoCxpMaestro")]
        public int CXPDIASCRED { get; set; }
        public string CXPBALACT { get; set; }

        [Display(Name = "lblReseñaCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errReseñaCxpMaestro")]
        public string CXPRESENIA { get; set; }


        [Display(Name = "lblCodCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCodCxpMaestro")]
        public string CXPCOD { get; set; }

        [Display(Name = "lblRelacionCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRelacCxpMaestro")]
        public string CXPRELAC { get; set; }

        [Display(Name = "lblCuentaCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCtaMaCxpMaestro")]
        public string CXPCTAMAY { get; set; }

        [Display(Name = "lblFoPagCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFoPagCxpMaestro")]
        public string CXPFRMP { get; set; }


        [Display(Name = "lblNCtaCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNCtaCxpMaestro")]
        public string CXPNCTA { get; set; }

        public int CXPSEL { get; set; }

        [Display(Name = "lblTperCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTperCxpMaestro")]
        public int CXPTPOPERS { get; set; }
        public string CXPGRUPO { get; set; }
        [Display(Name = "lblITFCxpMaestro", ResourceType = typeof(StringResources))]
        public bool CXPITF { get; set; }
        public string CXPCATEGORIA { get; set; }
        [Display(Name = "lblTimbreCxpMaestro", ResourceType = typeof(StringResources))]
        public bool CXPTIMBRE { get; set; }
        [Display(Name = "lblIMPMUNCxpMaestro", ResourceType = typeof(StringResources))]
        public bool CXPIMPMUN { get; set; }
        public string USUARIO { get; set; }
        public DateTime FECHA { get; set; }

        public List<CXP_MAESTRO> cot_maestro { get; set; }




        static string sp_getAll = "SP_ALL_CXP_MAESTRO";
        //static string sp_get = "SP_Q_CXP_MAESTRO"; 

        //static string sp_existe = "SP_E_CXP_MAESTRO";  OJO PARA ELIMINAR DE LA BASE DE DATOS

        #region Constructor
        public CXP_MAESTRO()
        {
            //sp_Insert = "SP_INS_CXP_MAESTRO";
            //sp_Update = "SP_UPD_CXP_MAESTRO";
            //sp_Delete = "SP_DEL_CXP_MAESTRO";
            //sp_Find = "SP_Q_CXP_PROVEEDOR2";   OJO PARA ELIMINAR DE LA BASE DE DATOS
            Campo_Clave = "CXPCOD";
        }


        #endregion

        protected override ArrayList getParameters()
        {
            CXPCOMPRADOR = "";
            CXPBALACT = "";
            CXPGRUPO = "";
            CXPCATEGORIA = "";
            CXPSEL = 0;
            USUARIO = "";

            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(32);

            parametros.Add(CXPCOND);
            parametros.Add(CXPNOMBRE);
            parametros.Add(CXPDIR);
            parametros.Add(CXPWEB);
            parametros.Add(CXPRELACDESDE.ToString("yyyy-MM-dd"));
            parametros.Add(CXPRETENC);
            parametros.Add(CXPCOMPRADOR);
            parametros.Add(CXPNIT);
            parametros.Add(CXPRIF);
            parametros.Add(CXPEDO);
            parametros.Add(CXPCIUDAD);
            parametros.Add(CXPCODPOSTAL);
            parametros.Add(CXPRAMO);
            parametros.Add(CXPFAX);
            parametros.Add(CXPTELF);
            parametros.Add(CXPDIASCRED);
            parametros.Add(CXPBALACT);
            parametros.Add(CXPRESENIA);
            parametros.Add(CXPCOD);
            parametros.Add(CXPRELAC);
            parametros.Add(CXPCTAMAY);
            parametros.Add(CXPFRMP);
            parametros.Add(CXPNCTA);
            parametros.Add(CXPSEL);
            parametros.Add(CXPTPOPERS);
            parametros.Add(CXPGRUPO);
            parametros.Add(Convert.ToInt32(CXPITF));
            parametros.Add(CXPCATEGORIA);
            parametros.Add(CXPTIMBRE ? "1" : "0");
            parametros.Add(CXPIMPMUN ? "1" : "0");
            return parametros;
        }
        protected override clsMain LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {

                CXPCOND = item["CXPCOND"].ToString();
                CXPNOMBRE = item["CXPNOMBRE"].ToString();
                CXPDIR = item["CXPDIR"].ToString();
                CXPWEB = item["CXPWEB"].ToString();
                CXPRELACDESDE = DateTime.Parse(item["CXPRELACDESDE"].ToString()); //, new CultureInfo ("es-VE", false));                  
                CXPRELACDESDE = CXPRELACDESDE.Date;
                CXPRETENC = decimal.Parse(item["CXPRETENC"].ToString());
                CXPCOMPRADOR = item["CXPCOMPRADOR"].ToString();
                CXPNIT = item["CXPNIT"].ToString();
                CXPRIF = item["CXPRIF"].ToString();
                CXPEDO = item["CXPEDO"].ToString();
                CXPCIUDAD = item["CXPCIUDAD"].ToString();
                CXPCODPOSTAL = item["CXPCODPOSTAL"].ToString();
                CXPRAMO = item["CXPRAMO"].ToString();
                CXPFAX = item["CXPFAX"].ToString();
                CXPTELF = item["CXPTELF"].ToString();
                CXPDIASCRED = int.Parse(item["CXPDIASCRED"].ToString());
                CXPBALACT = item["CXPBALACT"].ToString();
                CXPRESENIA = item["CXPRESENIA"].ToString();
                CXPCOD = item["CXPCOD"].ToString();
                CXPRELAC = item["CXPRELAC"].ToString();
                CXPCTAMAY = item["CXPCTAMAY"].ToString();
                CXPFRMP = item["CXPFRMP"].ToString();
                CXPNCTA = item["CXPNCTA"].ToString();
                CXPSEL = int.Parse(item["CXPSEL"].ToString());
                CXPTPOPERS = int.Parse(item["CXPTPOPERS"].ToString());
                CXPGRUPO = item["CXPGRUPO"].ToString();
                CXPITF = (item["CXPITF"].ToString().Contains("1") ? true : false); //bool.Parse(item["CXPITF"].ToString());
                CXPCATEGORIA = item["CXPCATEGORIA"].ToString();
                CXPTIMBRE = (item["CXPTIMBRE"].ToString().Contains("1") ? true : false);
                CXPIMPMUN = (item["CXPIMPMUN"].ToString().Contains("1") ? true : false);
            }

            return this;
        }

        protected override clsMain LoadObject54(OleDbDataReader reader)
        {

            while (reader.Read())
            {


                CXPCOND = reader["CXPCOND"].ToString();
                CXPNOMBRE = reader["CXPNOMBRE"].ToString();
                CXPDIR = reader["CXPDIR"].ToString();
                CXPWEB = reader["CXPWEB"].ToString();
                CXPRELACDESDE = DateTime.Parse(reader["CXPRELACDESDE"].ToString()); //, new CultureInfo ("es-VE", false));                  
                CXPRELACDESDE = CXPRELACDESDE.Date;
                CXPRETENC = decimal.Parse(reader["CXPRETENC"].ToString());
                CXPCOMPRADOR = reader["CXPCOMPRADOR"].ToString();
                CXPNIT = reader["CXPNIT"].ToString();
                CXPRIF = reader["CXPRIF"].ToString();
                CXPEDO = reader["CXPEDO"].ToString();
                CXPCIUDAD = reader["CXPCIUDAD"].ToString();
                CXPCODPOSTAL = reader["CXPCODPOSTAL"].ToString();
                CXPRAMO = reader["CXPRAMO"].ToString();
                CXPFAX = reader["CXPFAX"].ToString();
                CXPTELF = reader["CXPTELF"].ToString();
                CXPDIASCRED = int.Parse(reader["CXPDIASCRED"].ToString());
                CXPBALACT = reader["CXPBALACT"].ToString();
                CXPRESENIA = reader["CXPRESENIA"].ToString();
                CXPCOD = reader["CXPCOD"].ToString();
                CXPRELAC = reader["CXPRELAC"].ToString();
                CXPCTAMAY = reader["CXPCTAMAY"].ToString();
                CXPFRMP = reader["CXPFRMP"].ToString();
                CXPNCTA = reader["CXPNCTA"].ToString();
                CXPSEL = int.Parse(reader["CXPSEL"].ToString());
                CXPTPOPERS = int.Parse(reader["CXPTPOPERS"].ToString());
                CXPGRUPO = reader["CXPGRUPO"].ToString();
                CXPITF = (reader["CXPITF"].ToString().Contains("1") ? true : false); //bool.Parse(reader["CXPITF"].ToString());
                CXPCATEGORIA = reader["CXPCATEGORIA"].ToString();
                CXPTIMBRE = (reader["CXPTIMBRE"].ToString().Contains("1") ? true : false);
                CXPIMPMUN = (reader["CXPIMPMUN"].ToString().Contains("1") ? true : false);

            }

            return this;
        }



        //SE UTILIZA EN AF_ASIGNA DROPDOWMLIST PROVEEDOR

        public IEnumerable<CXP_MAESTRO> GetProveedores()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            List<CXP_MAESTRO> proveedores = new List<CXP_MAESTRO>();
            string consulta = "SELECT  CXPNOMBRE ,  CXPCOD  FROM BICADM01W . CXP_MAESTRO";

            OleDbCommand DA = new OleDbCommand(consulta, cn);
            OleDbDataReader reader = DA.ExecuteReader();

            if (reader!= null)
            {
                while (reader.Read())
                {
                    Models.CXP_MAESTRO proveedor = new Models.CXP_MAESTRO();
                    proveedor.CXPCOD = reader["CXPCOD"].ToString();
                    proveedor.CXPNOMBRE = reader["CXPNOMBRE"].ToString();
                    //pre.DET_PROY = "ABC";
                    proveedores.Add(proveedor);
                }
            }
            cn.Close();
            return proveedores;
        }



        // PARA EL AUTOCOMPLETE
        #region Autocomplete
        public List<CXP_MAESTRO> GetProveedores2(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CXP_MAESTRO> proveedores = new List<CXP_MAESTRO>();
            string consulta = " SELECT cxpcod,cxpnombre FROM BICADM01W . CXP_MAESTRO WHERE(UPPER(CXPNOMBRE) LIKE CONCAT(CONCAT('%', UPPER('"+codigo+ "')), '%')) OR (UPPER(CXPCOD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) ";


           

            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CXP_MAESTRO listas = new CXP_MAESTRO();
                    {
                        listas.CXPCOD = item["CXPCOD"].ToString();
                        listas.CXPNOMBRE = item["CXPNOMBRE"].ToString();
                    }
                    proveedores.Add(listas);
                }
            }

                return proveedores;
            }

        // PARA EL AUTOCOMPLETE      
        public List<string> GetProveedores3(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> proveedores = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( CXPCOD , ' - ' ) , CXPNOMBRE ) AS CXPNOMB FROM BICADM01W . CXP_MAESTRO WHERE(UPPER(CXPNOMBRE) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) OR(UPPER(CXPCOD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%'))";

            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string proveedor = "";
                    proveedor = item["CXPNOMB"].ToString();
                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }





        #endregion

        public List<string> GetProveedores3(string codigo, string ndoc)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> listafinal = new List<string>();
            OleDbDataAdapter DA = new OleDbDataAdapter("Select bicadm01w.cxp_transacciones.cxpcod, bicadm01w.cxp_maestro.cxpnombre, bicadm01w.cxp_transacciones.tpodoc, bicadm01w.cxp_transacciones.ndoc, bicadm01w.cxp_transacciones.conc, bicadm01w.cxp_transacciones.fechdoc, bicadm01w.cxp_transacciones.fechvenc, bicadm01w.cxp_transacciones.mtoimp, bicadm01w.cxp_transacciones.total, bicadm01w.cxp_transacciones.pagar from bicadm01w.cxp_transacciones inner join bicadm01w.cxp_maestro on bicadm01w.cxp_transacciones.cxpcod=bicadm01w.cxp_maestro.cxpcod where bicadm01w.cxp_transacciones.cxpcod='" + codigo + "' and  bicadm01w.cxp_transacciones.ndoc='" + ndoc + "'", cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            foreach (DataRow linea in dt.Rows)
            {
                string listas;
                {
                    listas = linea["CXPNOMBRE"].ToString();
                    //listas = linea["MTOIMP"].ToString();
                    //listas = linea["TOTAL"].ToString();

                }
                listafinal.Add(listas);

            }
            return listafinal;

        }

        public static IEnumerable<SelectListItem> GetIva()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "0", Value = "0" },
                new SelectListItem{Text = "75", Value = "75"},
                new SelectListItem{Text = "100", Value = "100"},
            };
            return items;
        }


        //public class Row : Dictionary<string, object>
        //{

        //}
        //public class Rows : List<Row>
        //{

        //}


        public List<CXP_MAESTRO> BuscarCot(string codigo)
        {
            List<CXP_MAESTRO> listafinal = new List<CXP_MAESTRO>();

            string[] codigos = codigo.Split(',');

            foreach (string Elemento in codigos)
            {
                DataSet ds = new DataSet();
                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);

                string consulta = "Select * from bicadm01w." + tabla + " where " + Campo_Clave + " = '" + Elemento + "' ";
                OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
                DA.Fill(ds);
                DataTable dt = ds.Tables[0];

                if (dt.Rows != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CXP_MAESTRO tabla = new CXP_MAESTRO();

                        tabla.CXPNOMBRE = item["CXPNOMBRE"].ToString();
                        tabla.CXPCOD = item["CXPCOD"].ToString();
                        tabla.CXPCIUDAD = item["CXPCIUDAD"].ToString();
                        tabla.CXPRAMO = item["CXPRAMO"].ToString();
                        tabla.CXPCOND = item["CXPCOND"].ToString();
                        tabla.CXPRESENIA = item["CXPRESENIA"].ToString();
                        //tabla.CXPCONTCOD = item["CXPCONTCOD"].ToString();


                        listafinal.Add(tabla);

                    }
                }
            }

            return listafinal;
        }
    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_ALL_CXP_MAESTRO
//SP_DEL_CXP_MAESTRO
//SP_INS_CXP_MAESTRO
//SP_Q_CXP_MAESTRO
//SP_UPD_CXP_MAESTRO
//SP_Q_CXP_PROVEEDOR