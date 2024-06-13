using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eApoteka.Models
{

    public class Upit
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("Apotekar")]
        public int ApotekarId { get; set; }
        public Apotekar Apotekar { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        [StringLength(500)]
        public string Odgovor { get; set; }
    }

}
