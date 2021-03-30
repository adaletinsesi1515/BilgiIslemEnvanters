using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;
using BilgiIslemEnvanter.MyClasses;


namespace BilgiIslemEnvanter.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        private BilgiIslemEntities db = new BilgiIslemEntities();

        public ActionResult Index()
        {

            var liste = db.Personeller.Where(m => m.DURUM == true).ToList();
            return View(liste);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler1 = (from i in db.Unvanlar.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.UNVANAD,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr1 = degerler1;


            List<SelectListItem> degerler2 = (from i in db.Birimler.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.BIRIMAD,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr2 = degerler2;
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Personeller p)
        {


            var unvan = db.Unvanlar.Where(m => m.ID == p.Unvanlar.ID).FirstOrDefault();
            p.Unvanlar = unvan;
            var birim = db.Birimler.Where(m => m.ID == p.Birimler.ID).FirstOrDefault();
            p.Birimler = birim;
            if (p.SICIL == null)
            {
                return View("Ekle");
            }
            db.Personeller.Add(p);
            p.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var bilgi = db.Personeller.Find(id);
            bilgi.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Getir(int id)
        {
            List<SelectListItem> degerler1 = (from i in db.Unvanlar.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.UNVANAD,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr1 = degerler1;


            List<SelectListItem> degerler2 = (from i in db.Birimler.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.BIRIMAD,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr2 = degerler2;
            var bilgi = db.Personeller.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Personeller p)
        {
            var bilgi = db.Personeller.Find(p.ID);
            bilgi.SICIL = p.SICIL;
            bilgi.ADSOYAD = p.ADSOYAD;
            bilgi.DAHILITELEFONU = p.DAHILITELEFONU;
            bilgi.CEPTELEFONU = p.CEPTELEFONU;
            bilgi.DURUM = true;
            var unvan = db.Unvanlar.Where(m => m.ID == p.Unvanlar.ID).FirstOrDefault();
            bilgi.UNVANID = unvan.ID;
            var birim = db.Birimler.Where(m => m.ID == p.Birimler.ID).FirstOrDefault();
            bilgi.BIRIMID = birim.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ZimmetYap(int? ID)
        {
            ViewBag.id = ID;


            var BilgisayarModel = db.Bilgisayarlar.Where(x => x.DURUM == true && x.ZIMMET == false && x.SERINO != "BAKANLIK").OrderByDescending(x => x.SERINO == null).Select(a => new
            {
                Value = a.ID,
                Text = a.SERINO + " -- " + a.MARKA + " -- " + a.MODEL

            }).ToList();

            var YaziciModel = db.Yazicilar.Where(x => x.DURUM == true && x.ZIMMET == false).OrderByDescending(x => x.SERINO == null).Select(
                a => new
                {
                    Value = a.ID,

                    Text = a.SERINO
                }).ToList();


            var TarayiciModel = db.Tarayicilar.Where(x => x.DURUM == true && x.ZIMMET == false).OrderByDescending(x => x.SERINO == null).Select(a => new
            {
                Value = a.ID,
                Text = a.SERINO + " -- " + a.MARKA + " -- " + a.MODEL
            }).ToList();

            ViewBag.tarayiciSN = new SelectList(TarayiciModel, "Value", "Text");
            ViewBag.yaziciSN = new SelectList(YaziciModel, "Value", "Text");
            ViewBag.BilgisayarSN = new SelectList(BilgisayarModel, "Value", "Text");





            return View();
        }

        [HttpPost]
        public ActionResult Zimmetle(HepiTopu hep)
        {

            var personel = hep.personelID;
            var yazici = hep.yaziciSN;
            var tarayici = hep.tarayiciSN;
            var bilgisayar = hep.bilgisayarSN;
            var domainadi = hep.domainAdi;
            var domainip = hep.domainIP;
            var yaziciip = hep.yaziciIP;
            hep.Durum = true;
            hep.Zimmet = true;


            Tasinirlar tasinir = new Tasinirlar
            {
                BILGISAYARID = bilgisayar,
                DOMAINADI = domainadi,
                DOMAINIP = domainip,
                PERSONELID = personel,
                TARAYICIID = tarayici,
                YAZICIID = yazici,
                YAZICIPORT = yaziciip,
                DURUM = true,
                ZIMMET = true,

            };

            db.Tasinirlar.Add(tasinir);


            //Yazıcıyı Id ile buldu ve zimmet alanını true'ye çevirdi.
            var YaziciModel = db.Yazicilar.FirstOrDefault(x => x.ID == yazici);
            if (YaziciModel.SERINO == null)
            {
                YaziciModel.ZIMMET = false;
            }
            else
            {
                YaziciModel.ZIMMET = true;
            }
            db.Entry(YaziciModel).State = System.Data.Entity.EntityState.Modified;

            //Bilgisayar Id ile buldu ve zimmet alanını true'ye çevirdi.
            var BilgiModel = db.Bilgisayarlar.FirstOrDefault(x => x.ID == bilgisayar);
            if (BilgiModel.SERINO == null)
            {
                BilgiModel.ZIMMET = false;
            }
            else
            {
                BilgiModel.ZIMMET = true;
            }
            db.Entry(BilgiModel).State = System.Data.Entity.EntityState.Modified;

            //Tarayıcı Id ile buldu ve zimmet alanını true'ye çevirdi.
            var TaraModel = db.Tarayicilar.FirstOrDefault(x => x.ID == tarayici);
            if (TaraModel.SERINO == null)
            {
               TaraModel.ZIMMET = false;
            }
            else
            {
                TaraModel.ZIMMET = true;
            }
            db.Entry(TaraModel).State = System.Data.Entity.EntityState.Modified;


            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}