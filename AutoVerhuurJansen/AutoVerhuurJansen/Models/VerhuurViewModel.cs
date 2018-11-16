using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoVerhuurJansen.Models
{

    public class VerhuurAutoViewModel
    {
        [Required]
        [Display(Name = "Begin Datum huur")]
        public System.DateTime beginDatum { get; set; }

        [Required]
        [Display(Name = "Eind datum huur")]
        public System.DateTime eindDatum { get; set; }

        [Required]
        [Display(Name = "Kenteken")]
        public string kenteken { get; set; }
    }
}
