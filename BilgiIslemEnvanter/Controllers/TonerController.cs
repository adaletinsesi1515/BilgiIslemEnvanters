using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class TonerController : Controller
    {
        // GET: Toner
        BilgiIslemEntities db = new BilgiIslemEntities();

        public ActionResult Liste()
        {
            var listele = db.Yazicilar.Where(x => x.ZIMMET == true).ToList();
            return View(listele);
        }

        [HttpGet]
        public ActionResult TonerGiris()
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
        public ActionResult TonerGiris(TonerGiris p)
        {


            var markasi = db.YaziciMarkalari.Where(m => m.ID == p.YaziciMarkalari.ID).FirstOrDefault();
            p.YaziciMarkalari = markasi;

            var modeli = db.YaziciModelleri.Where(m => m.ID == p.YaziciModelleri.ID).FirstOrDefault();
            p.YaziciModelleri = modeli;


            if (p.TONERADET == null)
            {
                return View("TonerGiris");
            }

            db.TonerGiris.Add(p);
            p.DURUM = true;


            //Bilgisayar Id ile buldu ve zimmet alanını true'ye çevirdi.
            var TonerEkle = db.TonerStok.FirstOrDefault(x =>
                x.YAZICIMARKALARIID == p.YAZICIMARKA && x.YAZICIMODELLERIID == p.YAZICIMODEL);
            TonerEkle.KALANTONER += p.TONERADET;
            TonerEkle.KALANDRUM += p.DRUMADET;
            db.Entry(TonerEkle).State = System.Data.Entity.EntityState.Modified;


            db.SaveChanges();
            return RedirectToAction("Liste");
        }

        [HttpGet]
        public ActionResult TonerCikis(int? ID)
        {

            ViewBag.id = ID;


            //var YaziciModel = db.Yazicilar.Where(x => x.ZIMMET == true).Select(a => new
            //{
            //    Value = a.ID,
            //    Text = a.SERINO

            var YaziciModel = db.Yazicilar.Where(x => x.ZIMMET == true).Select(a => new
            {
                Value = a.ID,
                Text = a.SERINO

            }).ToList();

            //ViewBag.dgr1 = YaziciModel;

            //List<SelectListItem> birimler = (from i in db.Birimler.Where(i => i.DURUM == true).ToList()
            //                                 select new SelectListItem
            //                                 {

            //                                     Text = i.BIRIMAD,
            //                                     Value = i.ID.ToString(),

            //                                 }).ToList();
            //ViewBag.dgr2 = birimler;


            //List<SelectListItem> personeller = (from i in db.Personeller.Where(i => i.DURUM == true).ToList()
            //                                    select new SelectListItem
            //                                    {

            //                                        Text = i.ADSOYAD,
            //                                        Value = i.ID.ToString(),

            //                                    }).ToList();
            //ViewBag.dgr3 = personeller;

            List<SelectListItem> kullanici = (from i in db.Kullanicilar.Where(i => i.DURUM == true).ToList()
                select new SelectListItem
                {

                    Text = i.ADISOYADI,
                    Value = i.ID.ToString(),

                }).ToList();
            ViewBag.dgr4 = kullanici;

            //List<SelectListItem> yazicimarka = (from i in db.YaziciMarkalari.Where(i => i.DURUM == true).ToList()
            //                                    select new SelectListItem
            //                                    {

            //                                        Text = i.YAZICIMARKA,
            //                                        Value = i.ID.ToString(),

            //                                    }).ToList();
            //ViewBag.dgr5 = yazicimarka;


            //List<SelectListItem> yazicimodel = (from i in db.YaziciModelleri.Where(i => i.DURUM == true).ToList()
            //                                    select new SelectListItem
            //                                    {

            //                                        Text = i.YAZICIMODEL,
            //                                        Value = i.ID.ToString(),

            //                                    }).ToList();
            //ViewBag.dgr6 = yazicimodel;


            return View();
        }



        [HttpPost]
        public ActionResult TonerCikis(TonerCikis toner)
        {


            if (ModelState.IsValid)
            {
                if (toner.TONERADET == null || toner.DRAMADET == null)
                {
                    return View("TonerCikis");
                }

                db.TonerCikis.Add(toner);

                db.SaveChanges();

            }

            return RedirectToAction("Liste");
        }


        //if(ModelState.IsValid)
        //{



        //    if (toner.TONERADET== null || toner.DRAMADET == null)
        //    {
        //       return View("TonerCikis");
        //    }
        //    toner.DURUM = true;
        //    db.TonerCikis.Add(toner);


        public ActionResult TonerStok()
        {
            var model = db.TonerStok.ToList();
            return View(model);
        }

       

    [HttpGet]
        public ActionResult TonerCikis1(int ID)
        {

            ViewBag.id = ID;

            //var YaziciModel = db.Yazicilar.Where(x => x.ZIMMET == true).Select(a => new
            //{
            //    Value = a.ID,
            //    Text = a.SERINO

            //}).ToList();

            //ViewBag.dgr1 = YaziciModel;

            List<SelectListItem> birimler = (from i in db.Birimler.Where(i => i.DURUM == true).ToList()
                                             select new SelectListItem
                                             {

                                                 Text = i.BIRIMAD,
                                                 Value = i.ID.ToString(),

                                             }).ToList();
            ViewBag.dgr2 = birimler;


            //List<SelectListItem> personeller = (from i in db.Personeller.Where(i => i.DURUM == true).ToList()
            //                                    select new SelectListItem
            //                                    {

            //                                        Text = i.ADSOYAD,
            //                                        Value = i.ID.ToString(),

            //                                    }).ToList();
            //ViewBag.dgr3 = personeller;

            List<SelectListItem> kullanici = (from i in db.Kullanicilar.Where(i => i.DURUM == true).ToList()
                                              select new SelectListItem
                                              {

                                                  Text = i.ADISOYADI,
                                                  Value = i.ID.ToString(),

                                              }).ToList();
            ViewBag.dgr4 = kullanici;

            List<SelectListItem> yazicimarka = (from i in db.YaziciMarkalari.Where(i => i.DURUM == true).ToList()
                                                select new SelectListItem
                                                {

                                                    Text = i.YAZICIMARKA,
                                                    Value = i.ID.ToString(),

                                                }).ToList();
            ViewBag.dgr5 = yazicimarka;


            List<SelectListItem> yazicimodel = (from i in db.YaziciModelleri.Where(i => i.DURUM == true).ToList()
                                                select new SelectListItem
                                                {

                                                    Text = i.YAZICIMODEL,
                                                    Value = i.ID.ToString(),

                                                }).ToList();
            ViewBag.dgr6 = yazicimodel;


            return View();
        }



        [HttpPost]
        public ActionResult TonerCikis1(TonerCikis toner1)
        {


            db.TonerCikis.Add(toner1);
            db.SaveChanges();
            return RedirectToAction("Liste");

            //var birim = db.Birimler.Where(m => m.ID == toner1.BIRIMID).FirstOrDefault();
            //toner1.Birimler = birim;

            //if (ModelState.IsValid)
            //{
            //    if (toner.TONERADET == null || toner.DRAMADET == null)
            //    {
            //        return View("TonerCikis");
            //    }
            //    toner.DURUM = true;


            //}




                return RedirectToAction("Liste");
            }

          
        }

    }
