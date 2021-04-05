using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;
using Microsoft.Ajax.Utilities;

namespace BilgiIslemEnvanter.Controllers
{
    public class HomeController : Controller
    {
        private BilgiIslemEntities2 db = new BilgiIslemEntities2();
        [Authorize]
        public ActionResult Index()
        {
            var liste = db.TonerCikis.ToList();
            var kalantonerms510 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 6).Sum(stok => stok.KALANTONER);
            ViewBag.dgr1 = kalantonerms510;

            var kalandrumms510= db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 6).Sum(stok => stok.KALANDRUM);
            ViewBag.dgr2 = kalandrumms510;

            var kalantonerms810 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 7).Sum(stok => stok.KALANTONER);
            ViewBag.dgr3 = kalantonerms810;

            var kalandrumms810 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 7).Sum(stok => stok.KALANDRUM);
            ViewBag.dgr4 = kalandrumms810;

            var kalantonert650= db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 10).Sum(stok => stok.KALANTONER);
            ViewBag.dgr5 = kalantonert650;

            var kalantonerms823 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 8).Sum(stok => stok.KALANTONER);
            ViewBag.dgr6 = kalantonerms823;
            //var depopcsayisi = db.Bilgisayarlar.Where(i => i.ZIMMET == false).Count()-1;
            //ViewBag.dgr1 = depopcsayisi;
            //var yazicisayisi = db.Yazicilar.Where(i => i.ZIMMET == false).Count()-1;
            //ViewBag.dgr2 = yazicisayisi;
            //var tarayicisayisi = db.Tarayicilar.Where(i => i.ZIMMET == false).Count()-1;
            //ViewBag.dgr3 = tarayicisayisi;
            var kalantonermx710 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 9).Sum(stok => stok.KALANTONER);
            ViewBag.dgr7 = kalantonermx710;

            var kalandrummx710 = db.TonerStok.Where(i => i.YAZICIMARKALARIID == 1 && i.YAZICIMODELLERIID == 9).Sum(stok => stok.KALANDRUM);
            ViewBag.dgr8 = kalandrummx710;



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