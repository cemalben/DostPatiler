using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DostPatiler.Models
{
    public enum Cinsiyet
    {
        Erkek, Dişi
    }

    public class Hayvan
    {
        [Key]
        public int HayvanId { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Cinsiyeti")]
        public Cinsiyet HayvanCinsiyet { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Türü")]
        public string HayvanTur { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Cinsi")]
        public string HayvanCins { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Yas")]
        public DateTime HayvanYas { get; set; }
        [Display(Name = "Hayvanın Güncel Fotoğrafı")]
        public string CurrentHayvanImage { get; set; }
        [Display(Name = "Hayvanın Fotoğrafı")]
        public string HayvanImage { get; set; }

    }
}
