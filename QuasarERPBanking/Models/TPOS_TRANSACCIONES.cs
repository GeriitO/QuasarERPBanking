using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.ComponentModel.DataAnnotations;
using Resources.MANTENIMIENTO;
using System.Web.Mvc;

namespace QuasarERPBanking.Models
{
    public class TPOS_TRANSACCIONES : ConectDB
    {
        [Display(Name = "lblCode", ResourceType = typeof(StringResources))]
        public int TPOCOD { get; set; }

        [Display(Name = "lblDesc", ResourceType = typeof(StringResources))]
        public string DESCRIPCION { get; set; }
        public bool ACTIVO { get; set; }
        public string MODULO { get; set; }
        [Display(Name = "lblOper", ResourceType = typeof(StringResources))]
        public string OPERACION { get; set; }


        //public TPOS_TRANSACCIONES()
        //{
        //    Campo_Clave = "AFTPCOD";
        //    tabla = "AF_TIPOMOV";

        //}

        static string sp_getAll = "SP_ALL_TIPO_TRANS";      //PARA EL DROPDOWNLIST
        static string sp_getAllM = "SP_ALL_TIPTRANMANT";  //PARA EL MANTENIMIENTO

        //para cargar el dropdownlist de los formularios
        public static IEnumerable<TPOS_TRANSACCIONES> GetTransacciones()
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAll, parametros, false);
            List<TPOS_TRANSACCIONES> transacciones = new List<TPOS_TRANSACCIONES>();

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    TPOS_TRANSACCIONES transaccion = new TPOS_TRANSACCIONES();
                    //int codigo = 0;
                    //int.TryParse(item["RAMCOD"].ToString(), out codigo);
                    //ramo.RAMCOD = codigo;
                    transaccion.DESCRIPCION = item["DESCRIPCION"].ToString();
                    transaccion.TPOCOD = int.Parse(item["TPOCOD"].ToString());
                    transacciones.Add(transaccion);
                }
            }

            return transacciones;
        }

        public IEnumerable<TPOS_TRANSACCIONES> GetTransacciones(string modelo)
        {
            ConectDB cn = new ConectDB();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            //parametros.Add(modelo);
            parametros.Add("1");
            List<TPOS_TRANSACCIONES> transacciones = new List<TPOS_TRANSACCIONES>();          
            List<object> reader = cn.execute2(tabla, whereParam(parametros), false);


            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    TPOS_TRANSACCIONES transaccion = new TPOS_TRANSACCIONES();
                   
                    transaccion.DESCRIPCION = item["DESCRIPCION"].ToString();
                    transaccion.TPOCOD = int.Parse(item["TPOCOD"].ToString());
                    transacciones.Add(transaccion);
                }
            }

            return transacciones;
        }



        public TPOS_TRANSACCIONES()
        {
            Campo_Clave = "OPERACION";

            tabla = "TPOS_TRANSACCIONES";

            Campo_Clave2 = "ACTIVO";

        }

        //llenar la lista completa de transacciones para el mantenimiento
        public static List<TPOS_TRANSACCIONES> GetTransaccion()
        {
            List<TPOS_TRANSACCIONES> lstTrans = new List<TPOS_TRANSACCIONES>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc(sp_getAllM, parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    lstTrans.Add(setState(item));
                }
            }
            return lstTrans;
        }

        static TPOS_TRANSACCIONES setState(Row item)
        {
            TPOS_TRANSACCIONES user = new TPOS_TRANSACCIONES
            {
                DESCRIPCION = item["DESCRIPCION"].ToString(),
                TPOCOD = int.Parse(item["TPOCOD"].ToString()),
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false,
            };
            return user;
        }

        //cargar los datos de una transaccion
        protected override ConectDB LoadObject(Rows reader)
        {

            foreach (Row item in reader)
            {
                DESCRIPCION = item["DESCRIPCION"].ToString();
                TPOCOD = int.Parse(item["TPOCOD"].ToString());
                ACTIVO = item["ACTIVO"].ToString() == "1" ? true : false;
                OPERACION = item["OPERACION"].ToString();
            }

            return this;
        }
        protected override ArrayList getParameters()
        {
            ArrayList parametros = new ArrayList(3);
            parametros.Add(DESCRIPCION);
            parametros.Add(TPOCOD);
            parametros.Add(ACTIVO ? "1" : "0");
            parametros.Add(OPERACION);
            return parametros;
        }

        public int BuscarByDesc(string value)
        {
            clsAS400 cn = new clsAS400();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execProc("SP_Q_CODTRANS", arreglo, false);
            int codigo = 0;
            if (reader != null)
            {
                foreach (Row item in reader)
                {
                    codigo = int.Parse(item["TPOCOD"].ToString());
                }

            }
            return codigo;
        }

        public static IEnumerable<SelectListItem> GetOperacion()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "Sin Operación", Value = "N"},
                new SelectListItem{Text = "+", Value = "+" },
                new SelectListItem{Text = "-", Value = "-"},

            };
            return items;
        }




        protected override object whereParam(ArrayList value)
        {
            string QUERY = "";          
            int i = 0;
            foreach (object elementos in value)
            {
                if (i != value.Count - 1)
                {
                    QUERY = Campo_Clave2 + "="+"'"+ elementos+"'";
                }
                else
                {
                    QUERY = Campo_Clave2 + "=" + "'" + elementos + "'";
                    //QUERY = QUERY + " AND "+ Campo_Clave2 +'='+ "'"+ elementos + "'";
                }
                i++;
            }
            return QUERY;
        }
    }
}





//////////////// SP UTILIZADOS ////////////////
//SP_ALL_TIPO_TRANS
//SP_ALL_TIPTRANMANT
//SP_INS_TPOS_TRANS
//SP_DEL_TPOS_TRANS
//SP_UPD_TPOS_TRANS    
//SP_Q_TPOS_TRANS