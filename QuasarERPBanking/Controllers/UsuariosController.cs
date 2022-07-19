using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuasarERPBanking.Models;

namespace QuasarERPBanking.Controllers
{
    public class UsuariosController : ParentController
    {
        // GET: Usuarios
        public ActionResult Index()
        {

            return View(Usuarios.GetUsers());
        }

        // GET: Usuarios/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Usuarios/Create
        //public new  ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Usuarios/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Usuarios/Edit/5
        public ActionResult Edit(string id)
        {
            Usuarios usuario = new Usuarios();
            object consulta = " usuario='" + id + "' ";
            return View((Usuarios)usuario.cargar("Q_USUARIOS", consulta));
        }

        //// POST: Usuarios/Edit/5
        //[HttpPost]
        ////public ActionResult Edit( FormCollection collection)
        //public ActionResult Edit(Models.Usuarios user)
        //{
        //    try
        //    {

        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Usuarios/Delete/5
        public ActionResult Delete(string id)
        {
            Usuarios usuario = new Usuarios();
            string query = "SELECT USUARIO , CLAVE , FECHACREACION , NIVEL , STATUS , NOMBRE , APELLIDO , NIVACC , FECHAMODIF , DEPOS, CODEMP , CTEINT , STATUSCLAVE , CLAVECADUCIDAD , EMAIL , MAYUSCULA , MINUSCULA , ESPACIO , NUMERO , ESPECIAL , NOREPETIRCAR , RUTASKIN , CLAVENOREPETIR FROM " + ParametrosGlobales.bd + " Q_USUARIOS WHERE USUARIO = USUARIO_ ";
            return View((Usuarios)usuario.cargar("USUARIO", query));
        }

        //// POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                string message = string.Empty;
                Usuarios usuarios = new Usuarios();
                usuarios.USUARIO = id;
                message = usuarios.Eliminar();
                ViewBag.message = message;
                //return RedirectToAction("Index");
                return View("Index", Usuarios.GetUsers());
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(Usuarios usuarios)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.message = usuarios.Create();
                    //return RedirectToAction("Index");
                    return View("Index", Usuarios.GetUsers());
                }
                else
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
                    return View();
                }


            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Edit(Usuarios usuarios)
        {

            string message = string.Empty;

            try
            {
                if (ModelState.IsValid)
                {
                    message = usuarios.Edit();
                    ViewBag.message = message;
                }
                else
                {
                    message = "Existe(n) campo(s) vacios en el registro.  Por favor, consulte la lista de errores al final de la pagina...";
                    ViewBag.message = message;
                    return View(usuarios);
                }
            }
            catch
            {
                return View();
            }

            //return RedirectToAction("Index");
            return View("Index", Usuarios.GetUsers());


        }

        //public ActionResult Delete(string usuario)
        //{
        //    string message = string.Empty;
        //    Usuarios user = new Usuarios();
        //    user.USUARIO = usuario;
        //    message = user.Eliminar();
        //    ViewBag.message = message;
        //    return View("Index");

        //}






    }
}
