//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MTC_wpfApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Call
    {
        public int Id { get; set; }
        public System.DateTime date { get; set; }
        public int city_id { get; set; }
        public int client_id { get; set; }
        public int time { get; set; }
        public bool is_payment { get; set; }
    
        public virtual City City { get; set; }
        public virtual Client Client { get; set; }
    }
}
