//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BilgiIslemEnvanter.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TonerGiris
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TonerGiris()
        {
            this.TonerStok = new HashSet<TonerStok>();
        }
    
        public int ID { get; set; }
        public Nullable<int> YAZICIMARKA { get; set; }
        public Nullable<int> YAZICIMODEL { get; set; }
        public Nullable<int> TONERADET { get; set; }
        public Nullable<int> DRUMADET { get; set; }
        public Nullable<System.DateTime> STOKGIRISTARIHI { get; set; }
        public Nullable<bool> DURUM { get; set; }
    
        public virtual YaziciMarkalari YaziciMarkalari { get; set; }
        public virtual YaziciModelleri YaziciModelleri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TonerStok> TonerStok { get; set; }
    }
}
