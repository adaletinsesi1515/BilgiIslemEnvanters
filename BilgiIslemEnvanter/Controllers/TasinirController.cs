using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BilgiIslemEnvanter.Models.Entity;

namespace BilgiIslemEnvanter.Controllers
{
    public class TasinirController : Controller
    {
        // GET: Tasinir
        private BilgiIslemEntities db = new BilgiIslemEntities();
        public ActionResult Liste()
        {
            var liste = db.Personeller.Where(m => m.DURUM == true).ToList();
            return View(liste);
            
        }


        public ActionResult FisDuzenle()
        {
            return View();
        }


    }
}