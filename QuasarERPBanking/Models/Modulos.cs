using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class Modulos
    {
        public string OPCMODULO { get; set; }
        public string OPCPADRE { get; set; }
        public string OPCHIJO { get; set; }    //NUEVO  no existia

        public static List<Modulos> getSubMods(string OPCMODULO)
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(OPCMODULO);
            List<object> reader = cn.execProc("SP_GETSUBMOD", parametros, false);  //VIEJO como estaba antes
            //List<object> reader = cn.execProc("SP_GETSUBMOD2", parametros, false);
            List<Modulos> modulos = new List<Modulos>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Modulos modulo = new Modulos();
                    modulo.OPCMODULO = item["OPCMODULO"].ToString();        //VIEJO     como estaba antes
                    //modulo.OPCPADRE= item["OPCPADRE"].ToString();
                    //modulo.OPCHIJO = item["OPCHIJO"].ToString();
                    //modulo.OPCMODULO= item["MODULO"].ToString();
                    modulos.Add(modulo);

                }
            }

            return modulos;
        }




        public static List<Modulos> getSubMods2(string OPCMODULO)
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(OPCMODULO);
            //List<object> reader = cn.execProc("SP_GETSUBMOD", parametros, false);  //VIEJO como estaba antes
            List<object> reader = cn.execute2("Q_MOD2", whereParam(OPCMODULO), false);
            List<Modulos> modulos = new List<Modulos>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    Modulos modulo = new Modulos();
                    /*  modulo.OPCMODULO = item["OPCMODULO"].ToString();     */   //VIEJO     como estaba antes
                    modulo.OPCPADRE = item["OPCPADRE"].ToString();
                    //modulo.OPCHIJO = item["OPCHIJO"].ToString();
                    modulo.OPCMODULO = item["MODULO"].ToString();
                    modulos.Add(modulo);

                }
            }

            return modulos;
        }

        protected static object whereParam(string OPCMODULO)
        {
            string where = "MODULO ='"+ OPCMODULO + "'";


            return where;
        }

    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_GETSUBMOD