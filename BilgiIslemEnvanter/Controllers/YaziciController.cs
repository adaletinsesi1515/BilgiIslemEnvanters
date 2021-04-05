using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class YaziciController : Controller
    {
        // GET: Yazici

        private BilgiIslemEntities2 db = new BilgiIslemEntities2();
        [Authorize]
        public ActionResult Index()
        {
            var listeleme = db.Yazicilar.Where(i=>i.DURUM==true).ToList();
            //var liste = db.Yazicilar.Where(m => m.DURUM == true).ToList();
            return View(listeleme);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler1 = (from i in db.YaziciMarkalari.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.YAZICIMARKA,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr1 = degerler1;


            List<SelectListItem> degerler2 = (from i in db.YaziciModelleri.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.YAZICIMODEL,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr2 = degerler2;

            return View();

        }

        [HttpPost]
        public ActionResult Ekle(Yazicilar p)
        {
            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }
            db.Yazicilar.Add(p);
            p.DURUM = true;
            p.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Yazicilar.Find(id);
            bilgi.DURUM = false;
            bilgi.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            var bilgi = db.Yazicilar.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Yazicilar p)
        {
            var bilgi = db.Yazicilar.Find(p.ID);


            var yazicimarkasi= db.YaziciMarkalari.Where(m => m.ID == p.YaziciMarkalari.ID).FirstOrDefault();
            bilgi.MARKAID= p.YaziciMarkalari.ID;

            var yazicimodels = db.YaziciModelleri.Where(m => m.ID == p.YaziciModelleri.ID).FirstOrDefault();
            bilgi.MODELID = p.YaziciModelleri.ID;

            bilgi.MARKAID = p.MARKAID;
            bilgi.MODELID = p.MODELID;

            bilgi.SERINO = p.SERINO;
            bilgi.DURUM = true;
            bilgi.ZIMMET = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}