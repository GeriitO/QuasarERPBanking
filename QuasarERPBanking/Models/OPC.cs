using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;

using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class OPC
    {

        public int OPCCOD { get; set; }
        public string OPCNOM { get; set; }
        public string OPCMODULO { get; set; }
        public int OPCULT { get; set; }
        public int OPCDET { get; set; }
        public string OPCNBMOD { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string IDLEVEL { get; set; }


        public static List<OPC> getOpcMod(string OPCMODULO)
        {
            List<OPC> opciones = new List<OPC>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(OPCMODULO);
            List<object> reader = cn.execProc("SP_Q_OPCMOD", parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    OPC opc = new OPC();
                    opc.OPCCOD = int.Parse(item["OPCCOD"].ToString());
                    opc.OPCDET = int.Parse(item["OPCDET"].ToString());
                    opc.OPCNOM = item["OPCNOM"].ToString();
                    opc.OPCMODULO = item["OPCMODULO"].ToString();
                    opc.OPCNBMOD = item["OPCNBMOD"].ToString();
                    opc.OPCULT = int.Parse(item["OPCULT"].ToString());
                    //opc.OPCNOM = item["OPCNOM"].ToString();           //viejo

                    opciones.Add(opc);
                }
            }
            return opciones;
        }
        public static List<string> getModulos()
        {
            List<string> modulos = new List<string>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc("SP_Q_MODULOS", parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    string modulo = "";
                    modulo = item["OPCNBMOD"].ToString();
                    //modulo = item["OPCNBMOD"].ToString();    //viejo
                    modulos.Add(modulo);
                }
            }
            return modulos;
        }


        public static List<string> getModulos_()
        {
            List<string> modulos = new List<string>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc("SP_Q_MODULOS_", parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    string modulo = "";
                    modulo = item["OPCNBMOD"].ToString();
                    //modulo = item["OPCNBMOD"].ToString();    //viejo
                    modulos.Add(modulo);
                }
            }
            return modulos;
        }


        public static List<OPC> getOpcUser(string usuario)
        {

            List<OPC> lista = new List<OPC>();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            OleDbCommand comando = new OleDbCommand("SELECT DISTINCT R . OPCCOD , R . OPCMODULO , I . OPCNOM , V . OPCONTROLLER , V . OPCACTION , V . OPCORDER FROM " + ParametrosGlobales.bd + "  Q_ROLOPC R INNER JOIN " + ParametrosGlobales.bd + "  Q_OPCVIEW V ON R . OPCCOD = V . OPCCOD AND R . OPCMODULO = V . OPCMODULO INNER JOIN " + ParametrosGlobales.bd + "  Q_OPCIDIOM I ON R . OPCCOD = I . OPCCOD AND R . OPCMODULO = I . OPCMODULO WHERE R . ROLCOD IN ( SELECT ROLCOD FROM " + ParametrosGlobales.bd + "  Q_ROLUSER WHERE USUARIO = '" + usuario + "' ) AND I . CULTURE = '" + (ParametrosGlobales.culture) + "'  ORDER BY V . OPCORDER ", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    OPC opcUsr = new OPC
                    {
                        OPCCOD = int.Parse(reader["OPCCOD"].ToString()),
                        OPCMODULO = reader["OPCMODULO"].ToString(),
                        Controller = reader["OPCONTROLLER"].ToString(),
                        Action = reader["OPCACTION"].ToString(),
                        OPCNOM = reader["OPCNOM"].ToString(),

                    };
                    lista.Add(opcUsr);
                }
            }
            cn.Close();
            return lista;
        }


        // NUEVO
        public static List<OPC> getOpcUser2(string usuario)
        {

            List<OPC> lista = new List<OPC>();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
           
            cn.Open();
            OleDbCommand comando = new OleDbCommand("SELECT A . OPCPADRE , A . MODULO , A . OPCHIJO , A . OPCONTROLLER , A . OPCACTION , A . OPCORDER , A . IDLEVEL , B . OPCNOM , C . OPCNOMB  FROM  " + ParametrosGlobales.bd + "  Q_MOD2 A  INNER JOIN  " + ParametrosGlobales.bd + "  Q_OPCIDIOM B  ON  A . OPCPADRE = B . OPCCOD  AND  A . MODULO = B . OPCMODULO  INNER JOIN  " + ParametrosGlobales.bd + "  Q_OPCIDIOM2 C  ON  A . OPCHIJO = C . OPCHIJO  AND  A . MODULO = C . MODULO  WHERE B . CULTURE = '" + (ParametrosGlobales.culture) + "' AND C . CULTURE = '" + (ParametrosGlobales.culture) + "'", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    OPC opcUsr = new OPC
                    {
                        OPCCOD = int.Parse(reader["OPCPADRE"].ToString()),
                        OPCMODULO = reader["MODULO"].ToString(),
                        Controller = reader["OPCONTROLLER"].ToString(),
                        Action = reader["OPCACTION"].ToString(),
                        OPCNOM = reader["OPCNOMB"].ToString(),
                        OPCNBMOD = reader["OPCNOM"].ToString(),
                        IDLEVEL = reader["IDLEVEL"].ToString(),
                    };
                    lista.Add(opcUsr);
                }
            }

            return lista;
        }


        protected static object whereParam()
        {
            string where = " B.CULTURE = '" + ParametrosGlobales.culture + "' AND C.CULTURE = '" + ParametrosGlobales.culture + "' ; ";


            return where;
        }
        public static List<OPC> getOpcLevel3()
        {

            List<OPC> lista = new List<OPC>();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
           
            cn.Open();
            //OleDbCommand comando = new OleDbCommand("SELECT A . OPCPADRE , A . MODULO , A . OPCHIJO , A . OPCONTROLLER , A . OPCACTION , A . OPCORDER , A . IDLEVEL , B . OPCNOM , C . OPCNOMB  FROM  " + ParametrosGlobales.bd + " . Q_MOD2 A  INNER JOIN  " + ParametrosGlobales.bd + " . Q_OPCIDIOM B  ON  A . OPCPADRE = B . OPCCOD  AND  A . MODULO = B . OPCMODULO  INNER JOIN  " + ParametrosGlobales.bd + " . Q_OPCIDIOM2 C  ON  A . OPCHIJO = C . OPCHIJO  AND  A . MODULO = C . MODULO  WHERE B . CULTURE = '" + (ParametrosGlobales.culture) + "' AND C . CULTURE = '" + (ParametrosGlobales.culture) + "'", cn);
            OleDbCommand comando = new OleDbCommand("SELECT A . OPCPADRE , A . OPCONTROLLER , A . OPCACTION , C . OPCNOMB FROM " + ParametrosGlobales.bd + "Q_MOD3 A INNER JOIN " + ParametrosGlobales.bd + "Q_OPCIDIOM3 C ON A . OPCHIJO = C . OPCHIJO AND A . MODULO = C . MODULO WHERE C . CULTURE ='" + (ParametrosGlobales.culture) + "'", cn);
            OleDbDataReader reader = comando.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    OPC opcUsr = new OPC
                    {
                        IDLEVEL = reader["OPCPADRE"].ToString(),
                        //OPCMODULO = item["MODULO"].ToString(),
                        Controller = reader["OPCONTROLLER"].ToString(),
                        Action = reader["OPCACTION"].ToString(),
                        OPCNOM = reader["OPCNOMB"].ToString(),
                        //OPCNBMOD = item["OPCNOM"].ToString(),
                        //IDLEVEL = item["IDLEVEL"].ToString(),
                    };
                    lista.Add(opcUsr);
                }
            }

            return lista;
        }




    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_Q_OPCMOD
//SP_Q_MODULOS
//SP_Q_OPCUSR
//SP_Q_OPCUSR2
//SP_Q_OPCUSR3

