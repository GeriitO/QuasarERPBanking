using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class CULTURES
    {
        public string CODCULT { get; set; }
        public string CULTURE { get; set; }

        public System.Globalization.CultureInfo CurrentCulture { get; set; }


        static string sp_getAll = "SP_GET_CULTURES";
        public static IEnumerable<CULTURES> GetCultures()
        {

            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<CULTURES> culturas = new List<CULTURES>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    CULTURES cultura = new CULTURES();

                   

                    cultura.CODCULT = item["CODCULT"].ToString();
                    cultura.CULTURE = item["CULTURE"].ToString();
                    culturas.Add(cultura);
                }
            }

            return culturas;
        }

    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_GET_CULTURES
