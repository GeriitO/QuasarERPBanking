using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuasarERPBanking.Models
{
    /// <summary>
    /// Para generar el menu dependiendo de los roles que tenga el usuario
    /// </summary>
    public class Menu
    {
        public List<SubMenu> submenus { get; set; }
        /// <summary>
        /// Se utiliza cuando se llama para generar el menu de un usuario
        /// </summary>
        /// <param name="db">DbContext del controlador donde se esta llamando (para no crear otro)</param>
        /// <param name="contactoID">ID del contacto</param>
        public Menu(string usuario)
        {
            List<string> modulos = ROLOPC.getModUser(usuario, "0");

            List<OPC> opcIDs = OPC.getOpcUser(Util.contacto.ToUpper());

            submenus = new List<SubMenu>(modulos.Count);

            List<MODIDIOM> lstModulos = MODIDIOM.getNOMMODIDIOM(ParametrosGlobales.culture);

            foreach (string modulo in modulos)
            {
                try
                {

                    string nomModulo = lstModulos.Find(x => x.OPCMODULO.Equals(modulo)).OPCNBMOD;
                    //string iconos = lstModulos.FindAll(x => x.ICONS.Equals(modulo));
                    //var iconos = lstModulos.Select(x => x.ICONS.Equals(modulo)).ToString();
                    string result = lstModulos.Find(dispatch => dispatch.OPCMODULO.Equals(modulo)).ICONS.Trim();

                    submenus.Add(new SubMenu(result, modulo, nomModulo, opcIDs.FindAll(x => x.OPCMODULO.Equals(modulo))));
                }
                catch (Exception ex)
                {

                }

            }
        }
        /// <summary>
        /// Se utiliza para obtener el menu completo (Solo para utilizar desde el modulo de seguridad)
        /// </summary>
        public Menu()
        {

            List<string> modulos = OPC.getModulos();
            //List<string> mod = OPC.getModulos_();
        
            submenus = new List<SubMenu>();
            foreach (string modulo in modulos)
            {
                submenus.Add(new SubMenu(modulo));
            }

            //foreach (string modulo in mod)
            //{
            //    submenus.Add(new SubMenu(modulo));
            //}

        }

        /// <summary>
        /// Se utiliza para determinar si el usuario tiene acceso a un metodo en un controlador
        /// </summary>
        /// <param name="Controller">Controlador donde el usuario va a ingresar</param>
        /// <param name="Action">Action del Controlador</param>
        /// <returns>true en caso de que tenga acceso</returns>
        public bool TieneAcceso(string Controller, string Action)
        {
            foreach (SubMenu sm in submenus)
            {
                return true;

            }
            return true;
        }

        /// <summary>
        /// Solo para utilizar desde el módulo de seguridad
        /// </summary>
        /// <param name="rolID">ID del Rol</param>
        /// <param name="opcionID">ID de la Opcion</param>
        /// <returns>true si el rol tiene acceso a ese rol</returns>
        public bool TieneAcceso(int rolID, int opcionID)
        {

            return true;
        }
    }

    public class SubMenu
    {
        public String NombreModulo { get; set; }
        public String CodModulo { get; set; }

        public List<OPC> opciones { get; set; }

        public List<SubMenu> submodulos { get; set; }

        public String icons {get; set; }
       

        public SubMenu(string nombreModulo)
        {
            NombreModulo = nombreModulo;
            OPC opciones = new OPC();
            this.opciones = OPC.getOpcMod(nombreModulo);

        }

        public SubMenu(string iconos,string codmodulo, string nombreModulo, List<OPC> opciones)
        {

            this.NombreModulo = nombreModulo;
            this.opciones = new List<OPC>(opciones.Count);
            this.icons = iconos;
            foreach (var opc in opciones)
            {
                OPC opcion = new OPC
                {
                    OPCCOD = opc.OPCCOD,
                    OPCMODULO = opc.OPCMODULO,
                    Controller = opc.Controller,
                    Action = opc.Action,
                    OPCNOM = opc.OPCNOM

                };
                this.opciones.Add(opcion);
            }

            this.submodulos = new List<SubMenu>();
          /*  List<Modulos> submods = Modulos.getSubMods(codmodulo);       */ // VIEJO
            List<Modulos> submods = Modulos.getSubMods2(codmodulo);
            /* List<MODIDIOM> lstModulos = MODIDIOM.getNOMMODIDIOM(ParametrosGlobales.culture);  */  //VIEJO como estaba antes
            List<MODIDIOM> lstModulos = MODIDIOM.getNOMMODIDIOM2(ParametrosGlobales.culture);
            /* List<OPC> opcIDs = OPC.getOpcUser(Util.contacto.ToUpper()); */      //VIEJO
            //List<OPC> opcIDs = OPC.getOpcUser2(Util.contacto.ToUpper());


            //if (submods.Equals()) { }
            //foreach (Modulos submod in submods)
            //{



            //    //string nomModulo = lstModulos.Find(x => x.OPCMODULO.Equals(submod.OPCMODULO)).OPCNBMOD; //viejo
            //    //this.submodulos.Add(new SubMenu(submod.OPCMODULO, nomModulo, opcIDs.FindAll(x => x.OPCMODULO.Equals(submod.OPCMODULO))));  //viejo

            //    //string nomModulo = lstModulos.Find(x => x.OPCHIJO.Equals(submod.OPCHIJO)).OPCNOMB;  //SIRVE viejo

            //    //string nomModulo = lstModulos.Find(x => x.OPCMODULO.Equals(submod.OPCMODULO)).OPCNOMB;     /// solo trae el nombre del opcion
            //    //this.submodulos.Add(new SubMenu(submod.OPCPADRE, nomModulo, opcIDs.FindAll(x => x.OPCCOD.ToString().Equals(submod.OPCPADRE))));  sirve viejo
            //    string nomModulo = opcIDs.Find(x => x.OPCMODULO.Equals(submod.OPCMODULO) && x.OPCCOD.ToString().Equals(submod.OPCPADRE)).OPCNBMOD;  //nuevo pruebas  funciona            
            //    this.submodulos.Add(new SubMenu(submod.OPCPADRE, nomModulo, opcIDs.FindAll(x => x.OPCCOD.ToString().Equals(submod.OPCPADRE) && x.OPCMODULO.Equals(submod.OPCMODULO))));    //nuevo prueba funciona

            //}


        }

    }

    //class Menu2
    //{
    //    List<> menues;
    //    List<SubMenu>

    //}
}