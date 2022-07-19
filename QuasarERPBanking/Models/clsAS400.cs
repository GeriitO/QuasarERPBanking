using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Collections;
using Row = System.Collections.Generic.Dictionary<string, object>;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class clsAS400
    {
        public static OleDbConnection CnAS400 = null;
        public OleDbCommand CmdAS400;
        //protected string _Respuesta;
        private static bool flgConectado = false;
        private static string DataSrc = "10.112.2.11";
        public static bool isDisconnected;
        public static string esquema = "BICADM01W";
        //public trans  OleDbTransaction
        public static string _provider = "IBMDA400.DataSource.1";
        //protected string camposProducto = "";
        //protected string camposDecision = "";
        public static string Usuario = "QUASAR00";
        public static string Clave = "CARACAS2";
        public object codigo;
        public string datasource = "";
        public int afectados { get; set; }

        public static OleDbConnection Conexion
        {
            get { return CnAS400; }
        }
        //public string Respuesta
        //{
        //    get { return _Respuesta; }
        //    //set {   MsgErr("Propiedad de Sólo Lectura"); }
        //}
        public static bool Connect()
        {
            string strConnection;
            bool wFlg = false;
            strConnection = "Provider=" + _provider + ";" +
            "Data source=" + DataSrc + ";" +
            "User Id=" + Usuario + ";" +
            "Password=" + Clave + ";";
            //strConnection = "Provider=DB2OLEDB;"
            CnAS400 = new OleDbConnection(strConnection);
            try
            {
                CnAS400.Open();
                wFlg = true;
            }
            catch (OleDbException ex)
            {
                //Util.MsgErr(ex.Message);
            }
            flgConectado = wFlg;
            return wFlg;
        }
        public static bool Connect(string CA_Con, string Usuario, string Clave)
        {
            string strConnection;
            bool wFlg = false;
            strConnection = "Provider=" + _provider + ";" +
            "Data source=" + CA_Con + ";" +
            "User Id=" + Usuario + ";" +
            "Password=" + Clave + ";";
            //strConnection = "Provider=DB2OLEDB;"
            CnAS400 = new OleDbConnection(strConnection);
            try
            {
                CnAS400.Open();
                wFlg = true;
            }
            catch (OleDbException ex)
            {
                //Util.MsgErr(ex.Message);
            }
            flgConectado = wFlg;
            return wFlg;
        }

        public bool Execute(ref string ParamInput, string esquema, string strProc, int size)
        {
            string Respuesta = "";
            int a = 0;
            bool wFlg = false;
            //OleDbDataReader reader = null;
            string servidor = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
            string bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];
            string usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
            string clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];
            string strConnection = "Provider=" + _provider + ";" +
                            "Data source=" + servidor + ";" +
                            "User Id=" + usuario + ";" +
                            "Password=" + clave + ";";
            using (OleDbConnection CnAS400 = new OleDbConnection())
            {
                using (OleDbCommand CmdAS400 = new OleDbCommand())
                {

                    CnAS400.ConnectionString = strConnection;
                    CnAS400.Open();

                    CmdAS400.Connection = CnAS400;
                    //CmdAS400.CommandType = System.Data.CommandType.StoredProcedure;
                    //CmdAS400.CommandType = System.Data.CommandType.Text;
                    CmdAS400.CommandText = "{{CALL " + esquema + "/" + strProc + "( ? )}}";
                    CmdAS400.Parameters.Add("", OleDbType.Char);// Value = ParamInput;
                    CmdAS400.Parameters[0].Size = size;
                    CmdAS400.Parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                    //CmdAS400.Parameters[0].Value = ParamInput;
                    CmdAS400.Parameters[0].Value = ParamInput;//"       ";

                    try
                    {
                        CmdAS400.ExecuteNonQuery();
                        //wFlg = true;
                        Respuesta = CmdAS400.Parameters[0].Value.ToString();
                        if (Respuesta.Substring(0, 1) == "1")
                        {
                            //     MsgErr("AS400 " + MidDevuelve(_Header, 1, 4) + " " + MidDevuelve(_Header, 11, 4) + " " + vbCrLf + _Respuesta);
                            ParamInput = Respuesta;
                            wFlg = true;
                        }
                        // trans.Commit()
                    }
                    catch (OleDbException ex)
                    {

                        //Util.MsgErr(ex.Message);
                        //wFlg = false;
                    }
                }
            }
            return wFlg;
        }

        public bool Execute2(ref string ParamInput, string esquema, string strProc, int size)
        {
            string Respuesta = "";
            bool wFlg = false;
            //OleDbDataReader reader = null;
            string servidor = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
            string bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];
            string usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
            string clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];
            string strConnection = "Provider=" + _provider + ";" +
                            "Data source=" + servidor + ";" +
                            "User Id=" + usuario + ";" +
                            "Password=" + clave + ";";
            using (OleDbConnection CnAS400 = new OleDbConnection())
            {
                using (OleDbCommand CmdAS400 = new OleDbCommand())
                {

                    CnAS400.ConnectionString = strConnection;
                    CnAS400.Open();

                    CmdAS400.Connection = CnAS400;
                    CmdAS400.CommandType = System.Data.CommandType.StoredProcedure;
                    //CmdAS400.CommandType = System.Data.CommandType.Text;
                    CmdAS400.CommandText = esquema + "." + strProc;
                    CmdAS400.Parameters.Add("", OleDbType.Char);// Value = ParamInput;
                    CmdAS400.Parameters[0].Size = size;
                    CmdAS400.Parameters[0].Direction = System.Data.ParameterDirection.InputOutput;
                    //CmdAS400.Parameters[0].Value = ParamInput;
                    CmdAS400.Parameters[0].Value = ParamInput;//"       ";

                    try
                    {
                        CmdAS400.ExecuteNonQuery();
                        //wFlg = true;
                        Respuesta = CmdAS400.Parameters[0].Value.ToString();
                        ParamInput = Respuesta;
                        wFlg = true;

                    }
                    catch (OleDbException ex)
                    {

                    }
                }
            }
            return wFlg;
        }
        public static bool isConnected()
        {
            //bool wFlg = false;
            if (!flgConectado)
            {
                //   wFlg = Connect(DataSrc);
            }
            return flgConectado;
        }

        public List<object> BaseDatos(string consulta, ArrayList parametros, bool action)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<object> listafinal = new List<object>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];


            foreach (DataRow item in dt.Rows)
            {
                listafinal.Add(SetLista(item));
            }
            return listafinal;

        }

        static List<object> SetLista(DataRow item)
        {
            List<object> categoria = new List<object>
            {

            };
            return categoria;
        }
        //IMPORTANTE MODIFICAR
        public List<object> Insert2(string Tabla, ArrayList parametros, bool action, string CAMPOS)
        {
            string campos = "";
            int i = 0;
            List<object> lista = new List<object>();
            try
            {

                foreach (object elementos in parametros)
                {
                    if (i == parametros.Count - 1)
                    {
                        campos = campos + "'" + elementos + "'";
                    }
                    else
                    {
                        campos = campos + "'" + elementos + "'" + ",";
                    }
                    i++;
                }

                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
                cn.Open();
                OleDbCommand comando = new OleDbCommand("INSERT INTO  " + ParametrosGlobales.bd + "." + Tabla + " (" + CAMPOS + ") values(" + campos + ") ", cn);
                afectados = 0;
                afectados = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Util.MsgErr(ex.ToString());
                afectados = -2;
            }

            return lista;
        }



        public List<object> execute(string Tabla, ArrayList parametros, bool action)
        {
            List<object> lista = new List<object>();
            try
            {
                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
                cn.Open();
                OleDbCommand comando = new OleDbCommand("SELECT * FROM " + ParametrosGlobales.bd + "" + Tabla + "", cn);
                if (action)
                {
                    afectados = 0;
                    afectados = comando.ExecuteNonQuery();
                }
                else
                {
                    OleDbDataReader reader = comando.ExecuteReader(System.Data.CommandBehavior.Default);
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            //object[] arr = new object[reader1.FieldCount - 1];
                            Row dic = new Row();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dic.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            lista.Add(dic);
                        }
                        while (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                //object[] arr = new object[reader1.FieldCount - 1];
                                Row dic = new Row();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dic.Add(reader.GetName(i), reader.GetValue(i));
                                }
                                lista.Add(dic);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                afectados = -2;
            }
            return lista;
        }

        public List<object> execute2(string Tabla, object whereParam, bool action)
        {
            List<object> lista = new List<object>();
            try
            {
                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
                cn.Open();
                OleDbCommand comando = new OleDbCommand("SELECT * FROM " + ParametrosGlobales.bd + Tabla + " where " + whereParam + "", cn);
                if (action)
                {
                    afectados = 0;
                    afectados = comando.ExecuteNonQuery();
                }
                else
                {
                    OleDbDataReader reader = comando.ExecuteReader(System.Data.CommandBehavior.Default);
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            //object[] arr = new object[reader1.FieldCount - 1];
                            Row dic = new Row();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                dic.Add(reader.GetName(i), reader.GetValue(i));
                            }
                            lista.Add(dic);
                        }
                        while (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                //object[] arr = new object[reader1.FieldCount - 1];
                                Row dic = new Row();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    dic.Add(reader.GetName(i), reader.GetValue(i));
                                }
                                lista.Add(dic);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                afectados = -2;
            }
            return lista;
        }


        public List<object> Update2(string Tabla, ArrayList parametros, object whereParam, bool action)
        {
            List<object> lista = new List<object>();
            string campos = "";
            int i = 0;
            foreach (object elementos in parametros)
            {
                if (i == parametros.Count - 1)
                {
                    campos = campos + elementos;
                }
                else
                {
                    campos = campos + elementos + ",";
                }
                i++;
            }
            try
            {
                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
                cn.Open();
                OleDbCommand comando = new OleDbCommand("UPDATE " + ParametrosGlobales.bd + "." + Tabla + " SET " + campos + "where " + whereParam + "", cn);
                afectados = 0;
                afectados = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //Util.MsgErr(ex.ToString());
                afectados = -2;
            }
            return lista;
        }


        public List<object> execProc(string strProc, ArrayList parametros, bool action)
        {
            //OleDbDataReader reader = null;
            string servidor = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
            string bd = System.Configuration.ConfigurationManager.AppSettings["bdAS400"];
            string usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
            string clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];
            string strConnection = "Provider=" + _provider + ";" +
                            "Data source=" + servidor + ";" +
                            "User Id=" + usuario + ";" +
                            "Password=" + clave + ";";

            List<object> lista = new List<object>();

            try
            {
                /*if (CnAS400 == null)
                {
                    Connect();
                }*/
                //if (isConnected())
                using (OleDbConnection CnAS400 = new OleDbConnection())
                {
                    using (OleDbCommand CmdAS400 = new OleDbCommand(bd  + strProc, CnAS400))
                    {
                        CnAS400.ConnectionString = strConnection;
                        CnAS400.Open();
                        //CmdAS400.Connection = CnAS400;
                        //CmdAS400.CommandText = esquema + "." + strProc;
                        CmdAS400.CommandType = System.Data.CommandType.StoredProcedure;

                        foreach (object elemento in parametros)
                        {
                            CmdAS400.Parameters.Add(new OleDbParameter(elemento.ToString(), elemento.ToString()));
                        }
                        if (action)
                        {
                            afectados = 0;
                            afectados = CmdAS400.ExecuteNonQuery();
                        }
                        else
                        {

                            OleDbDataReader reader = CmdAS400.ExecuteReader(System.Data.CommandBehavior.Default);

                            if (reader != null)
                            {


                                while (reader.Read())
                                {
                                    //object[] arr = new object[reader1.FieldCount - 1];
                                    Row dic = new Row();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {

                                        dic.Add(reader.GetName(i), reader.GetValue(i));
                                    }

                                    lista.Add(dic);

                                }
                                while (reader.NextResult())
                                {
                                    while (reader.Read())
                                    {
                                        //object[] arr = new object[reader1.FieldCount - 1];
                                        Row dic = new Row();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {

                                            dic.Add(reader.GetName(i), reader.GetValue(i));
                                        }

                                        lista.Add(dic);

                                    }
                                }



                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Util.MsgErr(ex.ToString());
                afectados = -2;
            }
            return lista;
        }



        public OleDbDataReader execProc(string strProc)
        {
            OleDbDataReader reader = null;
            try
            {
                if (CnAS400 == null)
                {
                    Connect();
                }

                if (isConnected())
                {
                    CmdAS400 = new OleDbCommand();
                    CmdAS400.Connection = CnAS400;
                    CmdAS400.CommandText = esquema + "." + strProc;
                    CmdAS400.CommandType = System.Data.CommandType.StoredProcedure;
                    reader = CmdAS400.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                //Util.MsgErr(ex.ToString());
            }
            return reader;
        }

        /*
                public void execProc(string strProc, ArrayList parametros, ref object valor)
                {

                    try
                    {
                        CmdAS400 = new OleDbCommand();
                        CmdAS400.Connection = CnAS400;
                        CmdAS400.CommandText = esquema + "." + strProc;
                        CmdAS400.CommandType = System.Data.CommandType.StoredProcedure;
                        foreach (OleDbParameter elemento in parametros)
                        {
                            CmdAS400.Parameters.Add(new OleDbParameter(elemento.ParameterName, elemento.Value));
                        }
                        valor = CmdAS400.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        //Util.MsgErr(ex.ToString());
                    }
                }


                public object GetID(string strCampo, string strTabla)
                {
                    Object valor = null;
                    try
                    {

                        CmdAS400 = new OleDbCommand();
                        CmdAS400.Connection = CnAS400;
                        CmdAS400.CommandText = esquema + "." + "SP_GETID2";
                        CmdAS400.CommandType = CommandType.StoredProcedure;

                        CmdAS400.Parameters.Add(new OleDbParameter("CAMPO", strCampo));
                        CmdAS400.Parameters.Add(new OleDbParameter("TABLA", esquema + "." + strTabla));

                        valor = CmdAS400.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        //Util.MsgErr(ex.ToString());
                    }
                    return valor;
                }



            /*    public int execProc(string strTabla, string strProc, clsBase obj)
                {
                    ArrayList arCampos;
                    int affectedRows = 0;
                    try
                    {
                        OleDbCommand CmdAS400 = new OleDbCommand();
                        CmdAS400.Connection = CnAS400;
                        CmdAS400.CommandText = esquema + "." + strProc;
                        CmdAS400.CommandType = CommandType.StoredProcedure;
                        arCampos = GetColumns(strTabla, obj);
                        AddParameters(CmdAS400, arCampos, obj);
                        if (arCampos != null)
                        {
                            affectedRows = CmdAS400.ExecuteNonQuery();
                            Util.exito = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        //Util.MsgErr(ex.ToString());
                        Util.exito = false;
                    }
                    return affectedRows;
                }

                public int execProc(string strTabla, string strProc, Hashtable _hash)
                {
                    ArrayList arCampos = null;
                    int affectedRows = 0;
                    try
                    {
                        CmdAS400 = new OleDbCommand();
                        CmdAS400.Connection = CnAS400;
                        CmdAS400.CommandText = esquema + "." + strProc;
                        CmdAS400.CommandType = CommandType.StoredProcedure;
                        arCampos = GetColumns(strTabla, _hash);
                        if (arCampos != null)
                        {
                            affectedRows = CmdAS400.ExecuteNonQuery();
                            //        Util.exito = true;
                        }
                        else
                        {
                            //        Util.exito = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                        Util.MsgErr(ex.ToString());
                        //     Util.exito = false;
                    }
                    return affectedRows;
                }

         /*       public ArrayList GetColumns(string tabla, clsBase obj)
                {

                    OleDbDataReader reader = null;
                    ArrayList lista = new ArrayList();
                    object propiedad = null;
                    string elemento;
                    try
                    {
                        OleDbCommand CmdAS400 = new OleDbCommand();
                        CmdAS400.Connection = CnAS400;
                        CmdAS400.CommandText = esquema + "." + "SP_GETCOLS";
                        CmdAS400.CommandType = CommandType.StoredProcedure;
                        CmdAS400.Parameters.Add(new OleDbParameter("TABLA", tabla));
                        reader = CmdAS400.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                elemento = reader.GetString(0);
                                lista.Add(elemento);
                                try
                                {
                                    propiedad = obj.GetProperty(elemento);
                                }
                                catch (Exception ex)
                                {
                                    Util.MsgErr(ex.ToString());
                                }

                                this.CmdAS400.Parameters.Add(new OleDbParameter(elemento, propiedad));

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                        Util.MsgErr(ex.ToString());
                        reader = null;
                        lista = null;
                    }
                    return lista;
                }
        */
        /*       public ArrayList GetColumns(string tabla, Hashtable _hash)
               {
                   OleDbDataReader reader;
                   ArrayList lista = new ArrayList();
                   string elemento;
                   try
                   {
                       OleDbCommand CmdAS400 = new OleDbCommand();
                       CmdAS400.Connection = CnAS400;
                       CmdAS400.CommandText = esquema + "." + "SP_GETCOLS";
                       CmdAS400.CommandType = CommandType.StoredProcedure;
                       CmdAS400.Parameters.Add(new OleDbParameter("TABLA", tabla));
                       reader = CmdAS400.ExecuteReader();
                       if (reader.HasRows)
                       {
                           while (reader.Read())
                           {
                               elemento = reader.GetString(0);
                               lista.Add(elemento);
                               //          this.CmdAS400.Parameters.Add(new OleDbParameter(elemento, _hash.Item(elemento)));
                           }
                       }
                   }
                   catch (Exception ex)
                   {
                       throw;
                       //Util.MsgErr(ex.ToString());
                       reader = null;
                   }
                   return lista;
               }
        */
        /*      public void AddParameters(ArrayList lista, clsBase obj)
              {

                  foreach (string elemento in lista)
                  {
                      CmdAS400.Parameters.Add(new OleDbParameter(elemento, obj.GetProperty(elemento)));
                  }
              }

              public void AddParameters(OleDbCommand cmd, ArrayList lista, clsBase obj)
              {

                  foreach (string elemento in lista)
                  {
                      cmd.Parameters.Add(new OleDbParameter(elemento, obj.GetProperty(elemento)));
                  }
              }


              public static void SetCamposAuditoria(Hashtable htParams)
              {
                  clsIBMas400 ibm400 = new clsIBMas400();
                  ibm400.execProc("SP_AUDTR", CrearListParams(htParams));
              }

              public static OleDbDataReader GetCamposAuditoria()
              {
                  clsIBMas400 ibm400 = new clsIBMas400();
                  return ibm400.execProc("SP_GETAUDT");
              }

        */
        //public static string Comandos()
        //{
        //    return exec("DBCSLCDTDC", "COMANDOS", 1000);
        //}


        //public static string exec(string libreria, string programa, int tamaño)
        //{
        //    string str = "";
        //    string s = "";
        //    bool wFlg = false;
        //    string sRet = "";

        //    wFlg = Execute(s, libreria, programa, tamaño);
        //    if (wFlg)
        //    {
        //        //     sRet = LinkDA400.Respuesta;
        //    }
        //    else
        //    {
        //        //     sRet = Util.strZero("0", 10);
        //    }
        //    return str;

        //}

        public void BeginTrans()
        {
            //try
            //{
            //    if (trans = null)
            //    {
            //        trans = CnAS400.BeginTransaction();
            //    }
            //    else
            //    {
            //        //           trans.Begin()
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Util.MsgErr(ex.ToString());
            //}

        }

        public void EndTrans()
        {
            //try
            //{
            //    if (trans != null)
            //    {
            //        trans.Commit();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Util.MsgErr(ex.ToString());
            //}

        }

        public void RollbackTrans()
        {
            //try
            //{
            //    if (trans != null)
            //    {
            //        trans.Rollback();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Util.MsgErr(ex.ToString());
            //}

        }
        public void AddTrans()
        {
            //  AddTrans(CmdAS400)
        }
        public void AddTrans(OleDbCommand cmd)
        {
            // cmd.Transaction = trans
        }

        /*     public OleDbDataReader getTable(string tabla, string campos, string _where)
             {
                 OleDbDataReader reader = null;
                 try
                 {
                     CmdAS400 = new OleDbCommand();
                     if (CnAS400.State == ConnectionState.Open)
                     {
                         CmdAS400.Connection = CnAS400;
                         CmdAS400.CommandText = esquema + "." + "SP_GETTBL";
                         CmdAS400.CommandType = CommandType.StoredProcedure;
                         CmdAS400.Parameters.Add(new OleDbParameter("ESQUEMA", esquema));
                         CmdAS400.Parameters.Add(new OleDbParameter("TABLA", tabla));
                         CmdAS400.Parameters.Add(new OleDbParameter("CAMPOS", campos));
                         CmdAS400.Parameters.Add(new OleDbParameter("I_WHERE", ""));
                         reader = CmdAS400.ExecuteReader();
                     }
                     else
                     {
                         //Util.MsgErr("LA CONEXIÓN NO ESTÁ ESTABLECIDA");
                     }

                 }
                 catch (Exception ex)
                 {
                     //Util.MsgErr(ex.ToString());
                 }
                 return reader;
             }
             */
        public static OleDbDataReader CreaDataReader(string wCommand, OleDbConnection wConexion)
        {
            OleDbCommand wCmd = new OleDbCommand();
            OleDbDataReader wDr = null;
            wCmd.CommandText = wCommand;
            if ((wConexion != null) /*&& (wConexion.State == ConnectionState.Open)*/)
            {
                wCmd.Connection = wConexion;
                wDr = wCmd.ExecuteReader();
            }
            else
            {
                wDr = null;
            }
            return wDr;
        }


        /* public void Connect()
         {
             string strCon = "";
             OleDbConnection cnAS400 = new OleDbConnection(strCon);
             cnAS400.Open();

         }*/

    }
}