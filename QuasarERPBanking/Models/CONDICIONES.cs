using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources.MANTENIMIENTO;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace QuasarERPBanking.Models
{
    public class CONDICIONES : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int CODIGO { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string CONDICION { get; set; }
            
            //para cargar el dropdownlist de los formularios
            static string sp_getAll = "SP_ALL_CONDICIONES";
            public static IEnumerable<CONDICIONES> GetCondicion()
            {
                clsAS400 cn = new clsAS400();
                System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
                List<object> reader = cn.execProc(sp_getAll, parametros, false);
                List<CONDICIONES> Condiciones = new List<CONDICIONES>();

                if (reader != null)
                {
                    foreach (Dictionary<string, object> item in reader)
                    {
                    CONDICIONES Condicion = new CONDICIONES();



                    Condicion.CONDICION = item["CONDICION"].ToString();
                    Condiciones.Add(Condicion);
                    }
                }

                return Condiciones;
            }

        public CONDICIONES()
        {
            Campo_Clave = "CODIGO";
        }

        //llenar la lista completa de condiciones para el mantenimiento
        public static List<CONDICIONES> GetCond()
        {
            List<CONDICIONES> lstStates = new List<CONDICIONES>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    lstStates.Add(setState(item));
                }
            }
            return lstStates;
        }

        static CONDICIONES setState(Row item)
        {
            CONDICIONES user = new CONDICIONES
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                CONDICION = item["CONDICION"].ToString(),

            };
            return user;
        }

        //cargar los datos de una condicion
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                CODIGO = int.Parse(item["CODIGO"].ToString());
                CONDICION = item["CONDICION"].ToString();

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);
            parametros.Add(CODIGO);
            parametros.Add(CONDICION);
            return parametros;
        }



    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_ALL_CONDICIONES
//SP_DEL_CONDICIONES
//SP_INS_CONDICIONES
//SP_UPD_CONDICIONES
//SP_Q_CONDICIONES
