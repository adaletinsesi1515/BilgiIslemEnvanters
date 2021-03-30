using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilgiIslemEnvanter.MyClasses
{
    public class TonerZimmet
    {
        public int YaziciID { get; set; }
        public string YaziciMarka { get; set; }
        public string YaziciModel { get; set; }
        public string YaziciSeriNo { get; set; }
        

        public bool Durum { get; set; }
        public bool Zimmet { get; set; }
        public bool BirimID { get; set; }
        public bool PersonelID { get; set; }

    }
}