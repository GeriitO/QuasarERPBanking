using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    public class CONFIG
    {
        public string culture { get; set; }


        public static void updidioma(string idioma)
        {
            Models.clsAS400 cn = new Models.clsAS400();
            ArrayList parametros = new ArrayList(1);
            parametros.Add(idioma);
            cn.execProc("SP_UPD_IDIOMA", parametros, true);
        }

        public static void setResource(string culture)
        {
            Resources.StringResources.Culture =
            Resources.CentrodeCostoInterno.StringResources.Culture =
            Resources.INV_NEMAESTRO.StringResources.Culture =
            //Resources.USUARIO.StringResources.Culture = new System.Globalization.CultureInfo(culture);
            Resources.CC_MAESTRO.StringResources.Culture =
            Resources.ActivoFijo.StringResources.Culture = new System.Globalization.CultureInfo(culture);
            //Resources.ActivoFijo.StringResources.Culture =

            Resources.Productos.StringResources.Culture =
            //Resources.SERIALES.StringResources.Culture =
            Resources.ActivoFijoGrupos.StringResources.Culture =
            Resources.ActivoFijoParametros.StringResources.Culture =

            //Resources.MovimientoActivoFijo.StringResources.Culture =
            //Resources.CentrodeCostoInterno.StringResources.Culture =
            //
            //Resources.CC_CONTACTOS.StringResources.Culture =
            //Resources.CC_ACTIVIDADES.StringResources.Culture =
            //Resources.Inventario.StringResources.Culture =
            //Resources.INV_PARAMETROS.StringResources.Culture =
            //Resources.INV_NEMAESTRO.StringResources.Culture =
            //Resources.INV_TRANSACCIONES.StringResources.Culture =
            Resources.CC_PARAMETROS.StringResources.Culture = new System.Globalization.CultureInfo(culture);
            //Resources.ETIQUETAS.StringResources.Culture =
            //Resources.INV_MAESTRO.StringResources.Culture =
            //Resources.INV_PARAMETROS.StringResources.Culture =
            //Resources.INV_NEMAESTRO.StringResources.Culture =
            //Resources.INV_TRANSACCIONES.StringResources.Culture =
            //Resources.MANTENIMIENTO.StringResources.Culture =
            //Resources.PARAMETRIZACION.StringResources.Culture =
            //Resources.CXP_ACTIVIDADES.StringResources.Culture =
            //Resources.CXP_CONTACTOS.StringResources.Culture =
            //Resources.CXP_PARAMETROS.StringResources.Culture =
            //Resources.CXP_GRUPOS.StringResources.Culture =
            //Resources.SEGURIDAD.StringResources.Culture =
            //Resources.CXP_INGREDOC.StringResources.Culture =
            //Resources.CXP_PROCOMP.StringResources.Culture =
            //Resources.CXP_SELECTPAGAR.StringResources.Culture =
            //Resources.CXP_ORDENPAGO.StringResources.Culture =
            //Resources.CXP_ORDPAGO.StringResources.Culture =
            // Resources.COT_MAESTRO.StringResources.Culture =
            //Resources.CXP_MAESTRO.StringResources.Culture = 
            //Resources.CC_PARAMETROS.StringResources.Culture = new System.Globalization.CultureInfo(culture);
            Models.ParametrosGlobales.culture = culture;
        }
    }
}

//////////////// SP UTILIZADOS ////////////////
//SP_UPD_IDIOMA




