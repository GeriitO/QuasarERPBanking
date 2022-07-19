using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Models
{
    public class Graficos
    {
        public string query { get; set; }
        public DataSet dts()
        {
            DataSet ds = Models.ConectDB.Datos_Retorno(query);

            return ds;
        }


    }
}
