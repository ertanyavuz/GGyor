
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
    
    public partial class ConvertedDataSet
    {
    
        public ConvertedDataSet()
        {
    
            this.ConvertedDataSetHistories = new HashSet<ConvertedDataSetHistory>();
    
            this.Transforms = new HashSet<Transform>();
    
        }
    
    
        public int ID { get; set; }
    
        public string Name { get; set; }
    
        public string SourceXmlPath { get; set; }
    
    
    
        public virtual ICollection<ConvertedDataSetHistory> ConvertedDataSetHistories { get; set; }
    
        public virtual ICollection<Transform> Transforms { get; set; }
    
    }

}
