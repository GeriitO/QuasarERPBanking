using Resources.ActivoFijo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Row = System.Collections.Generic.Dictionary<string, object>;
using Rows = System.Collections.Generic.List<object>;
using System.Data.OleDb;
using System.Data;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Models
{
    public class ActivoFijo : ConectDB
    {
        [Display(Name = "lblCod", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCod")]
        //[Required(ErrorMessage = "El Campo del Codigo del Producto esta vacio.")]
        //[Display(Name = "*Codigo:")]
        public string AF_CODACT { get; set; }          //PRODUC ASOCIA- codigo del producto

        [Display(Name = "lblEmpl", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrEmpl")]
        //[Display(Name = "Empleado:")]
        public string AF_CODEMPL { get; set; }        //DATOS DE ASIG- codigo del empleado

        [Display(Name = "lblSerial", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrSerial")]
        //[Display(Name = "*serial:")]
        public string AF_SERIAL { get; set; }          //DATOS ACTIVO -SERIAL

        public string AF_SERIAL_ { get; set; }


        [Display(Name = "lblFecha", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFech")]
        //[Display(Name = "Fecha:")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AF_FECH { get; set; }            //FECHA DE CREACION
        public string strAFFECH       // se utiliza en el autocomplete de la vista
        {
            get
            {
                return AF_FECH.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }


        [Display(Name = "lblFechAsig", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFechAsig")]
        //[Display(Name = "Fecha:")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AF_FECHREAL { get; set; }        // DATOS DE ASIGNACION

        public string strAFFECHREAL       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AF_FECHREAL.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        [Display(Name = "lblCenCos", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCenCos")]
        //[Display(Name = "*Centro de Costo:")]
        public string AF_CCTO { get; set; }            //DATOS DE ASIGNACION- CENTRO DE COSTO 

        [Display(Name = "lblUsAsig", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrUsAsig")]
        //[Display(Name = "USUARIO QUE ASIGNÓ:")]
        public string AF_USUARIO { get; set; }         //USUARIO QUE ASIGNO - NOMBRE


        [Display(Name = "lblNumAct", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNumAct")]
        //[Display(Name = "*N° de Activo:")]
        //[Required(ErrorMessage = "Debe ingresar un numero de activo para su consulta.")]
        public string AF_ETIQ { get; set; }            //DATOS DE ACTIVO - NUMERO DE ACTIVO 

        public string AF_ETIQ_ { get; set; }
        [Display(Name = "lblFecIni", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecIni")]
        //[Display(Name = "Inicio:")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AF_DEPINI { get; set; }          // DATOS DE DEPRECIASION - FECHA DE INICIO

        public string strAFDEPINI       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AF_DEPINI.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }


        [Display(Name = "lblFecUlt", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecUlt")]
        //[Display(Name = "Ultimo Calculo:")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AF_DEPULT { get; set; }           //DATOS DE DEPRECIACION- FECHA FINAL

        public string strAFDEPULT       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AF_DEPULT.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }


        [Display(Name = "lblMesDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMesDep")]
        //[Display(Name = "Meses a Depreciar:")]
        public int AF_DEPMESES { get; set; }        //DAT DEP- NUMERO DE MESES A DEPRE

        [Display(Name = "lblMeYaDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMeYaDep")]
        //[Display(Name = "Meses Ya Depreciados:")]
        public int AF_DEPMESESYA { get; set; }      //DAT DEP- MESES YA DEPR

        public double AF_ORGCOSTO { get; set; }        //OTRA TABLA

        public DateTime AF_ORGFECHA { get; set; }        // fecha OTRA TABLA

        public int AF_ORGMESESDEP { get; set; }    //OTRA TABLA


        [Display(Name = "lblFecAdq", ResourceType = typeof(StringResources))]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFecAdq")]
        //[Display(Name = "Fecha:")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AF_ADQFECHA { get; set; }        // DAT ADQU- FECHA DE ADQUI

        public string strAFADQFECHA       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AF_ADQFECHA.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }


        [Display(Name = "lblOrdCom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrOrdCom")]
        //[Display(Name = "Orden de Compra:")]
        public string AF_ADQOC { get; set; }           // DAT ADQ- ORDEN DE COMPRA

        [Display(Name = "lblNumFac", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNumFac")]
        //[Display(Name = "N° Factura:")]
        public string AF_ADQFACT { get; set; }         //DAT ADQ- NUM FACTURA


        [Display(Name = "lblNumCom", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNumCom")]
        //[Display(Name = "N° Comprobante:")]
        public string AF_ADQCOMP { get; set; }         //DAT ADQ- NUMERO COMPROBANTE

        public string AF_ADQCONT { get; set; }         // NUNCA SE UTILIZA 

        public DateTime AF_ADQVIGD { get; set; }         // SON FECHAS DE OTRA TABLA

        public DateTime AF_ADQVIGH { get; set; }         // SON FECHAS DE OTRA TABLA

        [Display(Name = "lblCodProv", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCodProv")]
        //[Display(Name = "Proveedor:")]
        public string AF_ADQPROV { get; set; }        //DAT ADQ- CODIGO DE PROVEEDOR 

        [Display(Name = "lblCuenAct", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCuenAct")]
        //[Display(Name = "Activo:")]
        public string AF_CTAACT { get; set; }          // CTA CONT- ACTIVO

        [Display(Name = "lblGasDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrGasDep")]
        //[Display(Name = "Gasto de Depreciación:")]
        public string AF_CTAGTODEP { get; set; }       //CTA CONT- GASTOS DEPRE

        [Display(Name = "lblCueDep", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCueDep")]
        //[Display(Name = "Depreciación:")]
        public string AF_CTADEP { get; set; }         //CTA CONT- DEPRECIA

        [Display(Name = "lblValor", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrValor")]
        //[Display(Name = "Valor Actual:")]
        public double AF_VALACT { get; set; }          //MONT ACTIVO- VALOR ACTUAL

        [Display(Name = "lblValorRes", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrValorRes")]
        //[Display(Name = "Valor Residual:")]
        public double AF_VALRES { get; set; }         //MONT ACTIV- VALO RESID

        [Display(Name = "lblValorOrig", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrValorOrig")]
        //[Display(Name = "Valor Original:")]
        public double AF_VALORI { get; set; }          // MONT ACTIV- VALO ORIGINAL

        [Display(Name = "lblDepAcum", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDepAcum")]
        //[Display(Name = "Depreciacion Acumulada:")]
        public double AF_DEPACC { get; set; }          //MONT ACTIV- DEPRE ACUMULAD

        public double AF_DEPSEM { get; set; }          //DE OTRA TABLA

        [Display(Name = "lblAlicuota", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrAlicuota")]
        //[Display(Name = "Alicuota:")]
        public double AF_ALICUOTA { get; set; }        //MONT ACTI- ALICUOTA 

        [Display(Name = "lblValorLib", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrValorLib")]
        //[Display(Name = "Valor Libre:")]
        public double AF_VALLIB { get; set; }          //MONT ACT- VAL LIBRE

        public int AF_CANT { get; set; }            //DE OTRA TABLA


        public DateTime AF_MODIFFECHA { get; set; }      //FECHA DE MODIFICACION DE OTRA TABLA

        public TimeSpan AF_MODIFHORA { get; set; }       //hora NUNCA SE UTILIZA   es time

        //[Display(Name = "lblNomMod", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrNomMod")]
        public string AF_MODIFUSUA { get; set; }      //NOMBRE DE QUIEN LO MODIFICA 

        public DateTime AF_DESFECHA { get; set; }        //FECHA DE OTRA TABLA

        //[Display(Name = "lblStatus", ResourceType = typeof(StringResources))]
        //[Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrStatus")]
        public string AF_DES { get; set; }         //ASIGNADO O REASIGNADO DE OTRA TABLA

        [Display(Name = "lblMejora", ResourceType = typeof(StringResources))]
        public double AF_MEJORA { get; set; }          //NUMERICO DE OTRA TABLA
        [Display(Name = "lblPVP", ResourceType = typeof(StringResources))]
        public double AF_PVP { get; set; }             //NUMERICO DE OTRA TABLA

        [Display(Name = "lblCostUni", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCostUni")]
        //[Display(Name = "Costo Unitario:")]
        public double AF_CTOUNIT { get; set; }         //DATOS DE ACTIVO- COSTO UNITARIO            

        public int AF_TPOGAR { get; set; }          // NO SE UTILIZA

        public string AF_NROGAR { get; set; }         // NO SE UTILIZA

        public string AF_PROVGAR { get; set; }         // DE OTRA TABLA ES NUMERICA

        public string AF_TELPROVGAR { get; set; }     // LABEL PROVEEDOR DEL BOTON CONTIENE

        public string AF_DIRGAR { get; set; }         // NO SE UTILIZA
        [Display(Name = "lblMarca", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrMarca")]
        //[Display(Name = "Marca:")]
        public string AF_MARCA { get; set; }          //DATOS DE LA ASIG-MARCA
        [Display(Name = "lblModelo", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrModelo")]
        //[Display(Name = "Modelo:")]
        public string AF_MOD { get; set; }           // DATOS DE LA ASIG-MODELO

        public DateTime AF_FEVENC { get; set; }           //fecha DE OTRA TABLA FECHA DE VENCIMIENTO 

        public string AF_OBS { get; set; }             //SE UTILIZA EN OBSERVACIONES DEL BOTON CONTIENE
        [Display(Name = "lblUbic", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrUbic")]
        //[Display(Name = "Ubicación:")]
        public string AF_UBICACION { get; set; }      //DE OTRA UBICACION
        [Display(Name = "lblPertA", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrPertA")]
        //[Display(Name = "Pertenece a:")]
        public string AF_CODPERT { get; set; }         //DATOS ACTIVO PERTENECE A 


        [Display(Name = "lblDeprec", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrDeprec")]
        public bool AF_DEPREC { get; set; }          //depreciar CheckBox

        public decimal AF_DEPANT { get; set; }

        public decimal AF_ULTDEP { get; set; }

        public string AF_BIENNAC { get; set; }

        public string AF_PARAM1 { get; set; }

        public string AF_DESCBIEN { get; set; }

        public string AF_CLASBIEN { get; set; }

        public int AF_ETIQ_IMPRE { get; set; }

        public string AFCTAACTOLD { get; set; }

        public string AFCTADEPOLD { get; set; }

        public string AFCTAGTOOLD { get; set; }

        public string AF_ETIQ_CONCILIA { get; set; }

        public string AF_COLOR { get; set; }

        public string AF_MATERIAL { get; set; }

        [Display(Name = "lblFechCal", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrFechCal")]
        //[Display(Name = "Fecha Final:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-yyyy}", ApplyFormatInEditMode = false)]
        public DateTime AFECHACAL { get; set; }      // PARA LA FECHA A CALCULAR
        public string strAFECHACAL       // este nombre se utiliza en el autocomplete de la vista
        {
            get
            {
                return AFECHACAL.ToString(Util.formatos[ParametrosGlobales.culture]);
            }
        }

        public string CC_NOMB { get; set; }

        public string UNIDAD { get; set; }  //DE Productos PARA UNIDAD DE PRODUCTO ASOCIADO

        public string NOM { get; set; }  //DE Productos PARA NOMBRE DE PRODUCTO ASOCIADO

        public string AFGRUDES { get; set; }  //DE ActivoFijoGrupos PARA GRUPO DE PRODUCTO ASOCIADO


        public Productos af_maestro { get; set; }
        public ActivoFijoGrupos ActivoFijoGrupos { get; set; }


        [Display(Name = "lblCantidad", ResourceType = typeof(StringResources))]
        [Required(ErrorMessageResourceType = typeof(StringResources), ErrorMessageResourceName = "ErrCantidad")]
        //[Display(Name = "Cantidad:")]
        public string cantidad { get; set; }




        static string sp_FindAFCODACT = "SP_Q_PROAFCODACT"; //PARA LLENAR GRIP Productos POR CODACTI


        public ActivoFijo()
        {
            //sp_Insert = "SP_INS_AF_ASIGNA";   //INSERTAR
            //sp_Update = "SP_UPD_AF_ASIGNA";   //EDITAR
            //sp_Delete = "SP_DEL_AF_ASIGNA";   //ELIMINAR
            /* sp_Find = "SP_Q_AF_ASIGNA";  */     //BUSCAR   SE CAMBIO SP_Q_PRUEBA POR ESTE 
            tabla = "AF_ASIGNA";
            Campo_Clave = "AF_ETIQ";
        }

        //SE UTILIZA PARA INSERT Y UPDATE
        protected override ArrayList getParameters()
        {
            AF_USUARIO = Util.ContactoActual();          //QUIEN LO CREO NO SE MODIFICA  
            AFCTAACTOLD = "";
            AFCTADEPOLD = "";
            AFCTAGTOOLD = "";
            AF_BIENNAC = "";
            AF_CLASBIEN = "";
            AF_COLOR = "";
            AF_DES = "Asignado";
            AF_DESCBIEN = "";
            AF_DIRGAR = "";
            AF_ETIQ_CONCILIA = "";
            AF_ADQCONT = "";
            //AF_CTAACT = ActivoFijoGrupos.AFGRUCTAACT;
            //AF_CTAGTODEP = ActivoFijoGrupos.AFGRUCTAGTODEP;
            //AF_CTADEP = ActivoFijoGrupos.AFGRUCTADEP;
            AF_CANT = 1;
            AF_MODIFUSUA = Util.ContactoActual();           //ULTIMO QUE LO MODIFICO
            AF_NROGAR = "";
            AF_PROVGAR = "";
            AF_TELPROVGAR = "";
            AF_OBS = "";
            //AF_CODPERT = "";
            AF_PARAM1 = "";
            AF_MATERIAL = "";



            ArrayList parametros = new ArrayList(67);


            parametros.Add(AF_CODACT);
            parametros.Add(AF_CODEMPL);
            parametros.Add(AF_SERIAL);
            parametros.Add(AF_FECH.ToString("yyyy-MM-dd"));
            parametros.Add(AF_FECHREAL.ToString("yyyy-MM-dd"));
            parametros.Add(AF_CCTO);
            parametros.Add(AF_USUARIO);
            parametros.Add(AF_ETIQ);
            parametros.Add(AF_DEPINI.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DEPULT.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DEPMESES);
            parametros.Add(AF_DEPMESESYA);
            parametros.Add(AF_ORGCOSTO);
            parametros.Add(AF_ORGFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ORGMESESDEP);
            parametros.Add(AF_ADQFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQOC);
            parametros.Add(AF_ADQFACT);
            parametros.Add(AF_ADQCOMP);
            parametros.Add(AF_ADQCONT);
            parametros.Add(AF_ADQVIGD.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQVIGH.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQPROV);
            parametros.Add(AF_CTAACT);
            parametros.Add(AF_CTAGTODEP);
            parametros.Add(AF_CTADEP);
            parametros.Add(AF_VALACT.ToString().Replace(",", "."));
            parametros.Add(AF_VALRES);
            parametros.Add(AF_VALORI.ToString().Replace(",", "."));
            parametros.Add(AF_DEPACC.ToString().Replace(",", "."));
            parametros.Add(AF_DEPSEM);
            parametros.Add(AF_ALICUOTA.ToString().Replace(",", "."));
            parametros.Add(AF_VALLIB.ToString().Replace(",", "."));
            parametros.Add(AF_CANT);
            parametros.Add(AF_MODIFFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_MODIFHORA);
            parametros.Add(AF_MODIFUSUA);
            parametros.Add(AF_DESFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DES);
            parametros.Add(AF_MEJORA);
            parametros.Add(AF_PVP);
            parametros.Add(AF_CTOUNIT.ToString().Replace(",", "."));
            parametros.Add(AF_TPOGAR);
            parametros.Add(AF_NROGAR);
            parametros.Add(AF_PROVGAR);
            parametros.Add(AF_TELPROVGAR);
            parametros.Add(AF_DIRGAR);
            parametros.Add(AF_MARCA);
            parametros.Add(AF_MOD);
            parametros.Add(AF_FEVENC.ToString("yyyy-MM-dd"));
            parametros.Add(AF_OBS);
            parametros.Add(AF_UBICACION);
            parametros.Add(AF_CODPERT);
            parametros.Add(AF_DEPREC ? "1" : "0");
            parametros.Add(AF_DEPANT.ToString().Replace(",", "."));
            parametros.Add(AF_ULTDEP.ToString().Replace(",", "."));
            parametros.Add(AF_BIENNAC);
            parametros.Add(AF_PARAM1);
            parametros.Add(AF_DESCBIEN);
            parametros.Add(AF_CLASBIEN);
            parametros.Add(AF_ETIQ_IMPRE);
            parametros.Add(AFCTAACTOLD);
            parametros.Add(AFCTADEPOLD);
            parametros.Add(AFCTAGTOOLD);
            parametros.Add(AF_ETIQ_CONCILIA);
            parametros.Add(AF_COLOR);
            parametros.Add(AF_MATERIAL);
            return parametros;
        }


        protected ArrayList getParameters2()
        {
            AF_ORGCOSTO = 0;
            //AF_ORGFECHA = "";
            AF_ORGMESESDEP = 0;
            //AF_ADQVIGD
            //AF_CTAACT = "0";
            //AF_CTAGTODEP = "0";
            AF_DEPSEM = 0;
            AF_CANT = 1;
            AF_USUARIO = Util.ContactoActual();
            //AF_ETIQ = "0";
            AF_MODIFUSUA = Util.ContactoActual();
            AF_TPOGAR = 0;
            AF_DEPANT = 0;
            AF_ULTDEP = 0;
            AF_ETIQ_IMPRE = 1;
            AF_DES = "Asignado";
            //AF_MODIFHORA = TimeSpan.TicksPerHour;


            //AF_USUARIO = Util.ContactoActual();          //QUIEN LO CREO NO SE MODIFICA  
            AFCTAACTOLD = "";
            AFCTADEPOLD = "";
            AFCTAGTOOLD = "";
            AF_BIENNAC = "";
            AF_CLASBIEN = "";
            AF_COLOR = "";
            //AF_DES = "Asignado";
            AF_DESCBIEN = "";
            AF_DIRGAR = "";
            AF_ETIQ_CONCILIA = "";
            AF_ADQCONT = "";
            //AF_CTAACT = ActivoFijoGrupos.AFGRUCTAACT;
            //AF_CTAGTODEP = ActivoFijoGrupos.AFGRUCTAGTODEP;
            //AF_CTADEP = ActivoFijoGrupos.AFGRUCTADEP;
            //AF_CANT = 1;        
            //AF_MODIFUSUA = Util.ContactoActual();           //ULTIMO QUE LO MODIFICO
            AF_NROGAR = "";
            AF_PROVGAR = "";
            AF_TELPROVGAR = "";
            AF_OBS = "";
            AF_CODPERT = "";
            AF_PARAM1 = "";
            AF_MATERIAL = "";
            ////AF_MODIFHORA = null;


            ArrayList parametros = new ArrayList(67);

            parametros.Add(cantidad);
            parametros.Add(AF_CODACT);
            parametros.Add(AF_CODEMPL);
            parametros.Add(AF_SERIAL);
            parametros.Add(AF_FECH.ToString("yyyy-MM-dd"));
            parametros.Add(AF_FECHREAL.ToString("yyyy-MM-dd"));
            parametros.Add(AF_CCTO);
            parametros.Add(AF_USUARIO);
            //parametros.Add(AF_ETIQ);
            parametros.Add(AF_DEPINI.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DEPULT.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DEPMESES);
            parametros.Add(AF_DEPMESESYA);
            parametros.Add(AF_ORGCOSTO);
            parametros.Add(AF_ORGFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ORGMESESDEP);
            parametros.Add(AF_ADQFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQOC);
            parametros.Add(AF_ADQFACT);
            parametros.Add(AF_ADQCOMP);
            parametros.Add(AF_ADQCONT);
            parametros.Add(AF_ADQVIGD.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQVIGH.ToString("yyyy-MM-dd"));
            parametros.Add(AF_ADQPROV);
            parametros.Add(AF_CTAACT);
            parametros.Add(AF_CTAGTODEP);
            parametros.Add(AF_CTADEP);
            parametros.Add(AF_VALACT.ToString().Replace(",", "."));
            parametros.Add(AF_VALRES);
            parametros.Add(AF_VALORI.ToString().Replace(",", "."));
            parametros.Add(AF_DEPACC);
            parametros.Add(AF_DEPSEM);
            parametros.Add(AF_ALICUOTA);
            parametros.Add(AF_VALLIB);
            parametros.Add(AF_CANT);
            parametros.Add(AF_MODIFFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_MODIFHORA);
            parametros.Add(AF_MODIFUSUA);
            parametros.Add(AF_DESFECHA.ToString("yyyy-MM-dd"));
            parametros.Add(AF_DES);
            parametros.Add(AF_MEJORA);
            parametros.Add(AF_PVP.ToString().Replace(",", "."));
            parametros.Add(AF_CTOUNIT.ToString().Replace(",", "."));
            parametros.Add(AF_TPOGAR);
            parametros.Add(AF_NROGAR);
            parametros.Add(AF_PROVGAR);
            parametros.Add(AF_TELPROVGAR);
            parametros.Add(AF_DIRGAR);
            parametros.Add(AF_MARCA);
            parametros.Add(AF_MOD);
            parametros.Add(AF_FEVENC.ToString("yyyy-MM-dd"));
            parametros.Add(AF_OBS);
            parametros.Add(AF_UBICACION);
            parametros.Add(AF_CODPERT);
            parametros.Add(AF_DEPREC ? 1 : 0);
            parametros.Add(AF_DEPANT);
            parametros.Add(AF_ULTDEP);
            parametros.Add(AF_BIENNAC);
            parametros.Add(AF_PARAM1);
            parametros.Add(AF_DESCBIEN);
            parametros.Add(AF_CLASBIEN);
            parametros.Add(AF_ETIQ_IMPRE);
            parametros.Add(AFCTAACTOLD);
            parametros.Add(AFCTADEPOLD);
            parametros.Add(AFCTAGTOOLD);
            parametros.Add(AF_ETIQ_CONCILIA);
            parametros.Add(AF_COLOR);
            parametros.Add(AF_MATERIAL);


            return parametros;
        }


        #region PROCEDURE
        protected ActivoFijo LoadObject(Row item, int nroRS)
        {
            switch (nroRS)
            {
                case 1:

                    AF_CODACT = item["AF_CODACT"].ToString();
                    AF_CODEMPL = item["AF_CODEMPL"].ToString();
                    AF_SERIAL = item["AF_SERIAL"].ToString();
                    AF_FECH = DateTime.Parse(item["AF_FECH"].ToString());
                    AF_FECHREAL = DateTime.Parse(item["AF_FECHREAL"].ToString());
                    AF_CCTO = item["AF_CCTO"].ToString();
                    AF_USUARIO = item["AF_USUARIO"].ToString();
                    AF_ETIQ = item["AF_ETIQ"].ToString();
                    AF_DEPINI = DateTime.Parse(item["AF_DEPINI"].ToString());
                    AF_DEPULT = DateTime.Parse(item["AF_DEPULT"].ToString());
                    AF_DEPMESES = int.Parse(item["AF_DEPMESES"].ToString());
                    AF_DEPMESESYA = int.Parse(item["AF_DEPMESESYA"].ToString());
                    //AF_ORGCOSTO= double.Parse(item["AF_ORGCOSTO"].ToString()); 		
                    //AF_ORGFECHA= DateTime.Parse(item["AF_ORGFECHA"].ToString()); 		
                    //AF_ORGMESESDEP= int.Parse(item["AF_ORGMESESDEP"].ToString()); 	
                    AF_ADQFECHA = DateTime.Parse(item["AF_ADQFECHA"].ToString());
                    AF_ADQOC = item["AF_ADQOC"].ToString();
                    AF_ADQFACT = item["AF_ADQFACT"].ToString();
                    AF_ADQCOMP = item["AF_ADQCOMP"].ToString();
                    AF_ADQCONT = item["AF_ADQCONT"].ToString();
                    //AF_ADQVIGD= DateTime.Parse(item["AF_ADQVIGD"].ToString()); 		
                    //AF_ADQVIGH= DateTime.Parse(item["AF_ADQVIGH"].ToString()); 		
                    AF_ADQPROV = item["AF_ADQPROV"].ToString();
                    AF_CTAACT = item["AF_CTAACT"].ToString();
                    AF_CTAGTODEP = item["AF_CTAGTODEP"].ToString();
                    AF_CTADEP = item["AF_CTADEP"].ToString();
                    AF_VALACT = ConvDoble(item["AF_VALACT"].ToString());
                    AF_VALRES = ConvDoble(item["AF_VALRES"].ToString());
                    AF_VALORI = ConvDoble(item["AF_VALORI"].ToString());
                    AF_DEPACC = ConvDoble(item["AF_DEPACC"].ToString());
                    AF_DEPSEM = ConvDoble(item["AF_DEPSEM"].ToString());
                    AF_ALICUOTA = ConvDoble(item["AF_ALICUOTA"].ToString());
                    AF_VALLIB = ConvDoble(item["AF_VALLIB"].ToString());
                    AF_CANT = int.Parse(item["AF_CANT"].ToString());
                    //AF_MODIFFECHA= DateTime.Parse(item["AF_MODIFFECHA"].ToString()); 	
                    //AF_MODIFHORA= TimeSpan.Parse(item["AF_MODIFHORA"].ToString()); 	
                    AF_MODIFUSUA = item["AF_MODIFUSUA"].ToString();
                    //AF_DESFECHA= DateTime.Parse(item["AF_DESFECHA"].ToString()); 		
                    AF_DES = item["AF_DES"].ToString();
                    //AF_MEJORA= int.Parse(item["AF_MEJORA"].ToString()); 		
                    //AF_PVP= double.Parse(item["AF_PVP"].ToString()); 			
                    AF_CTOUNIT = ConvDoble(item["AF_CTOUNIT"].ToString());
                    //AF_TPOGAR= int.Parse(item["AF_TPOGAR"].ToString()); 		
                    AF_NROGAR = item["AF_NROGAR"].ToString();
                    AF_PROVGAR = item["AF_PROVGAR"].ToString();
                    AF_TELPROVGAR = item["AF_TELPROVGAR"].ToString();
                    AF_DIRGAR = item["AF_DIRGAR"].ToString();
                    AF_MARCA = item["AF_MARCA"].ToString();
                    AF_MOD = item["AF_MOD"].ToString();
                    //AF_FEVENC= DateTime.Parse(item["AF_FEVENC"].ToString()); 		
                    AF_OBS = item["AF_OBS"].ToString();
                    AF_UBICACION = item["AF_UBICACION"].ToString();
                    AF_CODPERT = item["AF_CODPERT"].ToString();
                    AF_DEPREC = item["AF_DEPREC"].ToString() == "1" ? true : false;
                    AF_DEPANT = decimal.Parse(item["AF_DEPANT"].ToString());
                    AF_ULTDEP = decimal.Parse(item["AF_ULTDEP"].ToString());
                    AF_BIENNAC = item["AF_BIENNAC"].ToString();
                    AF_PARAM1 = item["AF_PARAM1"].ToString();
                    AF_DESCBIEN = item["AF_DESCBIEN"].ToString();
                    AF_CLASBIEN = item["AF_CLASBIEN"].ToString();
                    //AF_ETIQ_IMPRE= int.Parse(item["AF_ETIQ_IMPRE"].ToString()); 	no siempre viene vacio
                    AFCTAACTOLD = item["AFCTAACTOLD"].ToString();
                    AFCTADEPOLD = item["AFCTADEPOLD"].ToString();
                    AFCTAGTOOLD = item["AFCTAGTOOLD"].ToString();
                    AF_ETIQ_CONCILIA = item["AF_ETIQ_CONCILIA"].ToString();
                    AF_COLOR = item["AF_COLOR"].ToString();
                    AF_MATERIAL = item["AF_MATERIAL"].ToString();
                    AFECHACAL = AF_DEPINI.AddMonths(AF_DEPMESES);

                    break;
                case 2:
                    if (this.af_maestro == null)
                    {
                        this.af_maestro = new Productos();
                    }
                    this.af_maestro.AFCOD = item["AFCOD"].ToString();
                    this.af_maestro.AFNOM = item["AFNOM"].ToString();
                    this.af_maestro.AFUNIDAD = item["AFUNIDAD"].ToString();
                    break;
                case 3:
                    if (this.ActivoFijoGrupos == null)
                    {
                        this.ActivoFijoGrupos = new ActivoFijoGrupos();
                    }
                    this.ActivoFijoGrupos.AFGRUDES = item["AFGRUDES"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAACT = item["AFGRUCTAACT"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTADEP = item["AFGRUCTADEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAGTODEP = item["AFGRUCTAGTODEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUMESES = int.Parse(item["AFGRUMESES"].ToString());
                    break;


                default:
                    break;
            }

            return this;
        }
        #endregion
        protected ActivoFijo CargarObjeto(DataRow item, int nroRS)
        {
            switch (nroRS)
            {
                case 1:

                    AF_CODACT = item["AF_CODACT"].ToString();
                    AF_CODEMPL = item["AF_CODEMPL"].ToString();
                    AF_SERIAL = item["AF_SERIAL"].ToString();
                    AF_FECH = DateTime.Parse(item["AF_FECH"].ToString());
                    AF_FECHREAL = DateTime.Parse(item["AF_FECHREAL"].ToString());
                    AF_CCTO = item["AF_CCTO"].ToString();
                    AF_USUARIO = item["AF_USUARIO"].ToString();
                    AF_ETIQ = item["AF_ETIQ"].ToString();
                    AF_DEPINI = DateTime.Parse(item["AF_DEPINI"].ToString());
                    AF_DEPULT = DateTime.Parse(item["AF_DEPULT"].ToString());
                    AF_DEPMESES = int.Parse(item["AF_DEPMESES"].ToString());
                    AF_DEPMESESYA = int.Parse(item["AF_DEPMESESYA"].ToString());
                    //AF_ORGCOSTO= double.Parse(item["AF_ORGCOSTO"].ToString()); 		
                    //AF_ORGFECHA= DateTime.Parse(item["AF_ORGFECHA"].ToString()); 		
                    //AF_ORGMESESDEP= int.Parse(item["AF_ORGMESESDEP"].ToString()); 	
                    AF_ADQFECHA = DateTime.Parse(item["AF_ADQFECHA"].ToString());
                    AF_ADQOC = item["AF_ADQOC"].ToString();
                    AF_ADQFACT = item["AF_ADQFACT"].ToString();
                    AF_ADQCOMP = item["AF_ADQCOMP"].ToString();
                    AF_ADQCONT = item["AF_ADQCONT"].ToString();
                    //AF_ADQVIGD= DateTime.Parse(item["AF_ADQVIGD"].ToString()); 		
                    //AF_ADQVIGH= DateTime.Parse(item["AF_ADQVIGH"].ToString()); 		
                    AF_ADQPROV = item["AF_ADQPROV"].ToString();
                    AF_CTAACT = item["AF_CTAACT"].ToString();
                    AF_CTAGTODEP = item["AF_CTAGTODEP"].ToString();
                    AF_CTADEP = item["AF_CTADEP"].ToString();
                    AF_VALACT = ConvDoble(item["AF_VALACT"].ToString());
                    AF_VALRES = ConvDoble(item["AF_VALRES"].ToString());
                    AF_VALORI = ConvDoble(item["AF_VALORI"].ToString());
                    AF_DEPACC = ConvDoble(item["AF_DEPACC"].ToString());
                    AF_DEPSEM = ConvDoble(item["AF_DEPSEM"].ToString());
                    AF_ALICUOTA = ConvDoble(item["AF_ALICUOTA"].ToString());
                    AF_VALLIB = ConvDoble(item["AF_VALLIB"].ToString());
                    AF_CANT = int.Parse(item["AF_CANT"].ToString());
                    //AF_MODIFFECHA= DateTime.Parse(item["AF_MODIFFECHA"].ToString()); 	
                    //AF_MODIFHORA= TimeSpan.Parse(item["AF_MODIFHORA"].ToString()); 	
                    AF_MODIFUSUA = item["AF_MODIFUSUA"].ToString();
                    //AF_DESFECHA= DateTime.Parse(item["AF_DESFECHA"].ToString()); 		
                    AF_DES = item["AF_DES"].ToString();
                    //AF_MEJORA= int.Parse(item["AF_MEJORA"].ToString()); 		
                    //AF_PVP= double.Parse(item["AF_PVP"].ToString()); 			
                    AF_CTOUNIT = ConvDoble(item["AF_CTOUNIT"].ToString());
                    //AF_TPOGAR= int.Parse(item["AF_TPOGAR"].ToString()); 		
                    AF_NROGAR = item["AF_NROGAR"].ToString();
                    AF_PROVGAR = item["AF_PROVGAR"].ToString();
                    AF_TELPROVGAR = item["AF_TELPROVGAR"].ToString();
                    AF_DIRGAR = item["AF_DIRGAR"].ToString();
                    AF_MARCA = item["AF_MARCA"].ToString();
                    AF_MOD = item["AF_MOD"].ToString();
                    //AF_FEVENC= DateTime.Parse(item["AF_FEVENC"].ToString()); 		
                    AF_OBS = item["AF_OBS"].ToString();
                    AF_UBICACION = item["AF_UBICACION"].ToString();
                    AF_CODPERT = item["AF_CODPERT"].ToString();
                    AF_DEPREC = item["AF_DEPREC"].ToString() == "1" ? true : false;
                    AF_DEPANT = decimal.Parse(item["AF_DEPANT"].ToString());
                    AF_ULTDEP = decimal.Parse(item["AF_ULTDEP"].ToString());
                    AF_BIENNAC = item["AF_BIENNAC"].ToString();
                    AF_PARAM1 = item["AF_PARAM1"].ToString();
                    AF_DESCBIEN = item["AF_DESCBIEN"].ToString();
                    AF_CLASBIEN = item["AF_CLASBIEN"].ToString();
                    //AF_ETIQ_IMPRE= int.Parse(item["AF_ETIQ_IMPRE"].ToString()); 	no siempre viene vacio
                    AFCTAACTOLD = item["AFCTAACTOLD"].ToString();
                    AFCTADEPOLD = item["AFCTADEPOLD"].ToString();
                    AFCTAGTOOLD = item["AFCTAGTOOLD"].ToString();
                    AF_ETIQ_CONCILIA = item["AF_ETIQ_CONCILIA"].ToString();
                    AF_COLOR = item["AF_COLOR"].ToString();
                    AF_MATERIAL = item["AF_MATERIAL"].ToString();
                    AFECHACAL = AF_DEPINI.AddMonths(AF_DEPMESES);

                    break;
                case 2:
                    if (this.af_maestro == null)
                    {
                        this.af_maestro = new Productos();
                    }
                    this.af_maestro.AFCOD = item["AFCOD"].ToString();
                    this.af_maestro.AFNOM = item["AFNOM"].ToString();
                    this.af_maestro.AFUNIDAD = item["AFUNIDAD"].ToString();
                    break;
                case 3:
                    if (this.ActivoFijoGrupos == null)
                    {
                        this.ActivoFijoGrupos = new ActivoFijoGrupos();
                    }
                    this.ActivoFijoGrupos.AFGRUDES = item["AFGRUDES"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAACT = item["AFGRUCTAACT"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTADEP = item["AFGRUCTADEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAGTODEP = item["AFGRUCTAGTODEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUMESES = int.Parse(item["AFGRUMESES"].ToString());
                    break;


                default:
                    break;
            }

            return this;
        }




        //CONVERTIR DOUBLE QUE VENGAN VACIOS
        double ConvDoble(string item)
        {
            double aux = 0;
            double.TryParse(item, out aux);
            return aux;
        }


        //CONVERTIR INT QUE VENGAN VACIOS
        static int ConvInt(string item)
        {
            int aux = 0;
            int.TryParse(item, out aux);
            return aux;
        }



        //CONVERTIR DATETIME QUE VENGAN VACIOS
        DateTime ConvDate(string item)
        {
            DateTime aux = AF_DEPINI;
            DateTime.TryParse(item, out aux);

            return aux;
        }

        //CONVERTIR STRING QUE VENGAN VACIOS
        static string ConvString(string item)
        {
            if (string.IsNullOrEmpty(item))
                return "-";
            else
                return string.Format(item);
        }






        //    !!!!...Pendiente para GERMAN...!!!!

        protected override ConectDB LoadObject(Rows reader)
        {
            int nroRS = 1;
            foreach (Row item in reader)
            {
                LoadObject(item, nroRS++);
            }

            return this;
        }

        protected override ConectDB CargarObjeto(DataTable dt)
        {
            int nroRS = 1;
            foreach (DataRow item in dt.Rows)
            {
                CargarObjeto(item, nroRS++);
            }

            return this;
        }

        //////////////////////////////////////   para crear multiple se creo LoadObject2 en clsmain
        protected override ConectDB LoadObject2(Rows reader)
        {
            int nroRS = 1;
            foreach (Row item in reader)
            {

                LoadObject2(item, nroRS++);

            }

            return this;
        }

        protected ActivoFijo LoadObject2(Row item, int nroRS)
        {
            switch (nroRS)
            {
                case 1:

                    if (this.af_maestro == null)
                    {
                        this.af_maestro = new Productos();
                    }
                    this.af_maestro.AFCOD = item["AFCOD"].ToString();
                    this.af_maestro.AFNOM = item["AFNOM"].ToString();
                    this.af_maestro.AFUNIDAD = item["AFUNIDAD"].ToString();
                    var a = this.af_maestro.AFEXI = int.Parse(item["AFEXI"].ToString());
                    var b = this.af_maestro.AFUNIASIG = item["AFUNIASIG"].ToString();
                    this.af_maestro.calculo = (a - int.Parse(b)).ToString();
                    //var c = a - int.Parse(b);

                    break;
                case 2:
                    if (this.ActivoFijoGrupos == null)
                    {
                        this.ActivoFijoGrupos = new ActivoFijoGrupos();
                    }
                    this.ActivoFijoGrupos.AFGRUDES = item["AFGRUDES"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAACT = item["AFGRUCTAACT"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTADEP = item["AFGRUCTADEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAGTODEP = item["AFGRUCTAGTODEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUMESES = int.Parse(item["AFGRUMESES"].ToString());
                    break;


                default:
                    break;
            }

            return this;
        }


        /////////////////////////////////////////////////////////////////

        public int Insertmultiple() //<-----------Revisar el codigo
        {
            clsAS400 cn = new clsAS400();
            cn.execProc("SP_INS_AF_ASIGNA", getParameters2(), true);
            return cn.afectados;

        }



        #region AUTOCOMPLETE BUSCA POR ETIQUETA
        public static List<ActivoFijo> GetCampos2()
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT AF_ETIQ FROM '" + ParametrosGlobales.bd + "'  ActivoFijo FETCH FIRST 1001 ROWS ONLY";
            List<ActivoFijo> activos = new List<ActivoFijo>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ActivoFijo activo = new ActivoFijo();
                    activo.AF_ETIQ = item["AF_ETIQ"].ToString();
                    activos.Add(activo);
                }
            }

            return activos;
        }

        public List<string> GetCampos(string codigo)
        {
            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT AF_ETIQ FROM " + ParametrosGlobales.bd + "  AF_ASIGNA WHERE ( UPPER ( AF_ETIQ ) LIKE CONCAT ( UPPER ( '" + codigo + "' ) , '%' ) )";
            List<string> activos = new List<string>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string activo = "";
                    activo = item["AF_ETIQ"].ToString();
                    activos.Add(activo);
                }
            }

            return activos;
        }
        #endregion




        #region Autocomplete DE CODIGO
        public List<string> GetCamposCod(string codigo)
        {

            System.Collections.ArrayList parametros = new System.Collections.ArrayList(1);
            parametros.Add(codigo);

            DataSet ds = new DataSet();
            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            string consulta = "SELECT CONCAT ( CONCAT ( AFCOD , ' - ' ) , AFNOM ) AS AFNOMB FROM " + ParametrosGlobales.bd + " AF_MAESTRO WHERE ( UPPER ( AFNOM ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) ) OR ( UPPER ( AFCOD ) LIKE CONCAT ( CONCAT ( '%' , UPPER ( '" + codigo + "' ) ) , '%' ) AND AFEXI - AFUNIASIG > 0 )";
            List<string> proveedores = new List<string>();
            OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
            DA.Fill(ds);
            DataTable dt = ds.Tables[0];

            if (dt.Rows != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string proveedor = "";
                    proveedor = item["AFNOMB"].ToString();
                    proveedores.Add(proveedor);
                }
            }

            return proveedores;
        }
        #endregion


        // PARA CARGAR OBJETO   NO ESTA CREADO TODAVIA 
        public ConectDB BuscarCOD(string value)           //<-----------Revisar el codigo
        {
            clsAS400 cn = new clsAS400();
            ArrayList arreglo = new ArrayList(1);
            arreglo.Add(value);
            Rows reader = cn.execProc("SP_Q_AF_CODACT", arreglo, false);
            ConectDB objeto = null;
            if (reader != null)
            {
                objeto = LoadObject2(reader);
            }
            return objeto;
        }


        #region PROCEDURE

        public List<ActivoFijo> ProductosPorAFCODACT(string AF_CODACT)
        {
            List<ActivoFijo> lista = new List<ActivoFijo>();
            ConectDB cn = new ConectDB();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(AF_CODACT);
            Rows reader = cn.execute2(tabla, whereParam(parametros), false);
            if (reader != null)
            {
                if (reader.Count > 0)
                {
                    foreach (Row item in reader)
                    {
                        ActivoFijo ActivoFijo = new ActivoFijo();
                        //{
                        //ActivoFijo.AF_CODACT = item["AF_CODACT"].ToString();
                        //ActivoFijo.AF_SERIAL = item["AF_SERIAL"].ToString();
                        //ActivoFijo.AF_CCTO = item["AF_CCTO"].ToString();
                        //ActivoFijo.AF_FECH = DateTime.Parse(item["AF_FECH"].ToString());
                        //ActivoFijo.AF_FECHREAL = DateTime.Parse(item["AF_FECHREAL"].ToString());

                        lista.Add(ActivoFijo.LoadObject(item, 1));
                    }
                }
            }
            return lista;
        }
        #endregion

        #region SIN-PROCEDURE
        ////para el grid af_maestro
        //public static List<ActivoFijo> ProductosPorAFCODACT(string AF_CODACT)
        //{


        //    ArrayList parametros = new ArrayList(1);
        //    parametros.Add(AF_CODACT);


        //    DataSet ds = new DataSet();
        //    OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
        //    string consulta = "SELECT  A . * ,  M . CC_NOMB  FROM BICADM01W . AF_ASIGNA A INNER JOIN  BICADM01W . CC_MAESTRO M  ON A . AF_CCTO = M . CC_COD  WHERE AF_CODACT = '" + AF_CODACT + "' ";
        //    List<ActivoFijo> lista = new List<ActivoFijo>();
        //    OleDbDataAdapter DA = new OleDbDataAdapter(consulta, cn);
        //    DA.Fill(ds);
        //    DataTable dt = ds.Tables[0];



        //    if (dt.Rows != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow item in dt.Rows)
        //            {
        //                ActivoFijo ActivoFijo = new ActivoFijo();
        //                //{
        //                //    CXPCONTCOD  = item["CXPCONTCOD"].ToString(),
        //                //    CXPCONTNOM  = item["CXPCONTNOM"].ToString(),
        //                //    CXPCOD      = item["CXPCOD"].ToString(),
        //                //    CXPCONTDIR  = item["CXPCONTDIR"].ToString(),
        //                //    CXPCONTEXT  =  item["CXPCONTEXT"].ToString(),
        //                //    CXPCONTFAX   = item["CXPCONTFAX"].ToString(),
        //                //    CXPCONTTEL   = item["CXPCONTTEL"].ToString(),
        //                //    CXPCONTSEL   = int.Parse(item["CXPCONTSEL"].ToString()),
        //                //    CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString(),
        //                //};
        //                ActivoFijo.CC_NOMB = item["CC_NOMB"].ToString();
        //                lista.Add(ActivoFijo.CargarObjeto(item, 1));
        //            }
        //        }
        //    }
        //    return lista;
        //}
        #endregion

        public ActivoFijo AfAsigna(string term)
        {
            string consulta = "SELECT A.AF_CODACT ,A.AF_CODEMPL ,A.AF_SERIAL ,A.AF_FECH ,A.AF_FECHREAL ,A.AF_CCTO ,A.AF_USUARIO ,A.AF_ETIQ ,A.AF_DEPINI ,A.AF_DEPULT ,A.AF_DEPMESES ,A.AF_DEPMESESYA ,A.AF_ORGCOSTO ,A.AF_ORGFECHA ,A.AF_ORGMESESDEP ,A.AF_ADQFECHA ,A.AF_ADQOC ,A.AF_ADQFACT  ,A.AF_ADQCOMP  ,A.AF_ADQCONT  ,A.AF_ADQVIGD  ,A.AF_ADQVIGH  ,A.AF_ADQPROV  ,A.AF_CTAACT  ,A.AF_CTAGTODEP  ,A.AF_CTADEP  ,A.AF_VALACT  ,A.AF_VALRES  ,A.AF_VALORI  ,A.AF_DEPACC  ,A.AF_DEPSEM  ,A.AF_ALICUOTA  ,A.AF_VALLIB  ,A.AF_CANT  ,A.AF_MODIFFECHA  ,A.AF_MODIFHORA  ,A.AF_MODIFUSUA  ,A.AF_DESFECHA ,A.AF_DES  ,A.AF_MEJORA  ,A.AF_PVP  ,A.AF_CTOUNIT  ,A.AF_TPOGAR  ,A.AF_NROGAR  ,A.AF_PROVGAR  ,A.AF_TELPROVGAR  ,A.AF_DIRGAR  ,A.AF_MARCA  ,A.AF_MOD  ,A.AF_FEVENC  ,A.AF_OBS ,A.AF_UBICACION  ,A.AF_CODPERT  ,A.AF_DEPREC  ,A.AF_DEPANT  ,A.AF_ULTDEP  ,A.AF_BIENNAC  ,A.AF_PARAM1  ,A.AF_DESCBIEN  ,A.AF_CLASBIEN  ,A.AF_ETIQ_IMPRE  ,A.AFCTAACTOLD  ,A.AFCTADEPOLD  ,A.AFCTAGTOOLD  ,A.AF_ETIQ_CONCILIA  ,A.AF_COLOR  ,A.AF_MATERIAL  ,B.AFNOM ,B.AFDES ,B.AFREF ,B.AFGRU ,B.AFTIP ,B.AFDEP ,B.AFMAR ,B.AFMOD ,B.AFSER ,B.AFUNIASIG ,B.AFEXI ,B.AFCOD ,B.AFUNIDAD ,B.AFFIS ,B.AFMONTO ,B.AFCODOLD ,B.AFNOMOLD ,B.AFCATESPECCOD ,C.AFGRUCOD ,C.AFGRUDES ,C.AFGRUCTAACT ,C.AFGRUCTADEP ,C.AFGRUCTAGTODEP      ,C.AFGRUMESES ,C.AFCTAACTTD ,C.AFCTADEPTD ,C.AFCTAGTOTD FROM " + ParametrosGlobales.bd + "  AF_ASIGNA  AS A INNER JOIN  " + ParametrosGlobales.bd + " AF_MAESTRO AS B ON A . AF_CODACT = B .  AFCOD INNER JOIN " + ParametrosGlobales.bd + " ActivoFijoGrupos AS C ON B.AFGRU= C.AFGRUCOD WHERE A . AF_ETIQ = '" + term + "'  ";

            List<ActivoFijo> listafinal = new List<ActivoFijo>();


            OleDbConnection cn = new OleDbConnection(ConectDB.CnStr);
            cn.Open();
            OleDbCommand comando = new OleDbCommand(consulta, cn);
            OleDbDataReader reader = comando.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {

                    AF_CODACT = reader["AF_CODACT"].ToString();
                    AF_CODEMPL = reader["AF_CODEMPL"].ToString();
                    AF_SERIAL = reader["AF_SERIAL"].ToString();
                    AF_FECH = DateTime.Parse(reader["AF_FECH"].ToString());
                    AF_FECHREAL = DateTime.Parse(reader["AF_FECHREAL"].ToString());
                    AF_CCTO = reader["AF_CCTO"].ToString();
                    AF_USUARIO = reader["AF_USUARIO"].ToString();
                    AF_ETIQ = reader["AF_ETIQ"].ToString();
                    AF_DEPINI = DateTime.Parse(reader["AF_DEPINI"].ToString());
                    AF_DEPULT = DateTime.Parse(reader["AF_DEPULT"].ToString());
                    AF_DEPMESES = int.Parse(reader["AF_DEPMESES"].ToString());
                    AF_DEPMESESYA = int.Parse(reader["AF_DEPMESESYA"].ToString());
                    AF_ADQFECHA = DateTime.Parse(reader["AF_ADQFECHA"].ToString());
                    AF_ADQOC = reader["AF_ADQOC"].ToString();
                    AF_ADQFACT = reader["AF_ADQFACT"].ToString();
                    AF_ADQCOMP = reader["AF_ADQCOMP"].ToString();
                    AF_ADQCONT = reader["AF_ADQCONT"].ToString();
                    AF_ADQPROV = reader["AF_ADQPROV"].ToString();
                    AF_CTAACT = reader["AF_CTAACT"].ToString();
                    AF_CTAGTODEP = reader["AF_CTAGTODEP"].ToString();
                    AF_CTADEP = reader["AF_CTADEP"].ToString();
                    AF_VALACT = ConvDoble(reader["AF_VALACT"].ToString());
                    AF_VALRES = ConvDoble(reader["AF_VALRES"].ToString());
                    AF_VALORI = ConvDoble(reader["AF_VALORI"].ToString());
                    AF_DEPACC = ConvDoble(reader["AF_DEPACC"].ToString());
                    AF_DEPSEM = ConvDoble(reader["AF_DEPSEM"].ToString());
                    AF_ALICUOTA = ConvDoble(reader["AF_ALICUOTA"].ToString());
                    AF_VALLIB = ConvDoble(reader["AF_VALLIB"].ToString());
                    AF_CANT = int.Parse(reader["AF_CANT"].ToString());
                    AF_MODIFUSUA = reader["AF_MODIFUSUA"].ToString();
                    AF_DES = reader["AF_DES"].ToString();
                    AF_CTOUNIT = ConvDoble(reader["AF_CTOUNIT"].ToString());
                    AF_NROGAR = reader["AF_NROGAR"].ToString();
                    AF_PROVGAR = reader["AF_PROVGAR"].ToString();
                    AF_TELPROVGAR = reader["AF_TELPROVGAR"].ToString();
                    AF_DIRGAR = reader["AF_DIRGAR"].ToString();
                    AF_MARCA = reader["AF_MARCA"].ToString();
                    AF_MOD = reader["AF_MOD"].ToString();
                    AF_OBS = reader["AF_OBS"].ToString();
                    AF_UBICACION = reader["AF_UBICACION"].ToString();
                    AF_CODPERT = reader["AF_CODPERT"].ToString();
                    AF_DEPREC = reader["AF_DEPREC"].ToString() == "1" ? true : false;
                    AF_DEPANT = decimal.Parse(reader["AF_DEPANT"].ToString());
                    AF_ULTDEP = decimal.Parse(reader["AF_ULTDEP"].ToString());
                    AF_BIENNAC = reader["AF_BIENNAC"].ToString();
                    AF_PARAM1 = reader["AF_PARAM1"].ToString();
                    AF_DESCBIEN = reader["AF_DESCBIEN"].ToString();
                    AF_CLASBIEN = reader["AF_CLASBIEN"].ToString();
                    AFCTAACTOLD = reader["AFCTAACTOLD"].ToString();
                    AFCTADEPOLD = reader["AFCTADEPOLD"].ToString();
                    AFCTAGTOOLD = reader["AFCTAGTOOLD"].ToString();
                    AF_ETIQ_CONCILIA = reader["AF_ETIQ_CONCILIA"].ToString();
                    AF_COLOR = reader["AF_COLOR"].ToString();
                    AF_MATERIAL = reader["AF_MATERIAL"].ToString();
                    AFECHACAL = AF_DEPINI.AddMonths(AF_DEPMESES);

                    if (this.af_maestro == null)
                    {
                        this.af_maestro = new Productos();
                    }
                    this.af_maestro.AFCOD = reader["AFCOD"].ToString();
                    this.af_maestro.AFNOM = reader["AFNOM"].ToString();
                    this.af_maestro.AFUNIDAD = reader["AFUNIDAD"].ToString();

                    if (this.ActivoFijoGrupos == null)
                    {
                        this.ActivoFijoGrupos = new ActivoFijoGrupos();
                    }
                    this.ActivoFijoGrupos.AFGRUDES = reader["AFGRUDES"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAACT = reader["AFGRUCTAACT"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTADEP = reader["AFGRUCTADEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUCTAGTODEP = reader["AFGRUCTAGTODEP"].ToString();
                    this.ActivoFijoGrupos.AFGRUMESES = int.Parse(reader["AFGRUMESES"].ToString());

                }
            }


            return this;
        }

        protected override object whereParam(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {
                QUERY = ""+Campo_Clave+"='" + elemento + "'";
            }

            return QUERY;
        }

        protected override object whereParam2(ArrayList value)
        {
            string QUERY = "";
            foreach (object elemento in value)
            {
                QUERY = "AF_ETIQ='" + elemento + "'";
            }

            return QUERY;
        }


        //static void  MultipleEtiqueta(string AF_CODACT, string cantidad)
        //{
        //    //List<ActivoFijo> lista = new List<ActivoFijo>();
        //    clsAS400 cn = new clsAS400();
        //    ArrayList parametros = new ArrayList(2);
        //    parametros.Add(AF_CODACT);
        //    parametros.Add(cantidad);
        //    Rows reader = cn.execProc("SP_AF_MULTIPLE", parametros, false);
        //    if (reader != null)
        //    {
        //        if (reader.Count > 0)
        //        {
        //            foreach (Row item in reader)
        //            {
        //                ActivoFijo ActivoFijo = new ActivoFijo();
        //                //{
        //                //    CXPCONTCOD  = item["CXPCONTCOD"].ToString(),
        //                //    CXPCONTNOM  = item["CXPCONTNOM"].ToString(),
        //                //    CXPCOD      = item["CXPCOD"].ToString(),
        //                //    CXPCONTDIR  = item["CXPCONTDIR"].ToString(),
        //                //    CXPCONTEXT  =  item["CXPCONTEXT"].ToString(),
        //                //    CXPCONTFAX   = item["CXPCONTFAX"].ToString(),
        //                //    CXPCONTTEL   = item["CXPCONTTEL"].ToString(),
        //                //    CXPCONTSEL   = int.Parse(item["CXPCONTSEL"].ToString()),
        //                //    CXPCONTEMAIL = item["CXPCONTEMAIL"].ToString(),
        //                //};
        //                //ActivoFijo.CC_NOMB = item["CC_NOMB"].ToString();
        //                //ActivoFijo.LoadObject(item, 1);
        //            }
        //        }
        //    }
        //    return this;
        //}





        #region EXCEL

        //        private void button2_Click(object sender, EventArgs e)
        //        {
        //            string ruta = string.Empty;
        //            OpenFileDialog fDialog = new OpenFileDialog();
        //            fDialog.Title = "Open Image Files";
        //            //fDialog.Filter = "JPEG Files|*.jpeg|GIF Files|*.gif";
        //            //fDialog.Filter = "Excel Files|*.xls";
        //            fDialog.Filter = "Excel Files|*.*";
        //            fDialog.InitialDirectory = @"C:\";
        //            if (fDialog.ShowDialog() == DialogResult.OK)
        //            {
        //                ruta = fDialog.FileName.ToString();


        //                string CadenaConexion = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
        //@"Data Source=C:\archivo.xls;" + @"Extended Properties=" + '"' + "Excel 8.0;HDR=YES" + '"';

        //                string sqlExcel = "SELECT serial FROM [hoja1$]";

        //                DataTable DS = new DataTable();
        //                OleDbConnection oledbConn = new OleDbConnection(CadenaConexion);


        //                try
        //                {
        //                    oledbConn.Open();
        //                    OleDbCommand oledbCmd = new OleDbCommand(sqlExcel, oledbConn);
        //                    OleDbDataAdapter da = new OleDbDataAdapter(oledbCmd);
        //                    da.Fill(DS);
        //                    dataGridView1.DataSource = DS;
        //                }
        //                catch (Exception ex)
        //                {
        //                    MessageBox.Show(ex.Message);
        //                }
        //            }
        //            else
        //            {
        //                // cancelo 
        //            }
        //        }
        #endregion

    }






}



///////////////CAMPOS DE LA TABLA//////////////////
//AF_CODACT,			
//AF_CODEMPL, 			
//AF_SERIAL, 			
//AF_FECH, 			
//AF_FECHREAL, 		
//AF_CCTO, 			
//AF_USUARIO, 			
//AF_ETIQ, 			
//AF_DEPINI, 			
//AF_DEPULT, 			
//AF_DEPMESES, 		
//AF_DEPMESESYA, 		
//AF_ORGCOSTO, 		
//AF_ORGFECHA, 		
//AF_ORGMESESDEP, 		
//AF_ADQFECHA, 		
//AF_ADQOC, 			
//AF_ADQFACT, 			
//AF_ADQCOMP, 			
//AF_ADQCONT, 			
//AF_ADQVIGD, 			
//AF_ADQVIGH, 			
//AF_ADQPROV, 			
//AF_CTAACT, 			
//AF_CTAGTODEP, 		
//AF_CTADEP, 			
//AF_VALACT, 			
//AF_VALRES, 			
//AF_VALORI,			
//AF_DEPACC, 			
//AF_DEPSEM, 			
//AF_ALICUOTA, 		
//AF_VALLIB,			
//AF_CANT, 			
//AF_MODIFFECHA, 		
//AF_MODIFHORA, 		
//AF_MODIFUSUA, 		
//AF_DESFECHA, 		
//AF_DES, 				
//AF_MEJORA, 			
//AF_PVP, 				
//AF_CTOUNIT, 			
//AF_TPOGAR, 			
//AF_NROGAR, 			
//AF_PROVGAR, 			
//AF_TELPROVGAR, 		
//AF_DIRGAR, 			
//AF_MARCA, 			
//AF_MOD, 				
//AF_FEVENC, 			
//AF_OBS, 				
//AF_UBICACION, 		
//AF_CODPERT, 			
//AF_DEPREC, 			
//AF_DEPANT, 			
//AF_ULTDEP, 			
//AF_BIENNAC, 			
//AF_PARAM1, 			
//AF_DESCBIEN, 		
//AF_CLASBIEN, 		
//AF_ETIQ_IMPRE, 		
//AFCTAACTOLD, 		
//AFCTADEPOLD, 		
//AFCTAGTOOLD, 		
//AF_ETIQ_CONCILIA, 	
//AF_COLOR, 			
//AF_MATERIAL,





//////////////// SP UTILIZADOS ////////////////
//SP_INS_AF_ASIGNA
//SP_DEL_AF_ASIGNA
//SP_UPD_AF_ASIGNA
//SP_Q_AF_ASIGNA
//SP_Q_PROAFCODACT
//SP_Q_AF_ACTIVO
//SP_Q_AF_CODIGOS
//SP_Q_AF_CODACT


