using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.CC_MAESTRO;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;


namespace QuasarERPBanking.Models
{
    public class CentroDistriEstructural : ConectDB 
    {
        [Display(Name = "lblDes", ResourceType = typeof(StringResources))]
        public string CC_NOMB { get; set; }

        [Display(Name = "lblCodPertEs", ResourceType = typeof(StringResources))]
        public string CC_PERT_ESTRUC { get; set; }

        [Display(Name = "lblCodPert", ResourceType = typeof(StringResources))]
        public string CC_PERT_GEOG { get; set; }

        public double CC_SALDO { get; set; }

        [Display(Name = "lblCodPertCli", ResourceType = typeof(StringResources))]
        public string CC_CTEASOC { get; set; }
        public double CC_PRPROG_1 { get; set; }
        public double CC_PRPROG_2 { get; set; }
        public double CC_PRPROG_3 { get; set; }
        public double CC_PREJEC_1 { get; set; }
        public double CC_PREJEC_2 { get; set; }
        public double CC_PREJEC_3 { get; set; }

        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        public string CC_COD { get; set; }
        public string CC_ULTHIJO { get; set; }

        [Display(Name = "lblDesPert", ResourceType = typeof(StringResources))]
        public string CC_DES_ESTRUC { get; set; }
        [Display(Name = "lblDesPert", ResourceType = typeof(StringResources))]
        public string CC_DES_GEOGR { get; set; }
        [Display(Name = "lblDesPert", ResourceType = typeof(StringResources))]
        public string CC_DES_CTEASOC { get; set; }
        public string CODEDIT { get; set; }




        static string sp_getAll = "SP_ALL_CC_MAESTRO";

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<CentroDistriEstructural> GetCentroCosto()
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> estados = new List<CentroDistriEstructural>();
            string consulta = "SELECT  CC_NOMB ,  CC_COD ,  CC_ULTHIJO from "+ParametrosGlobales.bd +" CC_MAESTRO ORDER BY CC_NOMB ASC";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    CentroDistriEstructural ccosto = new CentroDistriEstructural();


