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
    public class ROLOPC
    {
        public int OPCCOD { get; set; }
        public string OPCMODULO { get; set; }
        public int ROLCOD { get; set; }

        // CARGA EL TREE VIEW
        public static List<ROLOPC> getRolOpc(int rolcod)
        {

            List<ROLOPC> lista = new List<ROLOPC>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(rolcod);
            List<object> reader = cn.execProc("SP_Q_ROLOPC", parametros, false);

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    ROLOPC rolopc = new ROLOPC
                    {
                        OPCCOD = int.Parse(item["OPCCOD"].ToString()),
                        OPCMODULO = item["OPCMODULO"].ToString()
                    };
                    lista.Add(rolopc);
                }
            }

            return lista;
        }

        public static List<ROLOPC> getOpcUser_(string usuario)
        {

            List<ROLOPC> lista = new List<ROLOPC>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(usuario);
            List<object> reader = cn.execProc("SP_Q_OPCUSR", parametros, false);

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    ROLOPC opcUsr = new ROLOPC
                    {
                        OPCCOD = int.Parse(item["OPCCOD"].ToString()),
                        OPCMODULO = item["OPCMODULO"].ToString()
                    };
                    lista.Add(opcUsr);
                }
            }

            return lista;
        }



        //protected override object whereParam()
        //{
        //    string where = "";


        //    return where;
        //}




        public static List<string> getModUser(string usuario, string padre)
        {

            List<string> lista = new List<string>();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            //List<COT_MAESTRO> listafinal = new List<COT_MAESTRO>();
            cn.Open();
            //original
            OleDbCommand comando = new OleDbCommand("SELECT DISTINCT A.OPCMODULO, B.OPCORDER FROM " + ParametrosGlobales.bd + "Q_ROLOPC A INNER JOIN " + ParametrosGlobales.bd + "Q_MOD B ON A.OPCMODULO = B.OPCMODULO WHERE ROLCOD IN(SELECT ROLCOD FROM " + ParametrosGlobales.bd + "Q_ROLUSER WHERE USUARIO = 'EVOLUTION') AND A.OPCMODULO IN(SELECT OPCMODULO FROM " + ParametrosGlobales.bd + "Q_MOD WHERE OPCPADRE = '0') ORDER BY B.OPCORDER ", cn);
            //OleDbCommand comando = new OleDbCommand("SELECT DISTINCT A.OPCMODULO, B.OPCORDER FROM " + ParametrosGlobales.bd + "Q_ROLOPC A INNER JOIN " + ParametrosGlobales.bd + "Q_MOD B ON A.OPCMODULO = B.OPCMODULO WHERE ROLCOD IN(SELECT ROLCOD FROM " + ParametrosGlobales.bd + "Q_ROLUSER WHERE USUARIO = 'EVOLUTION') AND A.OPCMODULO IN(SELECT OPCMODULO FROM " + ParametrosGlobales.bd + "Q_MOD WHERE OPCPADRE = '0') and a.OPCMODULO in (select OPCMODULO from " + ParametrosGlobales.bd +  "Q_OPCVIEW where opcvisible = 1 ) ORDER BY B.OPCORDER ", cn);

            OleDbDataReader reader = comando.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    string modUsr = "";
                    modUsr = reader["OPCMODULO"].ToString();
                    lista.Add(modUsr);
                }
            }
            cn.Close();
            return lista;
        }



        public int DeleteOpcRol()
        {
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(ROLCOD);
            cn.execProc("SP_DEL_OPCROL", parametros, true);
            return cn.afectados;
        }
        public int Insert()
        {
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(2);
            parametros.Add(ROLCOD);
            parametros.Add(OPCCOD);
            parametros.Add(OPCMODULO);
            cn.execProc("SP_INS_ROLOPC", parametros, true);
            return cn.afectados;
        }

    }

   
}




//////////////// SP UTILIZADOS ////////////////
//SP_Q_ROLOPC
//SP_Q_OPCUSR
//SP_Q_MODUSR
//SP_DEL_OPCROL
//SP_INS_ROLOPC  