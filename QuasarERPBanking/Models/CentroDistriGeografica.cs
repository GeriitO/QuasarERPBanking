using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.CC_MAESTRO;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class CentroDistriGeografica : ConectDB
    {
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        public string CC_COD { get; set; }

        [Display(Name = "lblDes", ResourceType = typeof(StringResources))]
        public string CC_DES { get; set; }

        [Display(Name = "lblCodPert", ResourceType = typeof(StringResources))]
        public string CC_PERT_GEO { get; set; }

        [Display(Name = "lblDesPert", ResourceType = typeof(StringResources))]
        public string CC_DES_PERT_GEO { get; set; }
        public string CODEDIT { get; set; }

        //static string sp_get = "";   //AUTOCOMPLETE
        //static string sp_getAll = "";  //DROPDOWN



        #region Constructor
        //public CC_DISTGEOG()
        //{

        //    Campo_Clave = "CC_COD";
        //}
        #endregion

        //protected override ArrayList getParameters()
        //{
        //    ArrayList parametros = new ArrayList(3);
        //    parametros.Add(CC_COD);
        //    parametros.Add(CC_DES);
        //    parametros.Add(CC_PERT_GEO);
        //    return parametros;
        //}



        protected override ConectDB CargarObjeto(DataTable  reader)
        {
            foreach (DataRow item in reader.Rows)
            {
                CC_COD = item["CC_COD"].ToString();
                CC_DES = item["CC_DES"].ToString();
                CC_PERT_GEO = item["CC_PERT_GEO"].ToString();
            }
            return this;
        }



        public static List<CentroDistriGeografica> getpadres()

        {
            string strConnection = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            List<CentroDistriGeografica> proveedores = new List<CentroDistriGeografica>();
            DataSet lista = new DataSet();
            OleDbConnection cnx = new OleDbConnection(strConnection);
            string consulta = "";

            consulta = "SELECT * FROM " + ParametrosGlobales.bd +  "CC_DISTGEOG WHERE CC_PERT_GEO = ''";

            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cnx);
            DA.Fill(lista);
            DataTable dt = lista.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriGeografica proveedor = new Models.CentroDistriGeografica();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_DES = item["CC_DES"].ToString();
                    proveedor.CC_PERT_GEO = item["CC_PERT_GEO"].ToString();

                    proveedores.Add(proveedor);
                }
            }
            return proveedores;
        }


        public static List<CentroDistriGeografica> gethijos(string cod)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(cod);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriGeografica> proveedores = new List<CentroDistriGeografica>();
            string consulta = "SELECT * FROM " + ParametrosGlobales.bd + "CC_DISTGEOG WHERE CC_PERT_GEO ='" + cod + "' ORDER BY CC_DES  ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    Models.CentroDistriGeografica proveedor = new Models.CentroDistriGeografica();

                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_DES = item["CC_DES"].ToString();
                    proveedor.CC_PERT_GEO = item["CC_PERT_GEO"].ToString();

                    proveedores.Add(proveedor);
                }
            }
            return proveedores;
        }

        public static List<CentroDistriGeografica> getree(string cod)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(cod);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriGeografica> proveedores = new List<CentroDistriGeografica>();
            string consulta = "SELECT * FROM " + ParametrosGlobales.bd + "CC_DISTGEOG WHERE CC_PERT_GEO != '" + cod + "' AND CC_PERT_GEO != '' ORDER BY CC_PERT_GEO , CC_DES";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    Models.CentroDistriGeografica proveedor = new Models.CentroDistriGeografica();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_DES = item["CC_DES"].ToString();
                    proveedor.CC_PERT_GEO = item["CC_PERT_GEO"].ToString();

                    proveedores.Add(proveedor);
                }
            }
            return proveedores;
        }


        ////SE UTILIZA DROPDOWMLIST 

        //public IEnumerable<CC_CTESINT> GetDropdownlist()
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
        //    Rows reader = cn.execProc(sp_getAll, parametros, false);
        //    List<CC_CTESINT> clientes = new List<CC_CTESINT>();

        //    if (reader != null)
        //    {
        //        foreach (Dictionary<string, object> item in reader)
        //        {
        //            Models.CC_CTESINT cliente = new Models.CC_CTESINT();
        //            cliente.CTECOD = item["CTECOD"].ToString();
        //            cliente.CTENOMBRE = item["CTENOMBRE"].ToString();

        //            clientes.Add(cliente);
        //        }
        //    }

        //    return clientes;
        //}


        //// PARA EL AUTOCOMPLETE
        //#region Autocomplete
        //public List<string> GetAutocClientes(string codigo)
        //{
        //    clsAS400 cn = new clsAS400();
        //    System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
        //    parametros.Add(codigo);

        //    Rows reader = cn.execProc(sp_get, parametros, false);
        //    List<string> clientes = new List<string>();

        //    if (reader != null)
        //    {
        //        foreach (Row item in reader)
        //        {
        //            string cliente = "";
        //            cliente = item["NOMBRE"].ToString();  // ESTE ES EL ALIAS DEL SP                 
        //            clientes.Add(cliente);
        //        }
        //    }

        //    return clientes;
        //}
        //#endregion


        public static List<CentroDistriGeografica> ListHijos(string cod)
        {


            ArrayList parametros = new ArrayList(1);
            parametros.Add(cod);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriGeografica> lista = new List<CentroDistriGeografica>();
            string consulta = "SELECT * FROM " + ParametrosGlobales.bd + "CC_DISTGEOG WHERE CC_PERT_GEO = '" + cod + "' ORDER BY CC_DES";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CentroDistriGeografica listas = new CentroDistriGeografica
                        {
                            CC_COD = item["CC_COD"].ToString(),
                            CC_DES = item["CC_DES"].ToString(),
                            CC_PERT_GEO = item["CC_PERT_GEO"].ToString(),
                        };
                        lista.Add(listas);
                    }
                }
            }
            return lista;
        }



        public static List<CentroDistriGeografica> getDescri(string cod)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(cod);
            //Rows reader = cn.execProc("SP_Q_AUTOGEO", parametros, false);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriGeografica> proveedores = new List<CentroDistriGeografica>();
            string consulta = "SELECT A . * , B . CC_DES AS DESPERT FROM " + ParametrosGlobales.bd + "CC_DISTGEOG A INNER JOIN " + ParametrosGlobales.bd + "CC_DISTGEOG B ON B . CC_COD = A . CC_PERT_GEO WHERE A . CC_COD = '" + cod + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriGeografica proveedor = new Models.CentroDistriGeografica();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_DES = item["CC_DES"].ToString();
                    proveedor.CC_PERT_GEO = item["CC_PERT_GEO"].ToString();
                    proveedor.CC_DES_PERT_GEO = item["DESPERT"].ToString();
                    proveedores.Add(proveedor);
                }
            }
            return proveedores;
        }


        public List<string> GetDistGeo(string codigo)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> proveedores = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( CC_COD , ' _ ' ) , CC_DES ) AS CODIGO FROM " + ParametrosGlobales.bd + "CC_DISTGEOG WHERE(UPPER(CC_DES) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) OR(UPPER(CC_COD) LIKE CONCAT(CONCAT('%', UPPER('" + codigo + "')), '%')) ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)

                {
                    string proveedor = "";
                    proveedor = item["CODIGO"].ToString();
                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }


        public string GetExist(string codigo)
        {
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string existencia = "";
            string consulta = "SELECT * FROM " + ParametrosGlobales.bd + "CC_DISTGEOG WHERE CC_COD = '" + codigo + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            if (dt.Rows != null)
            {
                if (dt.Rows.Count != 0)
                {
                    existencia = "1";
                }
                else
                {
                    existencia = "0";
                }
            }


            return existencia;
        }





        public string GetExist2(string codigo)  // <------------Revisar el codigo
        {
            string existencia;
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);
            Rows reader = cn.execProc("SP_Q_DEL_DISTGEOG", parametros, false);
            if (reader.Count != 0)
            {
                existencia = "1";
            }
            else
            {
                existencia = "0";
            }
            return existencia;
        }


        public string DelUtiliza(string codigo)  // <------------Revisar el codigo
        {
            string existencia = "0";
            clsAS400 cn = new clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(codigo);
            ArrayList parametros2 = new ArrayList(1);
            Rows reader = cn.execProc("SP_Q_DEL_CCTRANS", parametros, false);

            if (reader != null)
            {
                if (reader.Count > 0)
                {
                    foreach (Row item in reader)
                    {
                        string proveedor = "";
                        proveedor = item["CC_COD"].ToString();
                        parametros2.Add(proveedor);
                        Rows reader2 = cn.execProc("SP_Q_CC_TRANS", parametros2, false);
                        if (reader2.Count > 0)//con un solo registro que traiga ya no se puede eliminar
                        {
                            existencia = "1";
                            return existencia;
                        }
                    }
                }
            }
            return existencia;
        }

    }
}