using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eApoteka.Models
{
    public class Proizvod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Naziv { get; set; }

        [Required]
        [StringLength(50)]
        public string Vrsta { get; set; }

        [Required]
        public string Opis { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        public double Cijena { get; set; }

        [ForeignKey("Apotekar")]
        public int ApotekarId { get; set; }
        public Apotekar Apotekar { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public bool Dostupan { get; set; }
    }

}
