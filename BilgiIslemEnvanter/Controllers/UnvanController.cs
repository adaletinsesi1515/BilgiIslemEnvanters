using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan
        private BilgiIslemEntities2 db = new BilgiIslemEntities2();
        [Authorize]
        public ActionResult Index()
        {
            var liste = db.Unvanlar.Where(m => m.DURUM == true).ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Unvanlar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Unvanlar.Add(p);
            p.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Unvanlar.Find(id);
            bilgi.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var bilgi = db.Unvanlar.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Unvanlar p)
        {
            var bilgi = db.Unvanlar.Find(p.ID);
            bilgi.UNVANAD= p.UNVANAD;
            bilgi.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}