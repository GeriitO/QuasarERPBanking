using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources.USUARIO;

namespace QuasarERPBanking.Models
{
    //Clase necesaria para hacer login, no se guarda en BD
    public class Usuario
    {
        public long ID { get; set; }
        [Display(Name = "lblUserLogOn", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errUserLogOn")]
        [NotMapped]
        public string usuario { get; set; }
        [Display(Name = "lblKeyLogOn", ResourceType = typeof(StringResources))]
        [DataType(DataType.Password)]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "errKeyLogOn")]
        [NotMapped]
        public string clave { get; set; }
        //[Column("cont_id")]
        public long ContactoID { get; set; }

        public List<string> GetUsers(string nombre)
        {
            List<string> lstUsers = new List<string>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(nombre);

            List<object> reader = cn.execProc("SP_Q_USUARIO", parametros, false);

            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    string user = "";
                    user = item["USUARIO"].ToString();
                    lstUsers.Add(user);
                }
            }

            return lstUsers;
        }
   
        /*     public string toJSON()
        {
           return JsonConvert.SerializeObject(this,
                            new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
        } */
    }
}


//////////////// SP UTILIZADOS ////////////////
//SP_Q_USUARIO

