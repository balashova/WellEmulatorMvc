//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WellEmulatorMvc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WellTest
    {
        public WellTest()
        {
            this.ProductionTests = new HashSet<ProductionTest>();
        }
    
        public int idWellTest { get; set; }
        public Nullable<int> idWell { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> testDate { get; set; }
        public string testType { get; set; }
        public Nullable<double> chokeOrificeSize { get; set; }
    
        public virtual ICollection<ProductionTest> ProductionTests { get; set; }
        public virtual Well Well { get; set; }
    }
}
