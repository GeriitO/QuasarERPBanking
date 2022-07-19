using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace QuasarERPBanking.Models
{
    public class Util
    {
        public static IDictionary<string, string> formatos = new Dictionary<string, string>();
        public static string contacto { get; set; }
        public static string det_cc { get; set; }

        //public static string Today = DateTime.Today.ToString("dd/MM/yyyy"); 
        public static string ContactoActual()
        {
            //string contacto = "";
            return contacto;
        }

        public static string FechaQuasar { get; set; }
        //public static string[] formatDate 
        //    }
        public static void loadFormats()
        {

            formatos["es-VE"] = "dd/MM/yyyy";
            formatos["pt-BR"] = "dd/MM/yyyy";
            formatos["en-US"] = "MM/dd/yyyy";
        }



        public static string getip()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;

        }


    }
}