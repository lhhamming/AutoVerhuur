//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AutoVerhuurJansen.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Verhuren
    {
        public int VerhuurId { get; set; }
        public int klantId { get; set; }
        public string kenteken { get; set; }
        public Nullable<int> medewerkerId { get; set; }
        public System.DateTime beginDatum { get; set; }
        public System.DateTime eindDatum { get; set; }
        public bool afgehandeld { get; set; }
    
        public virtual Medewerkers Medewerkers { get; set; }
        public virtual Voertuigen Voertuigen { get; set; }
        public virtual Klanten Klanten { get; set; }
    }
}
