//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Motormechs.Data.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Parent { get; set; }
        public Nullable<int> SortBy { get; set; }
        public Nullable<int> OrderBy { get; set; }
    }
}
