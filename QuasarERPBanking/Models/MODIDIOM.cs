using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class MODIDIOM
    {
        public string OPCMODULO { get; set; }
        public string OPCNBMOD { get; set; }
        public string CULTURE { get; set; }

        //NUEVOS
        public string OPCHIJO { get; set; }
        public string MODULO { get; set; }
        public string OPCNOMB { get; set; }
        public string ICONS { get; set; }


        public static List<MODIDIOM> getNOMMODIDIOM(string culture)
        {
            List<MODIDIOM> modulos = new List<MODIDIOM>();
            clsAS400 cn = new clsAS400();
            List<object> reader = cn.execute2("Q_MODIDIOM", whereParam(), false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    MODIDIOM mod = new MODIDIOM();

                    mod.OPCMODULO = item["OPCMODULO"].ToString();
                    mod.OPCNBMOD = item["OPCNBMOD"].ToString();
                    mod.ICONS = item["ICONS"].ToString();
                    //mod.OPCULT = int.Parse(item["OPCULT"].ToString());

                    modulos.Add(mod);
                }
            }
            return modulos;
        }


        // NUEVO 
        public static List<MODIDIOM> getNOMMODIDIOM2(string culture)
        {
            List<MODIDIOM> modulos = new List<MODIDIOM>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(ParametrosGlobales.culture);
            List<object> reader = cn.execute2("Q_OPCIDIOM2", whereParam(), false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    MODIDIOM mod = new MODIDIOM();

                    mod.OPCHIJO = item["OPCHIJO"].ToString();
                    mod.MODULO = item["MODULO"].ToString();
                    mod.OPCNOMB = item["OPCNOMB"].ToString();
                    //mod.OPCULT = int.Parse(item["OPCULT"].ToString());

                    modulos.Add(mod);
                }
            }
            return modulos;
        }

        protected static object whereParam()
        {
            string where = "CULTURE ='" + ParametrosGlobales.culture + "'";


            return where;
        }


    }

}


//////////////// SP UTILIZADOS ////////////////
//SP_Q_MODIDIOM
