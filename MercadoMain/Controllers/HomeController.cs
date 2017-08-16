using System;
using System.Web.Mvc;
using System.Web.Security;
using MercadoAplicacao.LoginApp;
using MercadoDominio.Entidades;

namespace ProjetoMercado.Controllers
{
    public class HomeController : Controller
    {
        private LoginAplicacao appLogin; 

        public HomeController()
        {
            appLogin = LoginAplicacaoConstrutor.LoginAplicacaoADO();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorized()
        {
            ModelState.AddModelError("LOGIN", "Você não está logado!");
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                var logins = appLogin.ListarTodos();

                foreach (var log in logins)
                {
                    if (log.LoginU == login.LoginU && log.Senha == login.Senha)
                    {
                        Session["Login"] = login.LoginU;
                        ViewBag.Resultado = Session["Login"].ToString();
                    }
                }
                return RedirectToAction("LogadoComSucesso");
            }
            return View(login);
        }

        public ActionResult LogadoComSucesso()
        {
            if (Session["Login"] == null)
            {
                ViewBag.Resultado = "login ou senha incorretos";
            }
            else
            {
                ViewBag.Resultado = Session["Login"] + " logado com sucesso";
            }

            return View();
        }

        public ActionResult Logoff()
        {
            if (Session["Login"] != null)
            {
                Session.Clear();
            }

            return RedirectToAction("Index","Home");
        }
    }
}