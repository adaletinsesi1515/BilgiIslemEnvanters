using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgiIslemEnvanter.MyClasses
{
    public class HepiTopu
    {
        public int personelID { get; set; }
        public int bilgisayarSN { get; set; }
        public int yaziciSN { get; set; }
        public int tarayiciSN { get; set; }
        public string domainAdi { get; set; }
        public string domainIP { get; set; }
        public string yaziciIP { get; set; }
        public bool Durum { get; set; }
        public bool Zimmet { get; set; }
        public int BirimID { get; set; }
        


    }
}