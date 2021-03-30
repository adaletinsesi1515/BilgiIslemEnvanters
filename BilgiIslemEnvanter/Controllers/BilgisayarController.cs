using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class BilgisayarController : Controller
    {
        // GET: Bilgisayar
        private BilgiIslemEntities db = new BilgiIslemEntities();
        public ActionResult Index()
        {
            var bilgisayarlar = db.Bilgisayarlar.Where(m => m.DURUM == true && m.SERINO!="BAKANLIK").ToList();
            return View(bilgisayarlar);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Bilgisayarlar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Bilgisayarlar.Add(p);
            p.DURUM = true;
            p.ZIMMET = false; 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Bilgisayarlar.Find(id);
            bilgi.DURUM = false;
            bilgi.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var bilgi = db.Bilgisayarlar.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Bilgisayarlar p)
        {
            var bilgi = db.Bilgisayarlar.Find(p.ID);
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