                    ccosto.CC_COD = item["CC_COD"].ToString();
                    ccosto.CC_NOMB = item["CC_NOMB"].ToString();
                    estados.Add(ccosto);

                }
            }

            return estados;
        }

        //////////////public CC_MAESTRO()
        //////////////{
        //////////////    sp_Find = "SP_Q_CCM_EXIS";
        //////////////    Campo_Clave = "CC_COD";
        //////////////}

        //llenar la lista completa de centro de costos para el mantenimiento
        public static List<CentroDistriEstructural> GetCCosto()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> lstCcosto = new List<CentroDistriEstructural>();
            string consulta = "SELECT CC_NOMB , CC_COD , CC_ULTHIJO FROM " + ParametrosGlobales.bd + "CC_MAESTRO ORDER BY CC_NOMB ASC";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    lstCcosto.Add(setCcosto(item));
                }
            }
            return lstCcosto;
        }

        static CentroDistriEstructural setCcosto(Row item)
        {
            CentroDistriEstructural ccosto = new CentroDistriEstructural
            {
                CC_NOMB = item["CC_NOMB"].ToString(),
                CC_COD = item["CC_COD"].ToString(),
                CC_ULTHIJO = item["CC_ULTHIJO"].ToString(),

            };
            return ccosto;
        }

        static CentroDistriEstructural setCcosto(DataRow item)
        {
            CentroDistriEstructural ccosto = new CentroDistriEstructural
            {
                CC_NOMB = item["CC_NOMB"].ToString(),
                CC_COD = item["CC_COD"].ToString(),
                CC_ULTHIJO = item["CC_ULTHIJO"].ToString(),

            };
            return ccosto;
        }

        //cargar los datos de un centro de costo
        //protected override clsMain LoadObject(Rows reader)
        //{

        //    foreach (Row item in reader)
        //    {
        //        CC_NOMB = item["CC_NOMB"].ToString();
        //        CC_COD = item["CC_COD"].ToString();
        //        CC_ULTHIJO = item["CC_ULTHIJO"].ToString();

        //    }

        //    return this;
        //}

        protected  CentroDistriEstructural  LoadObject(DataSet reader)
        {

            foreach (DataRow item in reader.Tables[0].Rows )
            {
                CC_NOMB = item["CC_NOMB"].ToString();
                CC_COD = item["CC_COD"].ToString();
                CC_ULTHIJO = item["CC_ULTHIJO"].ToString();

            }

            return this;
        }

        ////////////protected override ArrayList getParameters()
        ////////////{

        ////////////    CC_ULTHIJO = "1";
        ////////////    ArrayList parametros = new ArrayList(6);
        ////////////    parametros.Add(CC_NOMB);
        ////////////    parametros.Add(CC_PERT_ESTRUC);
        ////////////    parametros.Add(CC_PERT_GEOG);
        ////////////    parametros.Add(CC_CTEASOC);
        ////////////    parametros.Add(CC_COD);
        ////////////    //parametros.Add(CC_COD ==null? CODEDIT: CC_COD);
        ////////////    parametros.Add(CC_ULTHIJO);
        ////////////    return parametros;
        ////////////}

        //DROPDOWNLIST
        public static IEnumerable<SelectListItem> GetCC()
        {
            List<SelectListItem> lstCCostos = new List<SelectListItem>(CentroDistriEstructural.GetCCosto().Count);
            foreach (CentroDistriEstructural item in CentroDistriEstructural.GetCCosto())
            {

                lstCCostos.Add(new SelectListItem
                {
                    Value = item.CC_COD,
                    Text = item.CC_COD + "-" + item.CC_NOMB.ToUpper()
                }
                );

            }
            return new SelectList(lstCCostos, "Value", "Text");
        }


        #region para el dropdownlist de nota de entrega
        public static List<CentroDistriEstructural> CCostoXCtesInt(string ctecod)

        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> proveedores = new List<CentroDistriEstructural>();
            string consulta = "SELECT CC_CTEASOC , CC_NOMB FROM " + ParametrosGlobales.bd + " CC_MAESTRO WHERE CC_CTEASOC = '" + ctecod + "' ";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriEstructural proveedor = new Models.CentroDistriEstructural();
                    proveedor.CC_CTEASOC = item["CC_CTEASOC"].ToString();
                    proveedor.CC_NOMB = item["CC_NOMB"].ToString();

                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }
        #endregion

        public List<string> GetCCostos(string codigo)
        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<string> proveedores = new List<string>();
            string consulta = "SELECT CONCAT ( CONCAT ( CC_COD , ' - ' ) , CC_NOMB ) AS CODIGO FROM " + ParametrosGlobales.bd + "CC_MAESTRO WHERE ( UPPER ( CC_NOMB ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) OR ( UPPER ( CC_COD ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) )";
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
            string consulta = "SELECT  *  FROM " + ParametrosGlobales.bd + "CC_MAESTRO WHERE CC_COD = '" + codigo + "'";
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



        public static List<CentroDistriEstructural> getpadres()

        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> proveedores = new List<CentroDistriEstructural>();
            string consulta = "SELECT CC_NOMB , CC_PERT_ESTRUC , CC_PERT_GEOG , CC_CTEASOC , CC_COD , CC_ULTHIJO FROM " + ParametrosGlobales.bd + "CC_MAESTRO WHERE CC_PERT_ESTRUC = ''";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriEstructural proveedor = new Models.CentroDistriEstructural();

                    proveedor.CC_NOMB = item["CC_NOMB"].ToString();
                    proveedor.CC_PERT_ESTRUC = item["CC_PERT_ESTRUC"].ToString();
                    proveedor.CC_PERT_GEOG = item["CC_PERT_GEOG"].ToString();
                    proveedor.CC_CTEASOC = item["CC_CTEASOC"].ToString();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_ULTHIJO = item["CC_ULTHIJO"].ToString();

                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }

        public static List<CentroDistriEstructural> gethijos(string cod)

        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> proveedores = new List<CentroDistriEstructural>();
            string consulta = "SELECT CC_NOMB , CC_PERT_ESTRUC , CC_PERT_GEOG , CC_CTEASOC , CC_COD , CC_ULTHIJO FROM " + ParametrosGlobales.bd + "CC_MAESTRO WHERE CC_PERT_ESTRUC = '" + cod + "' ORDER BY CC_PERT_ESTRUC , CC_NOMB";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriEstructural proveedor = new Models.CentroDistriEstructural();

                    proveedor.CC_NOMB = item["CC_NOMB"].ToString();
                    proveedor.CC_PERT_ESTRUC = item["CC_PERT_ESTRUC"].ToString();
                    proveedor.CC_PERT_GEOG = item["CC_PERT_GEOG"].ToString();
                    proveedor.CC_CTEASOC = item["CC_CTEASOC"].ToString();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_ULTHIJO = item["CC_ULTHIJO"].ToString();

                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }

        public static List<CentroDistriEstructural> getree(string cod)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> proveedores = new List<CentroDistriEstructural>();
            string consulta = "SELECT CC_NOMB , CC_PERT_ESTRUC , CC_PERT_GEOG , CC_CTEASOC , CC_COD , CC_ULTHIJO FROM " + ParametrosGlobales.bd + "CC_MAESTRO WHERE CC_PERT_ESTRUC <> '" + cod+"' AND CC_PERT_ESTRUC <> '' ORDER BY CC_COD , CC_NOMB";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.CentroDistriEstructural proveedor = new Models.CentroDistriEstructural();

                    proveedor.CC_NOMB = item["CC_NOMB"].ToString();
                    proveedor.CC_PERT_ESTRUC = item["CC_PERT_ESTRUC"].ToString();
                    proveedor.CC_PERT_GEOG = item["CC_PERT_GEOG"].ToString();
                    proveedor.CC_CTEASOC = item["CC_CTEASOC"].ToString();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_ULTHIJO = item["CC_ULTHIJO"].ToString();

                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }



        public static List<CentroDistriEstructural> getDescri(string cod)

        {

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> proveedores = new List<CentroDistriEstructural>();
            string consulta = "SELECT A . CC_NOMB , A . CC_PERT_ESTRUC , A . CC_PERT_GEOG , A . CC_CTEASOC , A . CC_COD , B . CC_NOMB AS PERTENECE , C . CC_DES , D . CTENOMBRE FROM " + ParametrosGlobales.bd + "CC_MAESTRO A INNER JOIN " + ParametrosGlobales.bd + "CC_MAESTRO B ON A . CC_PERT_ESTRUC = B . CC_COD INNER JOIN " + ParametrosGlobales.bd + "CC_DISTGEOG C ON A . CC_PERT_GEOG = C . CC_COD INNER JOIN " + ParametrosGlobales.bd + "CC_CTESINT D ON A . CC_CTEASOC = D . CTECOD WHERE A . CC_COD = '" + cod + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {

                    Models.CentroDistriEstructural proveedor = new Models.CentroDistriEstructural();



                    proveedor.CC_NOMB = item["CC_NOMB"].ToString();
                    proveedor.CC_PERT_ESTRUC = item["CC_PERT_ESTRUC"].ToString();
                    proveedor.CC_PERT_GEOG = item["CC_PERT_GEOG"].ToString();
                    proveedor.CC_CTEASOC = item["CC_CTEASOC"].ToString();
                    proveedor.CC_COD = item["CC_COD"].ToString();
                    proveedor.CC_DES_ESTRUC = item["PERTENECE"].ToString();
                    proveedor.CC_DES_GEOGR = item["CC_DES"].ToString();
                    proveedor.CC_DES_CTEASOC = item["CTENOMBRE"].ToString();


                    proveedores.Add(proveedor);

                }
            }

            return proveedores;
        }



        public static List<CentroDistriEstructural> ListHijos(string cod)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<CentroDistriEstructural> lista = new List<CentroDistriEstructural>();
            string consulta = "SELECT A . CC_NOMB , A . CC_PERT_ESTRUC , A . CC_PERT_GEOG , A . CC_CTEASOC , A . CC_COD , D . CTENOMBRE , B . CC_NOMB AS PERTENECE , C . CC_DES FROM " + ParametrosGlobales.bd + "CC_MAESTRO A INNER JOIN " + ParametrosGlobales.bd + "CC_MAESTRO B ON A.CC_PERT_ESTRUC = B.CC_COD INNER JOIN " + ParametrosGlobales.bd + "CC_DISTGEOG C ON  A.CC_PERT_GEOG = C.CC_COD INNER JOIN " + ParametrosGlobales.bd + "CC_CTESINT D ON A.CC_CTEASOC = D.CTECOD WHERE A .  CC_PERT_ESTRUC = '" + cod + "' ORDER BY A.CC_NOMB";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CentroDistriEstructural listas = new CentroDistriEstructural
                        {

                            CC_NOMB = item["CC_NOMB"].ToString(),
                            CC_PERT_ESTRUC = item["CC_PERT_ESTRUC"].ToString(),
                            CC_PERT_GEOG = item["CC_PERT_GEOG"].ToString(),
                            CC_CTEASOC = item["CC_CTEASOC"].ToString(),
                            CC_COD = item["CC_COD"].ToString(),
                            CC_DES_ESTRUC = item["PERTENECE"].ToString(),
                            CC_DES_GEOGR = item["CC_DES"].ToString(),
                            CC_DES_CTEASOC = item["CTENOMBRE"].ToString(),

                        };
                        lista.Add(listas);
                    }
                }
            }
            return lista;
        }


    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_CC_MAESTRO
//SP_Q_CCM_EXIS
//SP_Q_CCOSTOXCTES
//SP_Q_AUTOCMAESTRO
//SP_Q_CCMAESTRO
//SP_Q_CCMAESHIJ
//SP_Q_CCMAETREE
//SP_Q_CC_MAESTRO
//SP_Q_CCMAES_GRID

