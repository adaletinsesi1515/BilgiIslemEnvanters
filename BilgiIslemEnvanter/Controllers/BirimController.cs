using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class BirimController : Controller
    {
        // GET: Birim
        private BilgiIslemEntities2 db = new BilgiIslemEntities2();
        [Authorize] 
        public ActionResult Index()
        {
            var liste = db.Birimler.Where(m => m.DURUM == true).ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Birimler p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Birimler.Add(p);
            p.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Birimler.Find(id);
            bilgi.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var bilgi = db.Birimler.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Birimler p)
        {
            var bilgi = db.Birimler.Find(p.ID);
            bilgi.BIRIMAD= p.BIRIMAD;
            bilgi.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}