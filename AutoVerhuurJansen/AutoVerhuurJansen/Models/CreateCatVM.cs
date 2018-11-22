using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutoVerhuurJansen.Models
{
    public class CreateCateVM
    {
        [Required]
        [Display(Name = "Categorie Naam")]
        public string categorieID { get; set; }
        [Required]
        [Display(Name = "Naam")]
        public string categorieNaam { get; set; }
        [Required]
        [Display(Name = "Personen")]
        public string aantalPersonen { get; set; }
        [Required]
        [Display(Name = "Koffers")]
        public string aantalKoffers { get; set; }

        [Required]
        [Display(Name = "Prijs")]
        public string categorieprijs { get; set; }


    }
}
