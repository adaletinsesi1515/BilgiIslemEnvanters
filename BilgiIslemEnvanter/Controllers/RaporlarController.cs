using BilgiIslemEnvanter.Models.Entity;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BilgiIslemEnvanter.Controllers
{

    public class RaporlarController : Controller
    {
        private BilgiIslemEntities db = new BilgiIslemEntities();
        [Authorize]

        #region Zimmet Listesi

        public ActionResult ZimmetListesi()
        {
            var Model = db.Tasinirlar.Where(x => x.ZIMMET == true).ToList();
            return View(Model);
        }

        public void ZimmetExcelAktar()
        {
            string constr = ConfigurationManager.AppSettings["connectionString"];
            //var Client = new MongoClient(constr);
            //var db = Client.GetDatabase("Employee");

            //var collection = db.GetCollection<EmployeeDetails>("EmployeeDetails").Find(new BsonDocument()).ToList();
            var Model = db.Tasinirlar.ToList();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "AdSoyad";
            Sheet.Cells["B1"].Value = "Domain-Adi";
            Sheet.Cells["C1"].Value = "Domain-IP";
            Sheet.Cells["D1"].Value = "Yazici-Port";
            Sheet.Cells["E1"].Value = "Durum";
            Sheet.Cells["F1"].Value = "Zimmet";
            Sheet.Cells["G1"].Value = "Bilgisayar";
            Sheet.Cells["H1"].Value = "Tarayici";
            Sheet.Cells["I1"].Value = "Yazici";

            int row = 2;
            foreach (var item in Model)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.Personeller.ADSOYAD;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.DOMAINADI;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.DOMAINIP;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.YAZICIPORT;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.DURUM;

                Sheet.Cells[string.Format("F{0}", row)].Value = item.ZIMMET;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.Bilgisayarlar.SERINO + " - " + item.Bilgisayarlar.MARKA + " - " + item.Bilgisayarlar.MODEL;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.Tarayicilar.SERINO + " - " + item.Tarayicilar.MARKA + " - " + item.Tarayicilar.MODEL;
                //Sheet.Cells[string.Format("I{0}", row)].Value = item..SERINO + " - " + item.Yazicilar.MARKA + " - " + item.Yazicilar.MODEL;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
        }

        #endregion

        public ActionResult Getir(int id)
        {

            var bilgi = db.Tasinirlar.Find(id);
            return View("Getir", bilgi);
        }

        public ActionResult Guncelle(Tasinirlar ta)
        {
            var bilgi = db.Tasinirlar.Find(ta.ID);

            bilgi.DOMAINADI = ta.DOMAINADI;
            bilgi.DOMAINIP = ta.DOMAINIP;
            bilgi.YAZICIPORT = ta.YAZICIPORT;
            db.SaveChanges();

            return RedirectToAction("ZimmetListesi");
        }

        public ActionResult DepoyaCek(int ID, bool? Bilgisayar, bool? Yazici, bool? Tarayici)
        {
            var bilgi1 = db.Tasinirlar.Find(ID);


            if (Bilgisayar == true)
            {
                var BilgisayarM = db.Bilgisayarlar.FirstOrDefault(x => x.ID == bilgi1.BILGISAYARID);
                BilgisayarM.ZIMMET = false;
                bilgi1.BILGIZIMMET = false;
                bilgi1.BILGISAYARID= 177;
                if (bilgi1.YAZICIID == 119 && bilgi1.TARAYICIID == 39)
                {
                    bilgi1.YAZICIZIMMET = false;
                    bilgi1.TARAYICIZIMMET = false;
                    bilgi1.ZIMMET = false;
                }

                db.Entry(BilgisayarM).State = System.Data.Entity.EntityState.Modified;
            }


            if (Yazici == true)
            {
                var YaziciM = db.Yazicilar.FirstOrDefault(x => x.ID == bilgi1.YAZICIID);
                YaziciM.ZIMMET = false;
                bilgi1.YAZICIZIMMET = false;
                bilgi1.YAZICIID = 119;
                if (bilgi1.BILGISAYARID == 177 && bilgi1.TARAYICIID == 39 && bilgi1.YAZICIZIMMET == false)
                {
                    bilgi1.TARAYICIZIMMET = false;
                    bilgi1.BILGIZIMMET = false;
                    bilgi1.ZIMMET = false;
                }
                db.Entry(YaziciM).State = System.Data.Entity.EntityState.Modified;
            }
            if (Tarayici == true)
            {
                var TarayiciM = db.Tarayicilar.FirstOrDefault(x => x.ID == bilgi1.TARAYICIID);
                TarayiciM.ZIMMET = false;
                bilgi1.TARAYICIZIMMET = false;
                bilgi1.TARAYICIID = 39;
                if (bilgi1.BILGISAYARID == 177 && bilgi1.YAZICIID == 119 && bilgi1.TARAYICIZIMMET == false)
                {
                    bilgi1.YAZICIZIMMET = false;
                    bilgi1.BILGIZIMMET = false;
                    bilgi1.ZIMMET = false;
                }
                db.Entry(TarayiciM).State = System.Data.Entity.EntityState.Modified;
            }



            //şöyle bir şeyde olsa örneğin üçüde seçilmişse tasınırlar tablosundaki zimmet alanı false olsun, değilse true olmaya devam etsin.basit
            //o zaman alttaki işlevsiz kalacak. doğrumu yeni kod yazacağız. evete
            //var BilgiModel = db.Tasinirlar.FirstOrDefault(x => x.BILGISAYARID == x.Bilgisayarlar.ID);
            //var BilgiModel = db.Tasinirlar.FirstOrDefault(x => x.BILGISAYARID == ID);
            //BilgiModel.ZIMMET = false;    

            if (Tarayici == true && Yazici == true && Bilgisayar == true)
            {

                var BilgiModel = db.Tasinirlar.FirstOrDefault(x => x.ID == ID);
                BilgiModel.ZIMMET = false;
                bilgi1.BILGIZIMMET = false;
                bilgi1.YAZICIZIMMET = false;
                bilgi1.TARAYICIZIMMET = false;

                var BilgisayarM = db.Bilgisayarlar.FirstOrDefault(x => x.ID == bilgi1.BILGISAYARID);
                BilgisayarM.ZIMMET = false;

                var TarayiciM = db.Tarayicilar.FirstOrDefault(x => x.ID == bilgi1.TARAYICIID);
                TarayiciM.ZIMMET = false;

                var YaziciM = db.Yazicilar.FirstOrDefault(x => x.ID == bilgi1.YAZICIID);
                YaziciM.ZIMMET = false;


                db.Entry(BilgiModel).State = System.Data.Entity.EntityState.Modified;
                db.Entry(BilgisayarM).State = System.Data.Entity.EntityState.Modified;
                db.Entry(YaziciM).State = System.Data.Entity.EntityState.Modified;
                db.Entry(TarayiciM).State = System.Data.Entity.EntityState.Modified;

            }
            //else
            //{
            //    var BilgiModel = db.Tasinirlar.FirstOrDefault(x => x.ID == ID);
            //    BilgiModel.ZIMMET = true;
            //    db.Entry(BilgiModel).State = System.Data.Entity.EntityState.Modified;
            //}

            db.SaveChanges();
            return RedirectToAction("ZimmetListesi");
        }

        public ActionResult TarihFiltre1()
        {
            return View(db.TonerCikis.ToList());
        }
        [HttpPost]
        public ActionResult TarihFiltre1(DateTime start, DateTime end)
        {
            
            return View(db.GetFunctionTarihFiltresi(start, end));
        }

    }
}