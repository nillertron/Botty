//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Botty.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class permissionType
    {
        public int id { get; set; }
        public string permissionName { get; set; }
        public bool @override { get; set; }
        public bool deletion { get; set; }
        public bool dbAdmin { get; set; }
    }
}
