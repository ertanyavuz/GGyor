
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace StorMan.Data
{
    
    using System;
    using System.Collections.Generic;
    
    public partial class LocalCategory
    {
    
        public LocalCategory()
        {
    
            this.LocalCategory1 = new HashSet<LocalCategory>();
    
            this.Categories = new HashSet<Category>();
    
        }
    
    
        public int ID { get; set; }
    
        public Nullable<int> ParentID { get; set; }
    
        public string Code { get; set; }
    
        public string Name { get; set; }
    
        public System.DateTime CrDate { get; set; }
    
        public Nullable<System.DateTime> UpdDate { get; set; }
    
        public Nullable<int> Level { get; set; }
    
    
    
        public virtual ICollection<LocalCategory> LocalCategory1 { get; set; }
    
        public virtual LocalCategory LocalCategory2 { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
    
    }

}
