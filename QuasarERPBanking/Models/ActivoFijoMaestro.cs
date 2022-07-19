using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.Productos;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using QuasarERPBanking.Models;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class Productos : ConectDB
    {

        [Display(Name = "lblNom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNom")]
        public string AFNOM { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDesc")]
        public string AFDES { get; set; }

        public string AFREF { get; set; }
        [Display(Name = "lblGrupo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrGrupo")]
        public string AFGRU { get; set; }

        [Display(Name = "lblTipo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrTipo")]
        public string AFTIP { get; set; }

        [Display(Name = "lblDepo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDepo")]
        public string AFDEP { get; set; }
        [Display(Name = "lblMarca", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMarca")]
        public string AFMAR { get; set; }

        [Display(Name = "lblModelo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrModelo")]
        public string AFMOD { get; set; }

        public string AFSER { get; set; }

        [Display(Name = "lblUniAsig", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrUniAsig")]
        public string AFUNIASIG { get; set; }

        [Display(Name = "lblTotal", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrTotal")]
        public double AFEXI { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
  
        public string AFCOD { get; set; }

        [Display(Name = "lblUnidad", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrUnidad")]
        public string AFUNIDAD { get; set; }
        public double AFFIS { get; set; }
        public decimal AFMONTO { get; set; }
        public string AFCODOLD { get; set; }
        public string AFNOMOLD { get; set; }
        [Display(Name = "lblCateg", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCateg")]
        public string AFCATESPECCOD { get; set; }

        [Display(Name = "lblCalcu", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCalcu")]
        public string calculo { get; set; }



        static string sp_getAll = "SP_ALL_AF_MAESTRO";

        //para cargar unidad, nombre y grupo en af_asigna 
        public static IEnumerable<Productos> GetAfMaestro()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<Productos> activos = new List<Productos>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Productos afmaestro = new Productos();

                    afmaestro.AFNOM = item["AFNOM"].ToString();
                    afmaestro.AFNOM = item["AFGRU"].ToString();
                    afmaestro.AFUNIDAD = item["AFUNIDAD"].ToString();
                    activos.Add(afmaestro);
                }
            }

            return activos;
        }



        public Productos()
        {
            tabla = "AF_MAESTRO";
            Campo_Clave = "AFCOD";
        }

        //SE UTILIZA PARA INSERT Y UPDATE
        protected override ArrayList getParameters()
        {

            AFREF = "";
            AFSER = "";
            AFCODOLD = "";
            AFNOMOLD = "";
            AFUNIASIG = "";


            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(18);

            parametros.Add(AFNOM);
            parametros.Add(AFDES);
            parametros.Add(AFREF);
            parametros.Add(AFGRU);
            parametros.Add(AFTIP);
            parametros.Add(AFDEP);
            parametros.Add(AFMAR);
            parametros.Add(AFMOD);
            parametros.Add(AFSER);
            parametros.Add(AFUNIASIG);
            parametros.Add(AFEXI);
            parametros.Add(AFCOD);
            parametros.Add(AFUNIDAD);
            parametros.Add(AFFIS);
            parametros.Add(AFMONTO);
            parametros.Add(AFCODOLD);
            parametros.Add(AFNOMOLD);
            parametros.Add(AFCATESPECCOD);

            return parametros;
        }

        protected override ConectDB LoadObject(Rows reader)
        {
            foreach (Row item in reader)
            {
                AFNOM = item["AFNOM"].ToString();
                AFDES = item["AFDES"].ToString();
                AFREF = item["AFREF"].ToString();
                AFGRU = item["AFGRU"].ToString();
                AFTIP = item["AFTIP"].ToString();
                AFDEP = item["AFDEP"].ToString();
                AFMAR = item["AFMAR"].ToString();
                AFMOD = item["AFMOD"].ToString();
                AFSER = item["AFSER"].ToString();
                //AFUNIASIG= item["AFUNIASIG"].ToString();
                AFEXI = double.Parse(item["AFEXI"].ToString());
                AFCOD = item["AFCOD"].ToString();
                AFUNIDAD = item["AFUNIDAD"].ToString();
                AFFIS = double.Parse(item["AFFIS"].ToString());
                AFMONTO = decimal.Parse(item["AFMONTO"].ToString());
                AFCODOLD = item["AFCODOLD"].ToString();
                AFNOMOLD = item["AFNOMOLD"].ToString();
                AFCATESPECCOD = item["AFCATESPECCOD"].ToString();
                double doble = 0;
                double.TryParse(item["AFUNIASIG"].ToString(), out doble);
                AFUNIASIG = doble.ToString();
                calculo = (AFEXI - doble).ToString();

            }

            return this;
        }


        #region AUTOCOMPLETE BUSCAR POR PRODUCTO
        public List<string> GetCampos(string codigo)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);



            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> productos = new List<string>();
            string consulta = "SELECT  CONCAT ( CONCAT ( AFCOD , ' - ' ) , AFNOM ) AS AFCOD FROM " + ParametrosGlobales.bd + "  AF_MAESTRO WHERE ( UPPER ( AFNOM ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) OR ( UPPER ( AFCOD ) LIKE CONCAT ( UPPER ( '" + codigo + "' ) , '%' ) )";
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
        


        //PARA EL CAMPO BUSQUEDA DE MovimientoActivoFijo
        public List<string> GetTrans(string codigo)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> productos = new List<string>();
            string consulta = consulta = "SELECT CONCAT ( CONCAT ( AFCOD , ' - ' ) , AFNOM ) AS TRANS FROM " + ParametrosGlobales.bd + " AF_MAESTRO WHERE ( UPPER ( AFNOM ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) OR ( UPPER ( AFCOD ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) )";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    string producto = "";
                    producto = item["TRANS"].ToString();
                    productos.Add(producto);
                }
            }

            return productos;
        }



        protected override object whereParam(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {
                QUERY = "AFCOD='" + elemento + "'";
            }

            return QUERY;
        }

        //public bool Existe()
        //{
        //    bool existe = false;
        //    DataSet ds = new DataSet();
        //    OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
        //    List<INV_GRUPOS> lstGrupos = new List<INV_GRUPOS>();
        //    string consulta = "SELECT  AFCOD  AS COD  FROM " + ParametrosGlobales.bd + "  AF_MAESTRO  WHERE AFCOD =  '" + AFCOD + "'";
        //    OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
        //    DA.Fill(ds);
        //    DataTable dt = ds.Tables[0];

        //    if (dt.Rows != null)
        //    {
        //        foreach (DataRow item in dt.Rows)
        //        {
        //            if (item["COD"].ToString() != null)
        //            {
        //                existe = true;
        //            }
        //        }
        //    }
        //    return existe;
        //}

        public int Insert()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            //AFCODOLD = "0";
            //AFNOMOLD = "0";



            parametros.Add(AFNOM);
            parametros.Add(AFDES);
            parametros.Add(AFREF);
            parametros.Add(AFGRU);
            parametros.Add(AFTIP);
            parametros.Add(AFDEP);
            parametros.Add(AFMAR);
            parametros.Add(AFMOD);
            //parametros.Add(AFSER);
            parametros.Add(AFUNIASIG);
            parametros.Add(AFEXI);
            parametros.Add(AFCOD);
            parametros.Add(AFUNIDAD);
            parametros.Add(AFFIS);
            parametros.Add(AFMONTO);
            //parametros.Add(AFCODOLD);
            //parametros.Add(AFNOMOLD);
            parametros.Add(AFCATESPECCOD);
            //parametros.Add(USUARIO);
            //parametros.Add(FECHA);
            cn.Insert2(tabla, parametros, true, CAMPOS());
            return cn.afectados;

        }

        public int getParametersUpd()
        {
            Models.ConectDB cn = new Models.ConectDB();

            ArrayList parametros = new ArrayList(39);

            //AFCODOLD = "0";
            //AFNOMOLD = "0";



            parametros.Add("AFNOM=" + "'" + AFNOM + "'");
            parametros.Add("AFDES=" + "'" + AFDES + "'");
            parametros.Add("AFREF=" + "'" + AFREF + "'");
            parametros.Add("AFGRU=" + "'" + AFGRU + "'");
            parametros.Add("AFTIP=" + "'" + AFTIP + "'");
            parametros.Add("AFDEP=" + "'" + AFDEP + "'");
            parametros.Add("AFMAR=" + "'" + AFMAR + "'");
            parametros.Add("AFMOD=" + "'" + AFMOD + "'");
            parametros.Add("AFSER=" + "'" + AFSER + "'");
            parametros.Add("AFUNIASIG=" + "'" + AFUNIASIG + "'");
            parametros.Add("AFEXI=" + "'" + AFUNIASIG + "'");
            parametros.Add("AFUNIDAD=" + "'" + AFFIS + "'");
            parametros.Add("AFFIS=" + "'" + AFMONTO + "'");
            parametros.Add("AFMONTO=" + "'" + AFCATESPECCOD + "'");
            //parametros.Ad"AFCODOLD="+"'"+d(USUARIO);
            //parametros.Ad"AFNOMOLD="+"'"+d(FECHA);
            //parametros.Ad"AFCATESPECCOD="+"'"+d(AFSER);
            //parametros.Add(AFCODOLD);
            //parametros.Add(AFNOMOLD);
            cn.Update2(tabla, parametros, whereParam(), true);
            return cn.afectados;

        }


        protected override string CAMPOS()
        {
            string CAMPOS = "AFNOM,AFDES,AFREF,AFGRU,AFTIP,AFDEP,AFMAR,AFMOD,AFUNIASIG,AFEXI,AFCOD,AFUNIDAD,AFFIS,AFMONTO,AFCATESPECCOD";
            return CAMPOS;
        }
        protected override object whereParam()
        {
            string where = "AFCOD='" + AFCOD + "'" + "";


            return where;
        }

    }
}


/////////// CAMPOS DE LA TABLA /////////////
//    AFNOM 
//    AFDES 
//    AFREF 
//    AFGRU 
//    AFTIP 
//    AFDEP 
//    AFMAR 
//    AFMOD 
//    AFSER              
//    AFUNIASIG 
//    AFEXI 
//    AFCOD  
//    AFUNIDAD 
//    AFFIS 
//    AFMONTO     
//    AFCODOLD 
//    AFNOMOLD 
//    AFCATESPECCOD 




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_AF_MAESTRO
//SP_Q_AUTCOMAFMA
//SP_Q_AUTCOMTRANS
//SP_INS_AF_MAESTRO
//SP_DEL_AF_MAESTRO
//SP_UPD_AF_MAESTRO
//SP_Q_AF_MAESTRO   



