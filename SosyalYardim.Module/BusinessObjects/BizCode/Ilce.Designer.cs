﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace SosyalYardim.Module.BusinessObjects.Biz
{

    public partial class Ilce : XPObject
    {
        string fAd;
        public string Ad
        {
            get { return fAd; }
            set { SetPropertyValue<string>(nameof(Ad), ref fAd, value); }
        }
        Il fIl;
        [Association(@"IlceReferencesIl")]
        public Il Il
        {
            get { return fIl; }
            set { SetPropertyValue<Il>(nameof(Il), ref fIl, value); }
        }
        [Association(@"MahalleReferencesIlce")]
        public XPCollection<Mahalle> Mahalles { get { return GetCollection<Mahalle>(nameof(Mahalles)); } }
    }

}
