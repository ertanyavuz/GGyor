
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
    
    public partial class Operation
    {
    
        public int ID { get; set; }
    
        public int TransformID { get; set; }
    
        public string FieldName { get; set; }
    
        public Nullable<int> OperationType { get; set; }
    
        public string Value { get; set; }
    
        public Nullable<int> Order { get; set; }
    
        public string Name { get; set; }
    
    
    
        public virtual Transform Transform { get; set; }
    
    }

}
