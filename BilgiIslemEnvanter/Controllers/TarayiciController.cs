using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class TarayiciController : Controller
    {
        // GET: Tarayici
        private BilgiIslemEntities2 db = new BilgiIslemEntities2();
        [Authorize]
        public ActionResult Index()
        {
            var liste = db.Tarayicilar.Where(m => m.DURUM == true).ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Tarayicilar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Tarayicilar.Add(p);
            p.DURUM = true;
            p.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Tarayicilar.Find(id);
            bilgi.DURUM = false;
            bilgi.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var bilgi = db.Tarayicilar.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Tarayicilar p)
        {
            var bilgi = db.Tarayicilar.Find(p.ID);
            bilgi.MARKA = p.MARKA;
            bilgi.MODEL = p.MODEL;
            bilgi.SERINO = p.SERINO;
            bilgi.DURUM = true;
            bilgi.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}