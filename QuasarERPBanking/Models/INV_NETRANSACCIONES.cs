using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class INV_NETRANSACCIONES : ConectDB
    {    


   public string NETRANSAPR { get; set; }    
   public string NETRANSOCCOD { get; set; }    
   public string NETRANSOCELAB { get; set; }    
   public string NETRANSINVCOD { get; set; }    
   public DateTime NETRANSFECHARECIBE { get; set; }    
   public decimal NETRANSCANT { get; set; }    
   public decimal NETRANSCOSTO { get; set; }    
   public string USUARIO { get; set; }    
   public string NETRANSID { get; set; }


        public INV_NETRANSACCIONES()
        {
            //sp_Insert = "SP_INS_NETRANSAC";
            Campo_Clave = "NETRANSID";
        }

        protected override ArrayList getParameters()
        {
            USUARIO = Util.ContactoActual();
            
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(9);
            parametros.Add(NETRANSAPR);
            parametros.Add(NETRANSOCCOD);
            parametros.Add(NETRANSOCELAB);
            parametros.Add(NETRANSINVCOD);
            parametros.Add(NETRANSFECHARECIBE.ToString("yyyy-MM-dd"));
            parametros.Add(NETRANSCANT);
            parametros.Add(NETRANSCOSTO);
            parametros.Add(USUARIO);
            parametros.Add(NETRANSID);           
            return parametros;
        }

        public static string getLote()
        {
            clsAS400 cn = new clsAS400();
            string prefijo = "LM";
            string lote = "";
            ArrayList parametros = new ArrayList(2);
            parametros.Add(prefijo);
            parametros.Add(lote);
            cn.Execute2(ref prefijo, "BICADM01W", "SP_Q_NETRANSOC", 100);
            return prefijo;
        }


    }
}