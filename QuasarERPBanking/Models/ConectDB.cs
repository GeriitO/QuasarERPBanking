using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using QuasarERPBanking.Models;
using System.Data.OleDb;
using System.Data;
using Resources;



namespace QuasarERPBanking.Models
{
    public class ConectDB
    {
        public static string DataSrc = System.Configuration.ConfigurationManager.AppSettings["servidorAS400"];
        public static string esquema = System.Configuration.ConfigurationManager.AppSettings["LibreriaCN"];
        public static string _provider = System.Configuration.ConfigurationManager.AppSettings["dbprovider"];
        public static string Usuario = System.Configuration.ConfigurationManager.AppSettings["usuarioAS400"];
        public static string Clave = System.Configuration.ConfigurationManager.AppSettings["claveAS400"];
        public static string CnStr = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
        public static string CnStr2 = System.Configuration.ConfigurationManager.ConnectionStrings["CN2"].ToString();
        public static string QueryExiste = "";
        public static string QueryInsert = "";
        public static string QueryConsul = "";

        public virtual string tabla { get; set; }

        public virtual string tablalote { get; set; }
        public virtual string Campo_Clave { get; set; }
        public virtual string Campo_Clave2 { get; set; }

        public virtual string CampoLote { get; set; }
        public int afectados { get; set; }
        //DB2

        //public static string CnStr = "Provider=" + _provider + ";" +
        //                "Data source=" + DataSrc + ";" +
        //                "User Id=" + Usuario + ";" +
        //                "Password=" + Clave + ";";

        //sql
        //public static string CnStr = "provider=" + _provider + "; Data Source=" + DataSrc + ";User Id=" + Usuario + ";Password=" + Clave + ";initial catalog=" + esquema + ";";


        public ConectDB()
        {
            tabla = "";
            Campo_Clave = "";

        }


