using Resources.INV_NEMAESTRO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using Rows = System.Collections.Generic.List<object>;
using Row = System.Collections.Generic.Dictionary<string, object>;
using System.Collections;
using System.Data.OleDb;
using System.Data;

namespace QuasarERPBanking.Models
{
    public class INV_NEMAESTRO : ConectDB
    {
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
        public string NECOD { get; set; }

        [Display(Name = "lblStatus", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrStatus")]
        public string NESTATUS { get; set; }

        [Display(Name = "lblFemisi", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFemisi")]
        public DateTime NEEMISION { get; set; }
        public string strNEEMISION       // se utiliza en el autocomplete de la vista
        {
            get
            {
                return NEEMISION.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblCliCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCliCod")]
        public string NECTECOD { get; set; }

        [Display(Name = "lblCont", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCont")]
        public string NECTECONTACTO { get; set; }

        [Display(Name = "lblLuga", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCodLuga")]
        public string NELUGAR { get; set; }

        [Display(Name = "lblServi", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrServi")]
        public string NEOBSERV { get; set; }

        [Display(Name = "lblFemisi", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFelab")]
        public DateTime NEELAFECHA { get; set; }
        public string strNEELAFECHA       // se utiliza en el autocomplete de la vista
        {
            get
            {
                return NEELAFECHA.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblPerson", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrPerson")]
        public string NEELAPERSONA { get; set; }

        [Display(Name = "lblTab", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrTab")]
        public string NETABLA { get; set; }

        [Display(Name = "lblCodElab", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCodElab")]
        public string NECODELAB { get; set; }

        [Display(Name = "lblCC", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCC")]
        public string NECC { get; set; }

        [Display(Name = "lblConta", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrConta")]
        public string NESTATUSCONTA { get; set; }

        [Display(Name = "lblFcont", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFcont")]
        public DateTime NEFECHACONTA { get; set; }
        public string strNEFECHACONTA       // se utiliza en el autocomplete de la vista
        {
            get
            {
                return NEFECHACONTA.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        public INV_FIFO inv_fifo { get; set; }

        public INV_NEMAESTRO()
        {
            Campo_Clave = "NECOD";
        }



        protected override ArrayList getParameters()
        {
            //NEEMISION = DateTime.Parse("-");
            //NEFECHACONTA = DateTime.Parse("-");
            NECOD = "";
            NESTATUS = "Entregada";
            NEELAPERSONA = Util.ContactoActual();
            NETABLA = "I";
            NESTATUSCONTA = "-";
            NECODELAB = "";

            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(14);
            parametros.Add(NECOD);
            parametros.Add(NESTATUS);
            parametros.Add(NEEMISION.ToString("yyyy-MM-dd"));
            parametros.Add(NECTECOD);
            parametros.Add(NECTECONTACTO);
            parametros.Add(NELUGAR);
            parametros.Add(NEOBSERV);
            parametros.Add(NEELAFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(NEELAPERSONA);
            parametros.Add(NETABLA);
            parametros.Add(NECODELAB);
            parametros.Add(NECC);
            parametros.Add(NESTATUSCONTA);
            parametros.Add(NEFECHACONTA.ToString("yyyy-MM-dd"));
            return parametros;
        }


        #region GetPendientes
        public List<INV_NEMAESTRO> GetPendientes(string usuario)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            List<INV_NEMAESTRO> pendientes = new List<INV_NEMAESTRO>();
            string consulta = "SELECT DISTINCT INVCTEINT FROM "+ ParametrosGlobales.bd + "INV_TEMP WHERE USUARIO = '" + usuario + "'";
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    Models.INV_NEMAESTRO pendiente = new Models.INV_NEMAESTRO();
                    pendiente.NECTECOD = item["INVCTEINT"].ToString();
                    pendientes.Add(pendiente);
                }
            }

            return pendientes;
        }
        #endregion


        public static int insertne(string contacto, string lugar, string cliente, string observaciones, string ccosto, string fecha)
        {

            ConectDB cn = new ConectDB();
            //string exists = "";                  
            string quary = "ejemplo";
            INV_NEMAESTRO inv_nemaestro = new INV_NEMAESTRO
            {
                NECTECONTACTO = contacto,
                NELUGAR = lugar,
                NECTECOD = cliente,
                NEOBSERV = observaciones,
                NECC = ccosto,
                NEELAFECHA = DateTime.Parse(fecha),
            };

            int afectado = inv_nemaestro.Insert(quary);
            return afectado;
        }



        public static string getTemp(string codigo, decimal cantidad, string cliente, string deposito, int opc)
        {
            clsAS400 cn = new clsAS400();
            System.Collections.ArrayList parametros = new System.Collections.ArrayList(6);
            parametros.Add(codigo);
            parametros.Add(cantidad);
            parametros.Add(Util.ContactoActual());
            parametros.Add(cliente);
            parametros.Add(deposito);
            parametros.Add(Util.getip());
            string exists = "";
            switch (opc)
            {
                case 1:
                    Rows reader = cn.execProc("SP_Q_INVTEMP", parametros, false);
                    if (reader != null)
                    {
                        if (reader.Count > 0)
                        {
                            cn.execProc("SP_UPD_INVTEMP", parametros, true);
                        }
                        else
                        {
                            cn.execProc("SP_INS_INVTEMP", parametros, true);
                        }
                    }
                    break;
                case 2:
                    cn.execProc("SP_UPD_INVTEMP", parametros, true);
                    break;
                case 3:
                    cn.execProc("SP_DEL_INVTEMP", parametros, true);
                    break;
                case 4:
                    cn.execProc("SP_DEL_INVTEMP", parametros, true);
                    cn.execProc("SP_INS_INVTEMP", parametros, true);
                    break;
                case 5:
                    Rows reader2 = cn.execProc("SP_Q_INVTEMP", parametros, false);
                    if (reader2 != null)
                    {
                        if (reader2.Count > 0)
                        {
                            exists = "1";
                        }
                        else
                        {
                            exists = "2";
                        }
                    }
                    break;
                case 6:
                    cn.execProc("SP_DEL_INVTEMPLOAD", parametros, true);
                    break;

                default:
                    break;
            }
            return exists;
        }





    }
}



//SP_Q_NEPEND
//SP_Q_INVTEMP
//SP_UPD_INVTEMP
//SP_INS_INVTEMP
//SP_DEL_INVTEMP
//SP_DEL_INVTEMPLOAD