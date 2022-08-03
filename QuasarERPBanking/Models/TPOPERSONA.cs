using Resources.MANTENIMIENTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Collections;
using System.ComponentModel.DataAnnotations;




namespace QuasarERPBanking.Models
{
    public class TPOPERSONA : ConectDB
    {
      
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int CODIGO { get; set; }
        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string TPOPERSON { get; set; }

        static string sp_getAll = "SP_ALL_TPOPERSONA";




        //para cargar el dropdownlist de los formularios
        public static IEnumerable<TPOPERSONA> GetTpoPersona()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<TPOPERSONA> TpoPersonas = new List<TPOPERSONA>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    TPOPERSONA TpoPersona = new TPOPERSONA();



                    TpoPersona.TPOPERSON = item["TPOPERSON"].ToString();
                    TpoPersona.CODIGO = int.Parse(item["CODIGO"].ToString());
                    TpoPersonas.Add(TpoPersona);
                }
            }

            return TpoPersonas;
        }

        public TPOPERSONA()
        {
            Campo_Clave = "CODIGO";
        }

        //llenar la lista completa de TIPOS DE PERSONAS para el mantenimiento
        public static List<TPOPERSONA> GetTpoPersonas()
        {
            List<TPOPERSONA> lstTpoPersonas = new List<TPOPERSONA>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    lstTpoPersonas.Add(setTpoPersonas(item));
                }
            }
            return lstTpoPersonas;
        }

        static TPOPERSONA setTpoPersonas(Row item)
        {
            TPOPERSONA state = new TPOPERSONA
            {
                CODIGO = int.Parse(item["CODIGO"].ToString()),
                TPOPERSON= item["TPOPERSON"].ToString(),
                

            };
            return state;
        }

        //cargar los datos de un TIPO DE PERSONA
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                CODIGO = int.Parse(item["CODIGO"].ToString());
                TPOPERSON = item["TPOPERSON"].ToString();

            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(2);
            parametros.Add(CODIGO);
            parametros.Add(TPOPERSON);
            return parametros;
        }





    }
}




//////////////// SP UTILIZADOS ////////////////
//SP_ALL_TPOPERSONA
//SP_DEL_TPOPERSONA
//SP_INS_TPOPERSONA
//SP_UPD_TPOPERSONA
//SP_Q_TPOPERSONA