        public List<object> BaseDatos(string consulta, bool action)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<object> lista = new List<object>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];
            {
                if (dt.Rows != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        lista.Add(CargarObjeto(dt));
                    }
                }
            }
            return lista;

        }

        static List<object> SetLista(DataRow item)
        {
            List<object> categoria = new List<object>
            {

            };
            return categoria;
        }

        protected virtual ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }

        protected virtual ConectDB CargarObjeto(Rows dt)
        {
            foreach (DataRow item in dt)
            {

            }
            return this;

        }

        protected virtual ConectDB CargarObjeto(DataTable dt)
        {
            foreach (DataRow item in dt.Rows)
            {

            }
            return this;

        }

        protected virtual ConectDB LoadObject(Rows reader)
        {
            foreach (Row item in reader)
            {

            }
            return this;

        }

        protected virtual ConectDB LoadLote(Rows reader)
        {
            foreach (Row item in reader)
            {

            }
            return this;

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


        public bool Existe(string Query)
        {
            bool resultado = false;
            string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            OleDbConnection CN = new OleDbConnection(strconnect);
            CN.Open();
            OleDbCommand CMD = new OleDbCommand(Query, CN);
            OleDbDataReader DR = CMD.ExecuteReader();
            if (DR.Read())
            {
                resultado = true;
            }
            CN.Close();
            return resultado;
        }

        public int Insert(string Query)
        {
            int resultado = 0;
            string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            OleDbConnection CN = new OleDbConnection(strconnect);
            CN.Open();
            OleDbCommand CMD = new OleDbCommand(Query, CN);
            try
            {
                OleDbDataReader DR = CMD.ExecuteReader();
                resultado = -1;
            }
            catch (Exception ex)
            {
                resultado = -2;
            }
            CN.Close();
            return resultado;
        }

        public ConectDB BuscarOrig(string codigo)
        {

            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            OleDbCommand comando = new OleDbCommand("Select * from " + ParametrosGlobales.bd + "." + tabla + " where " + Campo_Clave + " ='" + codigo + "' ", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            ConectDB objeto = null;
            if (reader != null)
            {
                objeto = LoadObject54(reader);
            }
            return objeto;
        }

        protected virtual ConectDB LoadObject54(OleDbDataReader reader)
        {
            while (reader.Read())
            {

            }
            return this;

        }

        protected virtual ConectDB LoadLote(OleDbDataReader reader)
        {
            while (reader.Read())
            {

            }
            return this;

        }

        public int Update(string Query)
        {
            int resultado = 0;
            string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            OleDbConnection CN = new OleDbConnection(strconnect);
            CN.Open();
            OleDbCommand CMD = new OleDbCommand(Query, CN);
            try
            {
                OleDbDataReader DR = CMD.ExecuteReader();
                resultado = -1;
            }
            catch (Exception ex)
            {
                resultado = -2;
            }
            CN.Close();
            return resultado;
        }

        public int Delete(string Query)
        {
            int resultado = 0;
            string strconnect = System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString();
            OleDbConnection CN = new OleDbConnection(strconnect);
            CN.Open();
            OleDbCommand CMD = new OleDbCommand(Query, CN);
            try
            {
                OleDbDataReader DR = CMD.ExecuteReader();
                resultado = -1;
            }
            catch (Exception ex)
            {
                resultado = -2;
            }
            CN.Close();
            return resultado;
        }


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
                OleDbCommand comando = new OleDbCommand("INSERT INTO  " + ParametrosGlobales.bd + "" + Tabla + " (" + CAMPOS + ") values(" + campos + ") ", cn);
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

        public static DataSet Datos_Retorno(string Query)
        {
            DataSet oDataset = new DataSet();
            OleDbDataAdapter DA = new OleDbDataAdapter(Query, System.Configuration.ConfigurationManager.ConnectionStrings["CN"].ToString());
            DA.Fill(oDataset);
            return oDataset;

        }

        public string Edit()
        {
            string message = string.Empty;
            if (this.Existe(QueryExiste))
            {

                if (this.Update(QueryInsert) == -1)
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
                OleDbCommand comando = new OleDbCommand("UPDATE " + ParametrosGlobales.bd + "" + Tabla + " SET " + campos + "where " + whereParam + "", cn);
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

        public string Create()
        {
            string message = string.Empty;
            if (!this.Existe(QueryExiste))
            {

                switch (this.Insert(QueryInsert))
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
            int result = this.Delete(QueryInsert);
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


        public ConectDB cargar(string tabla, object consulta)
        {
            ConectDB cn = new ConectDB();
            Rows dt = cn.execute2(tabla, consulta, false);
            ConectDB objeto = null;

            if (dt != null)
            {
                objeto = CargarObjeto(dt);
            }
            return objeto;
        }


        protected virtual ConectDB LoadObject2(Rows reader)
        {
            foreach (Row item in reader)
            {

            }
            return this;

        }


        public List<object> NumeroLote(string tabla, string CampoLote)
        {
            string NumAlfa = "";
            decimal NumLot = '0';
            int Valor = +1;
            List<object> lista = new List<object>();
            try
            {
                OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
                cn.Open();
                OleDbCommand comando = new OleDbCommand("SELECT " + CampoLote + " FROM " + ParametrosGlobales.bd + "" + tabla + "", cn);
                OleDbDataReader reader = comando.ExecuteReader(System.Data.CommandBehavior.Default);
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        NumAlfa = reader[CampoLote].ToString().Substring(0, 2);
                        NumLot = (decimal.Parse(reader[CampoLote].ToString().Substring(2)) + Valor);
                    }
                }
                OleDbCommand cmd2 = new OleDbCommand("UPDATE " + ParametrosGlobales.bd + "" + tabla + " SET " + CampoLote + "='" + NumAlfa + "" + NumLot + "'  ", cn);
                cmd2.ExecuteNonQuery();
                Row dic = new Row();
                {
                    dic.Add(CampoLote, NumAlfa + NumLot);
                }
                lista.Add(dic);
            }
            catch (Exception ex)
            {
                afectados = -2;
            }
            return lista;
        }


        public ConectDB Buscar(string value)
        {
            ConectDB cn = new ConectDB();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execute2(tabla, whereParam(arreglo), false);
            ConectDB objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }


        public ConectDB Buscar(ArrayList value)
        {
            ConectDB cn = new ConectDB();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execute2(tabla, whereParam(value), false);
            ConectDB objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }

        public ConectDB Buscar2(string value)
        {
            ConectDB cn = new ConectDB();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execute2(tabla, whereParam2(arreglo), false);
            ConectDB objeto = null;
            if (reader != null)
            {
                objeto = LoadObject(reader);
            }
            return objeto;
        }

        protected virtual object whereParam(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {

            }

            return QUERY;
        }

        protected virtual string CAMPOS()
        {
            string CAMPOS = "";
            return CAMPOS;
        }

        protected virtual object whereParam2(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {

            }

            return QUERY;
        }
        protected virtual object whereParam()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }

        protected virtual ArrayList getParametersUpd()
        {
            ArrayList parametros = new ArrayList(0);
            return parametros;
        }


        public int Insert()
        {
            ConectDB cn = new ConectDB();
            cn.Insert2(tabla, getParameters(), true, CAMPOS());
            return cn.afectados;

        }

        public static string ValorCampoStr(string campo, string tabla, string condicion)
        {
            string valor = "";
            OleDbConnection cn = new OleDbConnection(CnStr);
            cn.Open();
            OleDbCommand cmd = new OleDbCommand("select " + campo + " from " + ParametrosGlobales.bd + tabla + " " +condicion, cn);
            OleDbDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                valor = dr[campo].ToString();
            }
            cn.Close();
            return valor;
        }

    }
}
