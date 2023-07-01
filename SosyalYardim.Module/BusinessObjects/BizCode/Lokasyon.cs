using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;

namespace SosyalYardim.Module.BusinessObjects.Biz
{
    [DefaultClassOptions]
    [NavigationItem("Adres")]
    [DefaultProperty("Ad")]
    public partial class Lokasyon
    {
        public Lokasyon(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
