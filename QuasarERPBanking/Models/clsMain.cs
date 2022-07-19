using System;
using System.Collections;
//using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Models
{
    public class clsMain
    {
        public string sp_Insert { get; set; }

        public static string Mensaje(int codigo)
        {
            string valor = "";
            switch (codigo)
            {
                case 0:
                    valor = StringResources.msgInsertFAIL;
                    break;
                case 1:
                    valor = StringResources.msgInsertOK;
                    break;
                case 2:
                    valor = StringResources.msgInsertEXIST;
                    break;

            }

            return valor;
        }
        public string sp_Update { get; set; }
        public string sp_Delete { get; set; }
        public string sp_Find { get; set; }
        public virtual string Campo_Clave { get; set; }

        public virtual string Campo_Clave2 { get; set; }


        public string sp_Exist { get; set; }
       
        public string tabla { get; set; }
        public string tabla2 { get; set; }
        public string tabla3 { get; set; }



        public clsMain()
        {
            sp_Insert = "SP_INS_" + this.GetType().Name;
            sp_Update = "SP_UPD_" + this.GetType().Name;
            sp_Delete = "SP_DEL_" + this.GetType().Name;
            sp_Find = "SP_Q_" + this.GetType().Name;
          
        }
        public int Insert()
        {
            clsAS400 cn = new clsAS400();
            cn.execProc(sp_Insert, getParameters(), true);
            return cn.afectados;

        }

        public int Update()
        {
            clsAS400 cn = new clsAS400();
            cn.execProc(sp_Update, getParameters(), true);
            return cn.afectados;

        }

        public int Update2()
        {
            clsAS400 cn = new clsAS400();
            cn.Update2(tabla, getParametersUpd(),whereParam(), true);
            return cn.afectados;

        }

        public int execute()
        {
            clsAS400 cn = new clsAS400();
            cn.execute(tabla, getParameters(), true);
            return cn.afectados;

        }

        //public int execute2()
        //{
        //    clsAS400 cn = new clsAS400();
        //    cn.execute2(tabla,  whereParam(), true);
        //    return cn.afectados;

        //}

        public int Insert2()
        {
            clsAS400 cn = new clsAS400();
            cn.Insert2(tabla, getParameters(), true, CAMPOS());
            return cn.afectados;

        }

        public int Delete()
        {
            clsAS400 cn = new clsAS400();
            cn.execProc(sp_Delete, getClave(), true);
            return cn.afectados;

        }

        protected virtual ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }

        protected virtual ArrayList getParametersUpd()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }

        protected virtual object whereParam()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }

        protected virtual string CAMPOS()
        {
            string CAMPOS = "";
            return CAMPOS;
        }

        protected virtual List<clsMain> CAMPOSUPD(ArrayList getParameters)
        {
            List<clsMain> paramaUpd = new List<clsMain>();
            return paramaUpd;
        }
        //Buscar y Existe para veriricar existencia
        public bool Existe()
        {
            bool exists = false;
            Models.clsAS400 cn = new Models.clsAS400();
            Rows reader = cn.execProc(this.sp_Find, getClave(), false);
            if (reader != null)
            {
                if (reader.Count > 0)
                {
                    exists = true;
                    //objeto = LoadObject(reader);
                }

            }
            return exists;
        }




        //Buscar y LoadObject para buscar
        public clsMain Buscar()
        {
            clsAS400 cn = new clsAS400();
            //ArrayList arreglo = new ArrayList(1);
            //arreglo.Add(value);

            Rows reader = cn.execProc(sp_Find, getClave(), false);
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }

        public clsMain BuscarOrig(string codigo)
        {

            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            OleDbCommand comando = new OleDbCommand("Select * from "+ParametrosGlobales.bd +"." + tabla + " where " + Campo_Clave + " ='" + codigo + "' ", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject54(reader);
            }
            return objeto;
        }

        public clsMain BuscarOrig2(string[] codigo)
        {
            string codigo1 = codigo[0];
            string codigo2 = codigo[1];
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            //OleDbCommand comando = new OleDbCommand("Select * from " + ParametrosGlobales.bd + "." + tabla + " where " + Campo_Clave + " ='" + codigo1 + "' ", cn);
            // SELECT COTSTATUS, A.COTPROVCOD ,A.COTPROVCONTACTO, A.COTSUBTOTAL , A.COTIVA ,  A.COTTOTAL , A.COTLUGAR , A.COTPLAZO ,  A.COTFPAGO ,  A.COTGARANTIA  , A.COTOBSERV  , A.COTELAFECHA ,  A.COTELAPERSONA,  A.COTTABLA ,  A.COTCODELAB ,  A.COTSOLIC, A.COTSOLICD ,  A.COTNPR ,  A.COTMONEDA ,  A.COTVP , A.COTPROY , A.COTCC ,B.CXPNOMBRE ,  B.CXPDIR , B.CXPRIF , B.CXPTELF , B.CXPFAX  ,C.CXPCONTNOM FROM " + ParametrosGlobales.bd + "COT_MAESTRO A INNER JOIN " + ParametrosGlobales.bd + "CXP_MAESTRO B ON A.COTPROVCOD = B.CXPCOD INNER JOIN " + ParametrosGlobales.bd + "CXP_CONTACTOS C ON B.CXPCOD = C.CXPCOD WHERE A.COTPROVCOD = '6133' AND A.COTCODELAB =

            OleDbCommand comando = new OleDbCommand("SELECT COTSTATUS, A.COTPROVCOD ,A.COTPROVCONTACTO, A.COTSUBTOTAL , A.COTIVA ,  A.COTTOTAL , A.COTLUGAR , A.COTPLAZO ,  A.COTFPAGO ,  A.COTGARANTIA  , A.COTOBSERV  , A.COTELAFECHA ,  A.COTELAPERSONA,  A.COTTABLA ,  A.COTCODELAB ,  A.COTSOLIC, A.COTSOLICD ,  A.COTNPR ,  A.COTMONEDA ,  A.COTVP , A.COTPROY , A.COTCC ,B.CXPNOMBRE ,  B.CXPDIR , B.CXPRIF , B.CXPTELF , B.CXPFAX  ,C.CXPCONTNOM FROM " + ParametrosGlobales.bd + "COT_MAESTRO A INNER JOIN " + ParametrosGlobales.bd + "CXP_MAESTRO B ON A.COTPROVCOD = B.CXPCOD INNER JOIN " + ParametrosGlobales.bd + "CXP_CONTACTOS C ON B.CXPCOD = C.CXPCOD WHERE A.COTCODELAB ='" + codigo1+ "' AND A.COTPROVCOD = '" + codigo2 + "'", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject54(reader);
            }
           
          
            return objeto;
        }


        protected virtual clsMain LoadObject54(OleDbDataReader reader)
        {
            while (reader.Read())
            {

            }
            return this;

        }



        public clsMain Buscar(string value)
        {
            clsAS400 cn = new clsAS400();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execProc(sp_Find, arreglo, false);
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }

        public clsMain Buscar(string[] value)
        {
            clsAS400 cn = new clsAS400();
            ArrayList arreglo = new ArrayList(1);
            foreach (string value2 in value)
            {
                arreglo.Add(value2);
            }
            Rows reader = cn.execProc(sp_Find, arreglo, false);
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }

        public clsMain cargar(string consulta)
        {
            ConectDB cn = new ConectDB();   
            Rows dt = cn.BaseDatos(consulta,  false);
            clsMain objeto = null;

            if (dt != null)
            {
                //objeto = CargarObjeto(dt);
            }
            return objeto;




        }


        public clsMain filtrobusqueda(string cod, string value)
        {
            clsAS400 cn = new clsAS400();
            ArrayList arreglo = new ArrayList(2);
            arreglo.Add(cod);
            arreglo.Add(value);
            Rows reader = cn.execProc(sp_Find, arreglo, false);
            clsMain objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }


        ////para multiples etiquetas
        //public clsMain Buscar_(string value)
        //{
        //    clsAS400 cn = new clsAS400();
        //    ArrayList arreglo = new ArrayList(1);
        //    arreglo.Add(value);
        //    Rows reader = cn.execProc(sp_Find, arreglo, false);
        //    clsMain objeto = null;
        //    if (reader != null)
        //    {
        //        objeto = LoadObject(reader);
        //    }
        //    return objeto;
        //}



        protected virtual clsMain LoadObject(Rows reader)
        {
            foreach (Row item in reader)
            {

            }
            return this;

        }

        protected virtual clsMain CargarObjeto(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {

            }
            return this;

        }



        protected virtual clsMain LoadObject3(Rows listafinal)
        {
            foreach (Row item in listafinal)
            {

            }
            return this;

        }


        protected virtual clsMain LoadObject2(Rows reader)
        {
            foreach (Row item in reader)
            {

            }
            return this;

        }

        //getParameters main para create y update 

        //getClave main para delete 
        protected ArrayList getClave()
        {
            ArrayList arreglo = new ArrayList(1);

            arreglo.Add(this.GetType().GetProperty(Campo_Clave).GetValue(this));
            return arreglo;
        }

        public string Edit()
        {
            string message = string.Empty;
            if (this.Existe())
            {

                if (this.Update() == -1)
                {
                    message = StringResources.msgEditOK;
                }
            }
            else
            {
                message = StringResources.msgEditFAIL;

            }


            return message;
        }

        public string Create()
        {
            string message = string.Empty;
            if (!this.Existe())
            {

                switch (this.Insert())
                {
                    case -2:
                        message = StringResources.msgInsertFAIL;
                        break;
                    case -1:
                        message = StringResources.msgInsertOK;
                        break;
                    default:
                        message = "";
                        break;
                }

            }
            else
            {
                message = StringResources.msgInsertEXIST;

            }
            return message;
        }

        public string Eliminar()
        {
            string message = string.Empty;
            int result = this.Delete();
            if (result != 0)
            {
                if (result == -2)
                {
                    message = StringResources.msgDeleteFAIL;

                }
                else
                {
                    message = StringResources.msgDeleteOK;
                }

            }
            else
            {

            }
            return message;
        }


        //public IEnumerable<CXP_ORDENPAGO> Buscar3(string codigo)
        //{
        //    string servidor = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
        //    string bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];
        //    string usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
        //    string clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];
        //    string _provider = "IBMDA400.DataSource.1";
        //    string strConnection = "Provider=" + _provider + ";" +
        //                    "Data source=" + servidor + ";" +
        //                    "User Id=" + usuario + ";" +
        //                    "Password=" + clave + ";";
        //    List<CXP_ORDENPAGO> listafinal = new List<CXP_ORDENPAGO>();
        //    DataSet lista = new DataSet();
        //    OleDbConnection cnx = new OleDbConnection(strConnection);
        //    string consulta = "";


        //    consulta = "Select " + ParametrosGlobales.bd + "cxp_transacciones.cxpcod, " + ParametrosGlobales.bd + "cxp_maestro.cxpnombre, " + ParametrosGlobales.bd + "cxp_transacciones.tpodoc, " + ParametrosGlobales.bd + "cxp_transacciones.ndoc, " + ParametrosGlobales.bd + "cxp_transacciones.conc, " + ParametrosGlobales.bd + "cxp_transacciones.fechdoc, " + ParametrosGlobales.bd + "cxp_transacciones.fechvenc, " + ParametrosGlobales.bd + "cxp_transacciones.mtoimp, " + ParametrosGlobales.bd + "cxp_transacciones.total, " + ParametrosGlobales.bd + "cxp_transacciones.pagar, " + ParametrosGlobales.bd + "cxp_transacciones.ref from " + ParametrosGlobales.bd + "cxp_transacciones inner join " + ParametrosGlobales.bd + "cxp_maestro on " + ParametrosGlobales.bd + "cxp_transacciones.cxpcod=" + ParametrosGlobales.bd + "cxp_maestro.cxpcod where  " + ParametrosGlobales.bd + "cxp_transacciones.cxpcod='" + codigo + "' ";


        //    OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cnx);
        //    DA.Fill(lista);
        //    DataTable dt = lista.Tables[0];

        //    if (dt.Rows != null)
        //    {
        //        foreach (DataRow linea in dt.Rows)

        //        {
        //            CXP_ORDENPAGO listas = new CXP_ORDENPAGO();
        //            listas.NDOC = linea["NDOC"].ToString();
        //            listas.FECHDOC = DateTime.Parse(linea["FECHDOC"].ToString());
        //            listas.CONC = linea["CONC"].ToString();
        //            listas.TOTAL = decimal.Parse(linea["TOTAL"].ToString());
        //            listas.MTOIMP = decimal.Parse(linea["MTOIMP"].ToString());
        //            listas.NDOC = linea["TPODOC"].ToString();
        //            listas.REF = linea["REF"].ToString();

        //            listafinal.Add(listas);
        //        }
        //    }

        //    return listafinal;

        //}





      

    }
}














































