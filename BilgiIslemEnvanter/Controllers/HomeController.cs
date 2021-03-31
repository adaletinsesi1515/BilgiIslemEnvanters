using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class HomeController : Controller
    {
        private BilgiIslemEntities db = new BilgiIslemEntities();
        public ActionResult Index()
        {
            var liste = db.TonerCikis.ToList();
            var depopcsayisi = db.Bilgisayarlar.Where(i => i.ZIMMET == false).Count()-1;
            ViewBag.dgr1 = depopcsayisi;
            var yazicisayisi = db.Yazicilar.Where(i => i.ZIMMET == false).Count()-1;
            ViewBag.dgr2 = yazicisayisi;
            var tarayicisayisi = db.Tarayicilar.Where(i => i.ZIMMET == false).Count()-1;
            ViewBag.dgr3 = tarayicisayisi;
            var perssayisi = db.Personeller.Where(i => i.DURUM== true).Count();
            ViewBag.dgr4 = perssayisi;



            return View(liste);


            
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}