//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GamesBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class RF_Distributors
    {
        public RF_Distributors()
        {
            this.Games = new HashSet<Games>();
        }
    
        public decimal ID_RF_Distributor { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Is_open { get; set; }
        public Nullable<System.DateTime> Date_open { get; set; }
        public Nullable<System.DateTime> Date_close { get; set; }
        public byte[] Icon { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Games> Games { get; set; }
    }
}
