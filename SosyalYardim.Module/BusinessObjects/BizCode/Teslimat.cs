using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using DevExpress.Persistent.Base;

namespace SosyalYardim.Module.BusinessObjects.Biz
{
    [DefaultClassOptions]
    [NavigationItem("Yardım")]
    public partial class Teslimat
    {
        public Teslimat(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
