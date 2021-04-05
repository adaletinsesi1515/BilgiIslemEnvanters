using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        private BilgiIslemEntities2 db = new BilgiIslemEntities2();

        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(Kullanicilar p)
        {
            var bilgiler =
                db.Kullanicilar.FirstOrDefault(x => x.SICIL == p.SICIL&& x.SIFRE == p.SIFRE);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.SICIL, false);
                Session["SICIL"] = bilgiler.SICIL.ToString();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();  
            }

            

        }
        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }

    }
}