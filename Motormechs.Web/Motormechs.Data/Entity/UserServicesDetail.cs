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
    
    public partial class UserServicesDetail
    {
        public int Id { get; set; }
        public Nullable<int> ParentServiceId { get; set; }
        public Nullable<int> UserSeviceId { get; set; }
        public Nullable<int> OrderServiceId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CompleteDate { get; set; }
    }
}
