using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class Q_TBLOBJ
    {
        public string tipo { get; set; }
        public string descripcion { get; set; }
        public int visible { get; set; }

        public static IEnumerable<Q_TBLOBJ> getobj(string tipo)
        {
            string Query = "Select * from " + ParametrosGlobales.bd + "q_tblobj where tipo='" + tipo + "' and visible=1";
            List<Q_TBLOBJ> lista = new List<Q_TBLOBJ>();
            DataSet iDataset = ConectDB.Datos_Retorno(Query);
           DataTable oDT = iDataset.Tables[0];
            if (oDT != null)
            { 
                foreach (DataRow oDR in oDT.Rows)
                {
                    Q_TBLOBJ item = new Models.Q_TBLOBJ();
                    item.descripcion = oDR["Descripcion"].ToString();
                    lista.Add(item);
                }
            }

            return lista;
        }
    }
}
