using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SistemaGCS.Models;
namespace SistemaGCS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Index(Usuario usuarios, string ReturnUrl)
        {
            HomeController obj = new HomeController();
            Usuario usu = IsValid(usuarios);
            if (usu != null)
            {
                Session["Id"] = usu.Id_usuario;
                Session["Nombre Completo"] = usu.Nombre + " " + usu.Apellido;

                FormsAuthentication.SetAuthCookie(usuarios.Correo, false);
                if (ReturnUrl != null)
                {
                    return Redirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home", new { });
            }
            TempData["mensaje"] = "El correo electrónico o contraseña que ingresaste no está correcto a una cuenta. Encuentra tu cuenta e inicia sesión.";

            return View(usuarios);
        }
        private Usuario IsValid(Usuario usuarios)
        {

            return usuarios.Autenticar();
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

    }
}