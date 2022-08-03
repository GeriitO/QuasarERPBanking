using Resources.MANTENIMIENTO;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Collections;


namespace QuasarERPBanking.Models
{
    public class RAMOS : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int RAMCOD { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string RAMNOMBRE { get; set; }

        static string sp_getAll = "SP_ALL_RAMOS";

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<RAMOS> GetRamos()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<RAMOS> ramos = new List<RAMOS>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    RAMOS ramo = new RAMOS();
                    //int codigo = 0;
                    //int.TryParse(item["RAMCOD"].ToString(), out codigo);
                    //ramo.RAMCOD = codigo;
                    ramo.RAMNOMBRE = item["RAMNOMBRE"].ToString();
                    ramos.Add(ramo);
                }
            }

            return ramos;
        }

        public RAMOS()
        {
            Campo_Clave = "RAMCOD";
        }

        //llenar la lista completa de ramos para el mantenimiento
        public static List<RAMOS> GetRamo()
        {
            List<RAMOS> lstRamos = new List<RAMOS>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    lstRamos.Add(setState(item));
                }
            }
            return lstRamos;
        }

        static RAMOS setState(Row item)
        {
            RAMOS user = new RAMOS
            {
                RAMCOD = int.Parse(item["RAMCOD"].ToString()),
                RAMNOMBRE = item["RAMNOMBRE"].ToString(),

            };
            return user;
        }

        //cargar los datos de una condicion
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                RAMCOD = int.Parse(item["RAMCOD"].ToString());
                RAMNOMBRE = item["RAMNOMBRE"].ToString();

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);
            parametros.Add(RAMCOD);
            parametros.Add(RAMNOMBRE);
            return parametros;
        }



    }
}



//////////////// SP UTILIZADOS ////////////////
//SP_ALL_RAMOS
//SP_INS_RAMOS
//SP_DEL_RAMOS
//SP_UPD_RAMOS
//SP_Q_RAMOS