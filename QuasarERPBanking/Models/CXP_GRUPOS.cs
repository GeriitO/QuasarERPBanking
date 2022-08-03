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
using System.Data.OleDb;
using System.Data;




namespace QuasarERPBanking.Models
{



    public class CXP_GRUPOS : clsMain
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
        [Display(Name = "lblIvaCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVACxpMaestro")]
        [Range(0.00, 100, ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIVAranCxpMaestro")]
        public decimal CXPRETENC { get; set; }
        [Display(Name = "lblNombreCxpMaestro", ResourceType = typeof(StringResources))]
        public string CXPCOMPRADOR { get; set; }
        [Display(Name = "lblSncCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errSNCCxpMaestro")]
        public string CXPNIT { get; set; }
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
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCtaMaCxpMaestro")]
        public string CXPCTAMAY { get; set; }
        [Display(Name = "lblFoPagCxpMaestro", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFoPagCxpMaestro")]
        public string CXPFRMP { get; set; }
        [Display(Name = "lblNCtaCxpMaestro", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNCtaCxpMaestro")]
        public string CXPNCTA { get; set; }
        public int CXPSEL { get; set; }
        [Display(Name = "lblTperCxpMaestro", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTperCxpMaestro")]
        public int CXPTPOPERS { get; set; }


   
        static string sp_get = "SP_Q_CXP_GRUPO";   //AUTOCOMPLETE
        static string sp_getAll = "SP_ALL_CXP_GRUPOS";  //DROPDOWN


        #region antiguo cxp grupos
        ///*void setCampos()
        //{
        //    CXPCOND
        //    CXPNOMBRE
        //    CXPDIR
        //    CXPWEB
        //    CXPRELACDESDE
        //    CXPRETENC
        //    CXPCOMPRADOR
        //    CXPNIT
        //    CXPRIF
        //    CXPEDO
        //    CXPCIUDAD
        //    CXPCODPOSTAL
        //    CXPRAMO
        //    CXPFAX
        //    CXPTELF
        //    CXPDIASCRED
        //    CXPBALACT
        //    CXPRESENIA
        //    CXPCOD
        //    CXPRELAC
        //    CXPCTAMAY
        //    CXPFRMP
        //    CXPNCTA
        //    CXPSEL
        //    CXPTPOPERS
        //    CXPGRUPO
        //    CXPITF
        //    CXPCATEGORIA
        //    CXPTIMBRE
        //    CXPIMPMUN
        //    USUARIO
        //    FECHA
        //}*/

        //public IEnumerable<CXP_GRUPOS> GetProveedores()
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
        //    List<object> reader = cn.execProc(sp_getAll, parametros, false);
        //    List<CXP_GRUPOS> grupos = new List<CXP_GRUPOS>();

        //    if (reader != null)
        //    {
        //        foreach (Dictionary<string, object> item in reader)
        //        {
        //            Models.CXP_GRUPOS grupo = new Models.CXP_GRUPOS();
        //            grupo.CXPCOD = item["CXPCOD"].ToString();
        //            grupo.CXPNOMBRE = item["CXPNOMBRE"].ToString();
        //            //pre.DET_PROY = "ABC";
        //            grupos.Add(grupo);
        //        }
        //    }

        //    return grupos;
        //}

        //public List<string> GetProveedores2(string codigo)
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
        //    parametros.Add(codigo);

        //    List<object> reader = cn.execProc("SP_Q_CXP_GRUPO", parametros, false);
        //    List<string> proveedores = new List<string>();
        //    //Models.PRE_DET pre = new Models.PRE_DET();
        //    if (reader != null)
        //    {
        //        foreach (Dictionary<string, object> item in reader)
        //        {
        //            string proveedor = "";
        //            proveedor = item["CXPNOMB"].ToString();
        //            proveedores.Add(proveedor);
        //        }
        //    }

        //    return proveedores;
        //}
        //public List<CXP_GRUPOS> GetProveedores3(string term)
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
        //    parametros.Add(term);
        //    List<object> reader = cn.execProc("SP_Q_CXP_GRUPO2", parametros, false);
        //    List<CXP_GRUPOS> proveedores = new List<CXP_GRUPOS>();

        //    if (reader != null)
        //    {
        //        foreach (Dictionary<string, object> item in reader)
        //        {
        //            Models.CXP_GRUPOS proveedor = new Models.CXP_GRUPOS();

        //            proveedor.CXPCOND = item["CXPCOND"].ToString();
        //            proveedor.CXPNOMBRE = item["CXPNOMBRE"].ToString();
        //            proveedor.CXPDIR = item["CXPDIR"].ToString();
        //            proveedor.CXPWEB = item["CXPWEB"].ToString();
        //            proveedor.CXPRELACDESDE = DateTime.Parse(item["CXPRELACDESDE"].ToString()); //, new CultureInfo ("es-VE", false));                  
        //            proveedor.CXPRELACDESDE = proveedor.CXPRELACDESDE.Date;
        //            proveedor.CXPRETENC = item["CXPRETENC"].ToString();
        //            proveedor.CXPCOMPRADOR = item["CXPCOMPRADOR"].ToString();
        //            proveedor.CXPNIT = item["CXPNIT"].ToString();
        //            proveedor.CXPRIF = item["CXPRIF"].ToString();
        //            proveedor.CXPEDO = item["CXPEDO"].ToString();
        //            proveedor.CXPCIUDAD = item["CXPCIUDAD"].ToString();
        //            proveedor.CXPCODPOSTAL = item["CXPCODPOSTAL"].ToString();
        //            proveedor.CXPRAMO = item["CXPRAMO"].ToString();
        //            proveedor.CXPFAX = item["CXPFAX"].ToString();
        //            proveedor.CXPTELF = item["CXPTELF"].ToString();
        //            proveedor.CXPDIASCRED = int.Parse(item["CXPDIASCRED"].ToString());
        //            proveedor.CXPBALACT = item["CXPBALACT"].ToString();
        //            proveedor.CXPRESENIA = item["CXPRESENIA"].ToString();
        //            proveedor.CXPCOD = item["CXPCOD"].ToString();
        //            proveedor.CXPRELAC = item["CXPRELAC"].ToString();
        //            proveedor.CXPCTAMAY = item["CXPCTAMAY"].ToString();
        //            proveedor.CXPFRMP = item["CXPFRMP"].ToString();
        //            proveedor.CXPNCTA = item["CXPNCTA"].ToString();
        //            proveedor.CXPSEL = int.Parse(item["CXPSEL"].ToString());
        //            proveedor.CXPTPOPERS = int.Parse(item["CXPTPOPERS"].ToString());
        //            //pre.DET_PROY = "ABC";
        //            proveedores.Add(proveedor);
        //        }
        //    }

        //    return proveedores;
        //}

        //public void Insert()
        //{
        //    CXPCOMPRADOR = "";
        //    CXPBALACT = "";
        //    CXPSEL = 0;
        //    Models.clsAS400 cn = new Models.clsAS400();

        //    ArrayList parametros = new ArrayList(32);

        //    parametros.Add(CXPCOND);
        //    parametros.Add(CXPNOMBRE);
        //    parametros.Add(CXPDIR);
        //    parametros.Add(CXPWEB);
        //    parametros.Add(CXPRELACDESDE.ToString("yyyy-MM-dd"));
        //    parametros.Add(CXPRETENC);
        //    parametros.Add(CXPCOMPRADOR);
        //    parametros.Add(CXPNIT);
        //    parametros.Add(CXPRIF);
        //    parametros.Add(CXPEDO);
        //    parametros.Add(CXPCIUDAD);
        //    parametros.Add(CXPCODPOSTAL);
        //    parametros.Add(CXPRAMO);
        //    parametros.Add(CXPFAX);
        //    parametros.Add(CXPTELF);
        //    parametros.Add(CXPDIASCRED);
        //    parametros.Add(CXPBALACT);
        //    parametros.Add(CXPRESENIA);
        //    parametros.Add(CXPCOD);
        //    parametros.Add(CXPRELAC);
        //    parametros.Add(CXPCTAMAY);
        //    parametros.Add(CXPFRMP);
        //    parametros.Add(CXPNCTA);
        //    parametros.Add(CXPSEL);
        //    parametros.Add(CXPTPOPERS);

        //    //parametros.Add(USUARIO);
        //    //parametros.Add(FECHA);
        //    cn.execProc("SP_INS_CXP_GRUPOS", parametros, true);
        //    if (cn.afectados > 0)
        //    { }

        //}

        //public void Delete()
        //{
        //    Models.clsAS400 cn = new Models.clsAS400();

        //    ArrayList parametros = new ArrayList(1);
        //    parametros.Add(CXPCOD);
        //    //clsAS400.afectados 
        //    cn.execProc("SP_DEL_CXP_GRUPOS", parametros, true);


        //}

        //public CXP_GRUPOS Buscar()
        //{
        //    Models.clsAS400 cn = new Models.clsAS400();

        //    ArrayList parametros = new ArrayList(1);
        //    parametros.Add(CXPCOD);

        //    List<object> reader = cn.execProc(sp_get, parametros, false);
        //    //List<CXP_MAESTRO> proveedores = new List<CXP_MAESTRO>();
        //    //Models.CXP_MAESTRO proveedor = new Models.CXP_MAESTRO();
        //    if (reader != null)
        //    {
        //        foreach (Dictionary<string, object> item in reader)
        //        {

        //            //estos no los utiliza
        //            CXPCOMPRADOR = item["CXPCOMPRADOR"].ToString();
        //            CXPDIASCRED = int.Parse(item["CXPDIASCRED"].ToString());
        //            CXPBALACT = item["CXPBALACT"].ToString();
        //            CXPFRMP = item["CXPFRMP"].ToString();
        //            CXPNCTA = item["CXPNCTA"].ToString();
        //            CXPTPOPERS = int.Parse(item["CXPTPOPERS"].ToString());
        //            CXPSEL = int.Parse(item["CXPSEL"].ToString());
        //            CXPRESENIA = item["CXPRESENIA"].ToString();
        //            CXPCOND = item["CXPCOD"].ToString();
        //            CXPNOMBRE = item["CXPNOMBRE"].ToString();
        //            CXPDIR = item["CXPDIR"].ToString();
        //            CXPWEB = item["CXPWEB"].ToString();
        //            CXPRELACDESDE = DateTime.Parse(item["CXPRELACDESDE"].ToString());
        //            CXPRETENC = item["CXPRETENC"].ToString();
        //            CXPCOMPRADOR = item["CXPCOMPRADOR"].ToString();
        //            CXPNIT = item["CXPNIT"].ToString();
        //            CXPRIF = item["CXPRIF"].ToString();
        //            CXPEDO = item["CXPEDO"].ToString();
        //            CXPCIUDAD = item["CXPCIUDAD"].ToString();
        //            CXPCODPOSTAL = item["CXPCODPOSTAL"].ToString();
        //            CXPRAMO = item["CXPRAMO"].ToString();
        //            CXPFAX = item["CXPFAX"].ToString();
        //            CXPTELF = item["CXPTELF"].ToString();
        //            CXPDIASCRED = int.Parse(item["CXPDIASCRED"].ToString());
        //            CXPBALACT = item["CXPBALACT"].ToString();
        //            CXPRESENIA = item["CXPRESENIA"].ToString();
        //            CXPCOD = item["CXPCOD"].ToString();
        //            CXPRELAC = item["CXPRELAC"].ToString();
        //            CXPCTAMAY = item["CXPCTAMAY"].ToString();
        //            CXPFRMP = item["CXPFRMP"].ToString();
        //            CXPNCTA = item["CXPNCTA"].ToString();
        //            CXPSEL = int.Parse(item["CXPSEL"].ToString());
        //            CXPTPOPERS = int.Parse(item["CXPTPOPERS"].ToString());

        //        }
        //    }

        //    return this;




        //}


        //public void Update()
        //{
        //    CXPCOMPRADOR = "";
        //    CXPBALACT = "";
        //    CXPSEL = 0;


        //    Models.clsAS400 cn = new Models.clsAS400();

        //    ArrayList parametros = new ArrayList(32);

        //    parametros.Add(CXPCOND);
        //    parametros.Add(CXPNOMBRE);
        //    parametros.Add(CXPDIR);
        //    parametros.Add(CXPWEB);
        //    parametros.Add(CXPRELACDESDE.ToString("yyyy-MM-dd"));
        //    parametros.Add(CXPRETENC);
        //    parametros.Add(CXPCOMPRADOR);
        //    parametros.Add(CXPNIT);
        //    parametros.Add(CXPRIF);
        //    parametros.Add(CXPEDO);
        //    parametros.Add(CXPCIUDAD);
        //    parametros.Add(CXPCODPOSTAL);
        //    parametros.Add(CXPRAMO);
        //    parametros.Add(CXPFAX);
        //    parametros.Add(CXPTELF);
        //    parametros.Add(CXPDIASCRED);
        //    parametros.Add(CXPBALACT);
        //    parametros.Add(CXPRESENIA);
        //    parametros.Add(CXPCOD);
        //    parametros.Add(CXPRELAC);
        //    parametros.Add(CXPCTAMAY);
        //    parametros.Add(CXPFRMP);
        //    parametros.Add(CXPNCTA);
        //    parametros.Add(CXPSEL);
        //    parametros.Add(CXPTPOPERS);
        //    //parametros.Add(USUARIO);
        //    //parametros.Add(FECHA); 		

        //    cn.execProc("SP_UPD_CXP_GRUPOS", parametros, true);
        //    //  var x= ViewBag.ViewBag.ProyectoSeleccionado;
        //    //  return View();
        //    // Update model to your db  
        //    //string message = "Success";
        //    //return Json(message, JsonRequestBehavior.AllowGet);
        //}

        ////public String ToString()
        ////{
        ////   return this.CXPCOD;
        ////}


        #endregion



        #region Constructor
        public CXP_GRUPOS()
        {
                      
            Campo_Clave = "CXPCOD";
        }
        #endregion

        protected override ArrayList getParameters()
        {
            CXPNCTA = "0";
            CXPCTAMAY = "0";
            CXPFRMP = "-";
            CXPRETENC = 0;
            CXPTPOPERS = 1;
            CXPDIASCRED = 0;
            CXPBALACT = "0";


            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(25);

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
                
            }

            return this;
        }



        //SE UTILIZA DROPDOWMLIST 

        public IEnumerable<CXP_GRUPOS> GetDropdownlist() // <<-- NO TIENE UNA FUNCIONALIDAD 
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            Rows reader = cn.execProc(sp_getAll, parametros, false);
            List<CXP_GRUPOS> grupos = new List<CXP_GRUPOS>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Models.CXP_GRUPOS grupo = new Models.CXP_GRUPOS();
                    grupo.CXPCOD = item["CXPCOD"].ToString();
                    grupo.CXPNOMBRE = item["CXPNOMBRE"].ToString();

                    grupos.Add(grupo);
                }
            }

            return grupos;
        }


        // PARA EL AUTOCOMPLETE
        #region Autocomplete
        public List<string> GetAutocGrupos(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> grupos = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( CXPCOD , ' - ' ) , CXPNOMBRE ) AS CXPNOMB FROM BICADM01W . CXP_GRUPOS WHERE(UPPER(CXPNOMBRE) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) OR(UPPER(CXPCOD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%'))";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string grupo = "";                   
                    grupo = item["CXPNOMB"].ToString();  // ESTE ES EL ALIAS                   
                    grupos.Add(grupo);
                }
            }

            return grupos;
        }
        #endregion






    }




}


////// CAMPOS DE LA TABLA ////////
//   CXPCOND 		
//   CXPNOMBRE 		
//   CXPDIR 		
//   CXPWEB 		
//   CXPRELACDESDE 	
//   CXPRETENC 		
//   CXPCOMPRADOR 	
//   CXPNIT 		
//   CXPRIF 		
//   CXPEDO 		
//   CXPCIUDAD 		
//   CXPCODPOSTAL 	
//   CXPRAMO 		
//   CXPFAX 		
//   CXPTELF 		
//   CXPDIASCRED 	
//   CXPBALACT 		
//   CXPRESENIA 	
//   CXPCOD 		
//   CXPRELAC 		
//   CXPCTAMAY 		
//   CXPFRMP 		
//   CXPNCTA 		
//   CXPSEL 		
//	 CXPTPOPERS 	






//////////////// SP UTILIZADOS ////////////////
//SP_Q_CXP_GRUPO
//SP_ALL_CXP_GRUPOS
//SP_DEL_CXP_GRUPOS
//SP_INS_CXP_GRUPOS
//SP_Q_CXP_GRUPOS
//SP_UPD_CXP_GRUPOS