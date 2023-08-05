using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DostPatiler.Models
{
    public enum Cinsiyet
    {
        Erkek, Dişi
    }
    public enum Tur
    {
        Kedi, Köpek, Kuş, Hamster, Tavşan
    }
    public enum Surec
    {
        Beklemede, Onaylandı
    }

    public class Hayvan
    {
        [Key]
        public int HayvanId { get; set; }
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Cinsiyeti")]
        public Cinsiyet HayvanCinsiyet { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Türü")]
        public Tur HayvanTur { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Cinsi")]
        public string HayvanCins { get; set; }
        [Required(ErrorMessage = "Bu alan zorunlu.")]
        [Display(Name = "Hayvan Yas")]
        [DataType(DataType.Date)]
        public DateTime HayvanYas { get; set; }
        [Display(Name = "Hayvanın Güncel Fotoğrafı")]
        public string? CurrentHayvanImage { get; set; }
        [Display(Name = "Hayvanın Fotoğrafı")]
        public string? HayvanImage { get; set; }
        public Surec Durum { get; set; } = Surec.Onaylandı;
        public bool Sahiplendirme { get; set; } = false;


    }
}
