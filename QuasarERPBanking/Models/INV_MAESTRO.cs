using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources.Inventario;
using System.Data.OleDb;
using System.Data;


namespace QuasarERPBanking.Models
{
    public class Inventario : ConectDB
    {

        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errNom")]
        public string INVNOM { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDesc")]
        public string INVDES { get; set; }

        //[Display(Name = "Proveedor:")]
        [Display(Name = "lblProv", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errProv")]
        public string INVREF { get; set; }

        [Display(Name = "lblGrupo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errGrupo")]
        public int INVGRU { get; set; }

        [Display(Name = "lblTipo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errTipo")]
        public int INVTIP { get; set; }
        public string INVUBI { get; set; }
        public string INVDEP { get; set; }

        [Display(Name = "lblZona", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errZona")]
        public int INVZON { get; set; }

        [Display(Name = "lblMarca", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errMarca")]
        public string INVMAR { get; set; }

        [Display(Name = "lblModel", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errModel")]
        public string INVMOD { get; set; }
        public string INVSER { get; set; }

        [Display(Name = "lblUnidad", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errUnidad")]
        public string INVUNI { get; set; }

        //[Display(Name = "Minimo:")]
        [Display(Name = "lblMinimo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errMinimo")]
        public string INVMIN { get; set; }
        //[Display(Name = "Maximo:")]
        [Display(Name = "lblMax", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errMax")]
        public string INVMAX { get; set; }

        //[Display(Name = "Existente:")]
        [Display(Name = "lblExist", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errExist")]
        public double INVEXI { get; set; }

        //[Display(Name = "IVA:")]
        [Display(Name = "lblIva", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errIva")]
        public string INVIVA { get; set; }
        public string INVPREMAY { get; set; }
        public string INVPREPROM { get; set; }
        public string INVPREDET { get; set; }
        public string INVCOSANT { get; set; }
        public string INVCOSPRO { get; set; }
        public string INVCOSACT { get; set; }
        public string INVCOSVAL { get; set; }
        //[Display(Name = "Reorden:")]
        [Display(Name = "lblReor", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errReor")]
        public string INVREO { get; set; }


        //[Display(Name = "Ordenada:")]
        [Display(Name = "lblOrde", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errOrde")]
        public string INVORD { get; set; }

        //[Display(Name = "Reservada:")]
        [Display(Name = "lblReservada", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errReservada")]
        public string INVRES { get; set; }

        //[Display(Name = "Fisica:")]
        [Display(Name = "lblFisica", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errFisica")]
        public string INVFIS { get; set; }

        //[Display(Name = "Codigo:")]
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCod")]
        public string INVCOD { get; set; }

        //[Display(Name = "Requerida:")]
        [Display(Name = "lblRequer", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errRequer")]
        public string INVCANTREQ { get; set; }

        //[Display(Name = "Cta Gastos:")]
        [Display(Name = "lblCtGas", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCtGas")]
        public string INVCTAGTO { get; set; }

        //[Display(Name = "Cta Inventario:")]
        [Display(Name = "lblCtInv", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errCtInv")]
        public string INVCTAINV { get; set; }

        //[Display(Name = "Disponible:")]
        [Display(Name = "lblDispo", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errDispo")]
        public string INVDISP { get; set; }
        public string INVSERD { get; set; }
        public string INVSERO { get; set; }
        public string INVVAL { get; set; }
        public string INVADJ { get; set; }
        public string INVISLR { get; set; }

        //[Display(Name = "Directo a Gasto:")]
        [Display(Name = "lblDircGto", ResourceType = typeof(StringResources))]
        public bool INVGASTO { get; set; }

        public string INVMARCAJE { get; set; }
        string USUARIO { get; set; }
        DateTime FECHA { get; set; }

        static string sp_getAll = "SP_ALL_Inventario";
        static string sp_get = "SP_Q_Inventario";
        static string sp_existe = "SP_E_Inventario";

        public IEnumerable<System.Web.Mvc.SelectListItem> Grupos { get; set; }


        public bool Existe()
        {
            bool existe = false;
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_GRUPOS> lstGrupos = new List<INV_GRUPOS>();
            string consulta = "SELECT  INVCOD  AS COD  FROM " + ParametrosGlobales.bd + "  INV_MAESTRO  WHERE INVCOD =  '" + INVCOD + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (item["COD"].ToString() != null)
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }

        #region AUTOCOMPLETE PARA BUSCAR POR NOMBRE Y CODIGO 
        public List<string> GetProducto(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> productos = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( INVCOD , ' - ' ) , INVNOM ) AS INVNOMB FROM " + ParametrosGlobales.bd + "  INV_MAESTRO WHERE (UPPER(INVNOM) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) OR(UPPER(INVCOD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%'))";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string producto = "";
                    producto = item["INVNOMB"].ToString();
                    productos.Add(producto);
                }
            }
            return productos;
        }
        #endregion



        #region AUTOCOMPLETE PARA BUSCAR POR PORUDCTO POR EXISTENCIA DE DEPARTAMENTO INV_NEMAESTRO
        public List<string> GetProducXdep(string codigo, string deposito)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> productos = new List<string>();
            string consulta = "SELECT DISTINCT CONCAT ( CONCAT ( A . INVCOD , ' - ' ) , A . INVNOM ) AS INVNOMB FROM "+ParametrosGlobales.bd+ "  INV_MAESTRO A INNER JOIN " + ParametrosGlobales.bd + "INV_FIFO B ON A . INVCOD = B . INVCOD WHERE ( ( UPPER ( A . INVNOM ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) OR ( UPPER ( A . INVCOD ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) ) AND B . INVDEP = '" + deposito + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string producto = "";
                    producto = item["INVNOMB"].ToString();
                    productos.Add(producto);
                }
            }
            return productos;
        }
        #endregion

        public IEnumerable<Inventario> GetProductos() //<<-- No tiene funcionalidad
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            List<object> reader = cn.execProc("SP_Q_INV_PRODUCTO", parametros, false);
            List<Inventario> productos = new List<Inventario>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Inventario producto = new Inventario();
                    producto.INVCOD = item["INVCOD"].ToString();
                    producto.INVNOM = item["INVNOM"].ToString();
                    productos.Add(producto);
                }
            }
            return productos;
        }

        public IEnumerable<Inventario> GetProveedores2()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(4);
            /*parametros.Add(DET_CC);
            parametros.Add(DET_DEF);
            parametros.Add(PREPROCOD);*/
            List<object> reader = cn.execProc("SP_PARTT", parametros, false);
            List<Inventario> productos = new List<Inventario>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Inventario producto = new Inventario();
                    producto.INVCOD = item["INVCOD"].ToString();
                    producto.INVNOM = item["INVNOM"].ToString();
                    productos.Add(producto);
                }
            }
            return productos;
        }

        protected override string CAMPOS()
        {
            string CAMPOS = "INVNOM ,INVDES ,INVREF ,INVGRU ,INVTIP ,INVUBI ,INVDEP ,INVZON ,INVMAR ,INVMOD ,INVSER ,INVUNI ,INVMIN ,INVMAX ,INVEXI ,INVIVA ,INVPREMAY ,INVPREPROM ,INVPREDET ,INVCOSANT ,INVCOSPRO ,INVCOSACT ,INVCOSVAL ,INVREO ,INVORD ,INVRES ,INVFIS ,INVCOD ,INVCANTREQ ,INVCTAGTO ,INVCTAINV ,INVDISP ,INVSERD ,INVSERO ,INVVAL ,INVADJ ,INVISLR ,INVGASTO ,INVMARCAJE";
            return CAMPOS;
        }
        public int Insert()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);
            INVPREMAY = "0";
            INVPREPROM = "0";
            INVPREDET = "0";
            INVCOSANT = "0";
            INVCOSPRO = "0";
            INVCOSACT = "0";
            INVCOSVAL = "0";
            INVUBI = "";
            INVDEP = "";
            INVSER = "";
            INVSERD = "";
            INVSERO = "";
            INVVAL = "";
            INVADJ = "";
            INVISLR = "";
            INVMARCAJE = "";
            USUARIO = "";
            INVEXI = 0;
            INVIVA ="0" ;
            INVORD = "0";
            INVRES = "0";
            INVFIS = "0";
            INVCANTREQ = "0";
            INVDISP = "0";
            INVREO = "0";
            //FECHA = "";

            parametros.Add(INVNOM);
            parametros.Add(INVDES);
            parametros.Add(INVREF);
            parametros.Add(INVGRU);
            parametros.Add(INVTIP);
            parametros.Add(INVUBI);
            parametros.Add(INVDEP);
            parametros.Add(INVZON);
            parametros.Add(INVMAR);
            parametros.Add(INVMOD);
            parametros.Add(INVSER);
            parametros.Add(INVUNI);
            parametros.Add(INVMIN);
            parametros.Add(INVMAX);
            parametros.Add(INVEXI);
            parametros.Add(INVIVA);
            parametros.Add(INVPREMAY);
            parametros.Add(INVPREPROM);
            parametros.Add(INVPREDET);
            parametros.Add(INVCOSANT);
            parametros.Add(INVCOSPRO);
            parametros.Add(INVCOSACT);
            parametros.Add(INVCOSVAL);
            parametros.Add(INVREO);
            parametros.Add(INVORD);
            parametros.Add(INVRES);
            parametros.Add(INVFIS);
            parametros.Add(INVCOD);
            parametros.Add(INVCANTREQ);
            parametros.Add(INVCTAGTO);
            parametros.Add(INVCTAINV);
            parametros.Add(INVDISP);
            parametros.Add(INVSERD);
            parametros.Add(INVSERO);
            parametros.Add(INVVAL);
            parametros.Add(INVADJ);
            parametros.Add(INVISLR);
            parametros.Add(INVGASTO ? "1" : "0");
            parametros.Add(INVMARCAJE);
            //parametros.Add(USUARIO);
            //parametros.Add(FECHA);
            cn.Insert2(tabla, parametros, true, CAMPOS() );
            return cn.afectados;

        }

        public void Delete()
        {
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(INVCOD);
            cn.execProc("SP_DEL_Inventario", parametros, true);
            //clsAS400.afectados 
        }

        public int getParametersUpd ()
        {

            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            INVPREMAY = "0";
            INVPREPROM = "0";
            INVPREDET = "0";
            INVCOSANT = "0";
            INVCOSPRO = "0";
            INVCOSACT = "0";
            INVCOSVAL = "0";
            INVUBI = "-";
            INVDEP = "-";
            INVSER = "-";
            INVSERD = "-";
            INVSERO = "-";
            INVVAL = "0";
            INVADJ = "-";
            INVISLR = "-";
            INVMARCAJE = "0";
            USUARIO = "";
            INVEXI = 0;
            INVIVA = "0";
            INVORD = "0";
            INVRES = "0";
            INVFIS = "0";
            INVCANTREQ = "0";
            INVDISP = "0";
            INVPREMAY = "0";
            INVPREPROM = "0";
            INVPREDET = "0";
            int boolInt = INVGASTO ? 1 : 0;
            INVGASTO = INVGASTO;



            parametros.Add("INVNOM ="+"'"+INVNOM+"'");
            parametros.Add("INVDES=" + "'" +INVDES+"'");
            parametros.Add("INVREF=" + "'" +INVREF + "'");
            parametros.Add("INVGRU=" + "'" +INVGRU + "'");
            parametros.Add("INVTIP=" + "'" +INVTIP + "'");
            parametros.Add("INVUBI=" + "'" +	INVUBI + "'");
            parametros.Add("INVDEP=" + "'" +	INVDEP + "'");
            parametros.Add("INVZON=" + "'" +	INVZON + "'");
            parametros.Add("INVMAR=" + "'" +	INVMAR + "'");
            parametros.Add("INVMOD=" + "'" +	INVMOD + "'");
            parametros.Add("INVSER=" + "'" +	INVSER + "'");
            parametros.Add("INVUNI=" + "'" +	INVUNI + "'");
            parametros.Add("INVMIN=" + "'" +	INVMIN + "'");
            parametros.Add("INVMAX=" + "'" +	INVMAX + "'");
            parametros.Add("INVEXI=" + "'" +	INVEXI + "'");
            parametros.Add("INVIVA=" + "'" +	INVIVA + "'");
            parametros.Add("INVPREMAY=" + "'" +INVPREMAY + "'");
            parametros.Add("INVPREPROM=" + "'"+INVPREPROM + "'");
            parametros.Add("INVPREDET=" + "'" +INVPREDET + "'");
            parametros.Add("INVCOSANT=" + "'" +INVCOSANT + "'");
            parametros.Add("INVCOSPRO=" + "'" +INVCOSPRO + "'");
            parametros.Add("INVCOSACT=" + "'" +INVCOSACT + "'");
            parametros.Add("INVCOSVAL=" + "'" +INVCOSVAL + "'");
            parametros.Add("INVREO=" + "'" 	+INVREO + "'");
            parametros.Add("INVORD=" + "'" 	+INVORD + "'");
            parametros.Add("INVRES=" + "'" 	+INVRES + "'");
            parametros.Add("INVFIS=" + "'" 	+INVFIS + "'");
            parametros.Add("INVCOD=" + "'" 	+INVCOD + "'");
            parametros.Add("INVCANTREQ=" + "'"+INVCANTREQ + "'");
            parametros.Add("INVCTAGTO=" + "'" +INVCTAGTO + "'");
            parametros.Add("INVCTAINV=" + "'" +INVCTAINV + "'");
            parametros.Add("INVDISP=" + "'" 	+INVDISP + "'");
            parametros.Add("INVSERD=" + "'" 	+INVSERD + "'");
            parametros.Add("INVSERO=" + "'" 	+INVSERO + "'");
            parametros.Add("INVVAL=" + "'" 	+INVVAL + "'");
            parametros.Add("INVADJ=" + "'" 	+INVADJ + "'");
            parametros.Add("INVISLR=" + "'" +	INVISLR + "'");
            parametros.Add("INVGASTO=" + "'" + boolInt + "'");
            parametros.Add("INVMARCAJE = " + "'"+INVMARCAJE + "'");
            //parametros.Add(USUARIO);
            //parametros.Add(FECHA);

            cn.Update2(tabla, parametros, whereParam(), true);
            //  var x= ViewBag.ViewBag.ProyectoSeleccionado;
            //  return View();
            // Update model to your db  
            //string message = "Success";
            //return Json(message, JsonRequestBehavior.AllowGet);
            return cn.afectados;
        }

     

        public Inventario Buscar()         //<<-- NO TIENE FUNCIONALIDAD
        {
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(INVCOD);
            List<object> reader = cn.execProc(sp_get, parametros, false);
            //List<CXP_MAESTRO> proveedores = new List<CXP_MAESTRO>();
            //Models.CXP_MAESTRO proveedor = new Models.CXP_MAESTRO();
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    INVNOM = item["INVNOM"].ToString();
                    INVDES = item["INVDES"].ToString();
                    INVREF = item["INVREF"].ToString();
                    INVGRU = int.Parse(item["INVGRU"].ToString());
                    INVTIP = int.Parse(item["INVTIP"].ToString());
                    INVUBI = item["INVUBI"].ToString();
                    INVDEP = item["INVDEP"].ToString();
                    INVZON = int.Parse(item["INVZON"].ToString());
                    INVMAR = item["INVMAR"].ToString();
                    INVMOD = item["INVMOD"].ToString();
                    INVSER = item["INVSER"].ToString();
                    INVUNI = item["INVUNI"].ToString();
                    INVMIN = item["INVMIN"].ToString();
                    INVMAX = item["INVMAX"].ToString();
                    INVEXI = double.Parse(item["INVEXI"].ToString());
                    INVIVA = item["INVIVA"].ToString();
                    INVPREMAY = item["INVPREMAY"].ToString();
                    INVPREPROM = item["INVPREPROM"].ToString();
                    INVPREDET = item["INVPREDET"].ToString();
                    INVCOSANT = item["INVCOSANT"].ToString();
                    INVCOSPRO = item["INVCOSPRO"].ToString();
                    INVCOSACT = item["INVCOSACT"].ToString();
                    INVCOSVAL = item["INVCOSVAL"].ToString();
                    INVREO = item["INVREO"].ToString();
                    INVORD = item["INVORD"].ToString();
                    INVRES = item["INVRES"].ToString();
                    INVFIS = item["INVFIS"].ToString();
                    INVCOD = item["INVCOD"].ToString();
                    INVCANTREQ = item["INVCANTREQ"].ToString();
                    INVCTAGTO = item["INVCTAGTO"].ToString();
                    INVCTAINV = item["INVCTAINV"].ToString();
                    INVDISP = item["INVDISP"].ToString();
                    INVSERD = item["INVSERD"].ToString();
                    INVSERO = item["INVSERO"].ToString();
                    INVVAL = item["INVVAL"].ToString();
                    INVADJ = item["INVADJ"].ToString();
                    INVISLR = item["INVISLR"].ToString();
                    INVGASTO = item["INVGASTO"].ToString() == "1" ? true : false;
                    //INVGASTO = bool.Parse(item["INVGASTO"].ToString());
                    INVMARCAJE = item["INVMARCAJE"].ToString();
                    USUARIO = item["USUARIO"].ToString();
                    FECHA = DateTime.Parse(item["FECHA"].ToString());
                }
            }
            return this;
        }

        public List<Inventario> GetProdAutoCom(string term)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<Inventario> productos = new List<Inventario>();
            string consulta = "SELECT INVNOM , INVDES , INVREF , INVGRU , INVTIP , INVUBI , INVDEP , INVZON , INVMAR , INVMOD , INVSER , INVUNI , INVMIN , INVMAX , INVEXI , INVIVA , INVPREMAY , INVPREPROM , INVPREDET , INVCOSANT , INVCOSPRO , INVCOSACT , INVCOSVAL , INVREO , INVORD , INVRES , INVFIS , INVCOD , INVCANTREQ , INVCTAGTO , INVCTAINV , INVDISP , INVSERD , INVSERO , INVVAL , INVADJ , INVISLR , INVGASTO , INVMARCAJE FROM " + ParametrosGlobales.bd + "  INV_MAESTRO WHERE INVCOD = '" + term + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.Inventario producto = new Models.Inventario();

                    producto.INVNOM = item["INVNOM"].ToString();
                    producto.INVDES = item["INVDES"].ToString();
                    producto.INVREF = item["INVREF"].ToString();
                    producto.INVGRU = int.Parse(item["INVGRU"].ToString());
                    producto.INVTIP = int.Parse(item["INVTIP"].ToString());
                    producto.INVUBI = item["INVUBI"].ToString();
                    producto.INVDEP = item["INVDEP"].ToString();
                    producto.INVZON = int.Parse(item["INVZON"].ToString());
                    producto.INVMAR = item["INVMAR"].ToString();
                    producto.INVMOD = item["INVMOD"].ToString();
                    producto.INVSER = item["INVSER"].ToString();
                    producto.INVUNI = item["INVUNI"].ToString();
                    producto.INVMIN = item["INVMIN"].ToString();
                    producto.INVMAX = item["INVMAX"].ToString();
                    producto.INVEXI = double.Parse(item["INVEXI"].ToString());
                    producto.INVIVA = item["INVIVA"].ToString();
                    producto.INVPREMAY = item["INVPREMAY"].ToString();
                    producto.INVPREPROM = item["INVPREPROM"].ToString();
                    producto.INVPREDET = item["INVPREDET"].ToString();
                    producto.INVCOSANT = item["INVCOSANT"].ToString();
                    producto.INVCOSPRO = item["INVCOSPRO"].ToString();
                    producto.INVCOSACT = item["INVCOSACT"].ToString();
                    producto.INVCOSVAL = item["INVCOSVAL"].ToString();
                    producto.INVREO = item["INVREO"].ToString();
                    producto.INVORD = item["INVORD"].ToString();
                    producto.INVRES = item["INVRES"].ToString();
                    producto.INVFIS = item["INVFIS"].ToString();
                    producto.INVCOD = item["INVCOD"].ToString();
                    producto.INVCANTREQ = item["INVCANTREQ"].ToString();
                    producto.INVCTAGTO = item["INVCTAGTO"].ToString();
                    producto.INVCTAINV = item["INVCTAINV"].ToString();
                    producto.INVDISP = item["INVDISP"].ToString();
                    producto.INVSERD = item["INVSERD"].ToString();
                    producto.INVSERO = item["INVSERO"].ToString();
                    producto.INVVAL = item["INVVAL"].ToString();
                    producto.INVADJ = item["INVADJ"].ToString();
                    producto.INVISLR = item["INVISLR"].ToString();
                    producto.INVGASTO = item["INVGASTO"].ToString() == "0" ? false : true;
                    producto.INVMARCAJE = item["INVMARCAJE"].ToString();
                    //producto.USUARIO = item["USUARIO"].ToString();
                    //producto.FECHA = DateTime.Parse(item["FECHA"].ToString());
                    productos.Add(producto);
                }
            }

            return productos;
        }

        protected override object whereParam()
        {
            string where = "INVCOD='" + INVCOD+ "'" + "";


            return where;
        }


    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_ALL_INV_MAESTRO
//SP_Q_INV_MAESTRO
//SP_E_INV_MAESTRO
//SP_Q_INV_PRODUCTO_
//SP_Q_INV_PRODUCTO
//SP_PARTT
//SP_INS_INV_MAESTRO
//SP_DEL_INV_MAESTRO
//SP_UPD_INV_MAESTRO
//SP_Q_INV_PRODUCTO



