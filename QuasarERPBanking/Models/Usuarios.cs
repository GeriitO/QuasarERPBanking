using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using Resources;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class Usuarios : ConectDB
    {
        public string USUARIO /*VARCHAR(15)*/ { get; set; }
        public string CLAVE /*VARCHAR(100)*/ { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FECHACREACION { get; set; }

        public string NIVEL /*VARCHAR(2)*/ { get; set; }
        public string STATUS /*VARCHAR(1)*/ { get; set; }
        public string NOMBRE /*VARCHAR(50)*/ { get; set; }
        public string APELLIDO /*VARCHAR(50)*/ { get; set; }
        public string NIVACC /*VARCHAR(15)*/ { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FECHAMODIF { get; set; }
        public string DEPOS/* VARCHAR(200)*/ { get; set; }
        public int CODEMP { get; set; }
        public string CTEINT /*VARCHAR(10)*/ { get; set; }
        public string STATUSCLAVE /* VARCHAR(50)*/ { get; set; }
        public string CLAVECADUCIDAD /*VARCHAR(10)*/ { get; set; }
        public string EMAIL/* VARCHAR(100)*/ { get; set; }
        public string MAYUSCULA /*VARCHAR(1)*/ { get; set; }
        public string MINUSCULA /*VARCHAR(1)*/ { get; set; }
        public string ESPACIO/* VARCHAR(1)*/ { get; set; }
        public string NUMERO /*VARCHAR(1)*/ { get; set; }
        public string ESPECIAL /*VARCHAR(1)*/ { get; set; }
        public string NOREPETIRCAR  /*VARCHAR(1)*/ { get; set; }
        public string RUTASKIN /*VARCHAR(500)*/ { get; set; }
        public string CLAVENOREPETIR/* VARCHAR(10)*/ { get; set; }

        public string strFECHACREACION
        {
            get
            {
                return FECHACREACION.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        public string strFECHAMODIF
        {
            get
            {
                return FECHAMODIF.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        public Usuarios()
        {
            //Campo_Clave = "USUARIO";
        }



        //llenar la lista completa de todos los usuarios en el grid
        public static List<Usuarios> GetUsers()
        {
            List<Usuarios> lstUsers = new List<Usuarios>();
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(0);
            List<object> reader = cn.execProc("SP_Q_USERS", parametros, false);
            if (reader != null)
            {
                foreach (Dictionary<string, object> item in reader)
                {
                    //Usuarios user = new Usuarios
                    //{
                    //    USUARIO = item["USUARIO"].ToString(),
                    //    STATUSCLAVE = item["STATUSCLAVE"].ToString(),
                    //    DEPOS = item["DEPOS"].ToString(),
                    //    APELLIDO = item["APELLIDO"].ToString(),
                    //    CLAVECADUCIDAD = item["CLAVECADUCIDAD"].ToString(),
                    //    CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "0" : item["CODEMP"].ToString()),
                    //    FECHACREACION =(item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now :  DateTime.Parse(item["FECHACREACION"].ToString())),
                    //    FECHAMODIF = DateTime.Parse(item["FECHAMODIF"].ToString()),                       
                    //    CLAVENOREPETIR = item["CLAVENOREPETIR"].ToString(),
                    //    CTEINT = item["CTEINT"].ToString(),
                    //    EMAIL = item["EMAIL"].ToString(),
                    //    ESPACIO = item["ESPACIO"].ToString(),
                    //    ESPECIAL = item["ESPECIAL"].ToString(),
                    //    MAYUSCULA = item["MAYUSCULA"].ToString(),
                    //    MINUSCULA = item["MINUSCULA"].ToString(),
                    //    NIVACC = item["NIVACC"].ToString(),
                    //    NIVEL = item["NIVEL"].ToString(),
                    //    NOMBRE = item["NOMBRE"].ToString(),
                    //    NOREPETIRCAR = item["NOREPETIRCAR"].ToString(),
                    //    NUMERO = item["NUMERO"].ToString(),
                    //    RUTASKIN = item["RUTASKIN"].ToString(),
                    //    STATUS = item["STATUS"].ToString(),
                    //};                   
                    lstUsers.Add(setUser(item));
                }
            }
            return lstUsers;
        }

        static Usuarios setUser(Row item)
        {
            Usuarios user = new Usuarios
            {
                USUARIO = item["USUARIO"].ToString(),
                STATUSCLAVE = item["STATUSCLAVE"].ToString(),
                DEPOS = item["DEPOS"].ToString(),
                APELLIDO = item["APELLIDO"].ToString(),
                CLAVECADUCIDAD = item["CLAVECADUCIDAD"].ToString(),
                CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "0" : item["CODEMP"].ToString()),
                FECHACREACION = (item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(item["FECHACREACION"].ToString())),
                FECHAMODIF = DateTime.Parse(item["FECHAMODIF"].ToString()),
                CLAVENOREPETIR = item["CLAVENOREPETIR"].ToString(),
                CTEINT = item["CTEINT"].ToString(),
                EMAIL = item["EMAIL"].ToString(),
                ESPACIO = item["ESPACIO"].ToString(),
                ESPECIAL = item["ESPECIAL"].ToString(),
                MAYUSCULA = item["MAYUSCULA"].ToString(),
                MINUSCULA = item["MINUSCULA"].ToString(),
                NIVACC = item["NIVACC"].ToString(),
                NIVEL = item["NIVEL"].ToString(),
                NOMBRE = item["NOMBRE"].ToString(),
                NOREPETIRCAR = item["NOREPETIRCAR"].ToString(),
                NUMERO = item["NUMERO"].ToString(),
                RUTASKIN = item["RUTASKIN"].ToString(),
                STATUS = item["STATUS"].ToString(),
            };
            return user;
        }



        protected override ConectDB CargarObjeto(Rows dt)
        {
            foreach (Row item in dt)
            {
                USUARIO = item["USUARIO"].ToString();
                STATUSCLAVE = item["STATUSCLAVE"].ToString();
                DEPOS = item["DEPOS"].ToString();
                APELLIDO = item["APELLIDO"].ToString();
                CLAVECADUCIDAD = item["CLAVECADUCIDAD"].ToString();
                CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "0" : item["CODEMP"].ToString());
                FECHACREACION = (item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(item["FECHACREACION"].ToString()));
                FECHAMODIF = DateTime.Parse(item["FECHAMODIF"].ToString());
                CLAVENOREPETIR = item["CLAVENOREPETIR"].ToString();
                CTEINT = item["CTEINT"].ToString();
                EMAIL = item["EMAIL"].ToString();
                ESPACIO = item["ESPACIO"].ToString();
                ESPECIAL = item["ESPECIAL"].ToString();
                MAYUSCULA = item["MAYUSCULA"].ToString();
                MINUSCULA = item["MINUSCULA"].ToString();
                NIVACC = item["NIVACC"].ToString();
                NIVEL = item["NIVEL"].ToString();
                NOMBRE = item["NOMBRE"].ToString();
                NOREPETIRCAR = item["NOREPETIRCAR"].ToString();
                NUMERO = item["NUMERO"].ToString();
                RUTASKIN = item["RUTASKIN"].ToString();
                STATUS = item["STATUS"].ToString();
            }
            return this;

        }


        //cargar los datos de un usuario

        //protected override ConectDB CargarObjeto(DataTable dt)
        //{
        //    foreach (DataRow item in dt.Rows)
        //    {


        //        USUARIO = item["USUARIO"].ToString();
        //        STATUSCLAVE = item["STATUSCLAVE"].ToString();
        //        DEPOS = item["DEPOS"].ToString();
        //        APELLIDO = item["APELLIDO"].ToString();
        //        CLAVECADUCIDAD = item["CLAVECADUCIDAD"].ToString();
        //        CODEMP = int.Parse(item["CODEMP"].ToString() == string.Empty ? "0" : item["CODEMP"].ToString());
        //        FECHACREACION = (item["FECHACREACION"].ToString() == string.Empty ? DateTime.Now : DateTime.Parse(item["FECHACREACION"].ToString()));
        //        FECHAMODIF = DateTime.Parse(item["FECHAMODIF"].ToString());
        //        CLAVENOREPETIR = item["CLAVENOREPETIR"].ToString();
        //        CTEINT = item["CTEINT"].ToString();
        //        EMAIL = item["EMAIL"].ToString();
        //        ESPACIO = item["ESPACIO"].ToString();
        //        ESPECIAL = item["ESPECIAL"].ToString();
        //        MAYUSCULA = item["MAYUSCULA"].ToString();
        //        MINUSCULA = item["MINUSCULA"].ToString();
        //        NIVACC = item["NIVACC"].ToString();
        //        NIVEL = item["NIVEL"].ToString();
        //        NOMBRE = item["NOMBRE"].ToString();
        //        NOREPETIRCAR = item["NOREPETIRCAR"].ToString();
        //        NUMERO = item["NUMERO"].ToString();
        //        RUTASKIN = item["RUTASKIN"].ToString();
        //        STATUS = item["STATUS"].ToString();

        //    }

        //    return this;
        //}
        //protected override ArrayList getParameters()
        //{
        //    ArrayList parametros = new ArrayList(22);
        //    parametros.Add(USUARIO);
        //    parametros.Add(strFECHACREACION);
        //    parametros.Add(NIVEL);
        //    parametros.Add(STATUS);
        //    parametros.Add(NOMBRE);
        //    parametros.Add(APELLIDO);
        //    parametros.Add(NIVACC);
        //    parametros.Add(strFECHAMODIF);
        //    parametros.Add(DEPOS);
        //    parametros.Add(CODEMP);
        //    parametros.Add(CTEINT);
        //    parametros.Add(STATUSCLAVE);
        //    parametros.Add(CLAVECADUCIDAD);
        //    parametros.Add(EMAIL);
        //    parametros.Add(MAYUSCULA);
        //    parametros.Add(MINUSCULA);
        //    parametros.Add(ESPACIO);
        //    parametros.Add(NUMERO);
        //    parametros.Add(ESPECIAL);
        //    parametros.Add(NOREPETIRCAR);
        //    parametros.Add(RUTASKIN);
        //    parametros.Add(CLAVENOREPETIR);
        //    return parametros;
        //}






    }

}



//////////////// SP UTILIZADOS ////////////////
//SP_Q_USERS
//SP_Q_USUARIOS
//SP_INS_USUARIOS
//SP_DEL_USUARIOS
//SP_UPD_USUARIOS