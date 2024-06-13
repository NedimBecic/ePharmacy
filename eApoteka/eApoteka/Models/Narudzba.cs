using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eApoteka.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }

        public ICollection<StavkaNarudzbe> Stavke { get; set; } = new List<StavkaNarudzbe>();

        [Required]
        public int DetaljPlacanjaId { get; set; }
        [ForeignKey("DetaljPlacanjaId")]
        public DetaljPlacanja DetaljPlacanja { get; set; }

        [Required]
        public int DetaljDostaveId { get; set; }
        [ForeignKey("DetaljDostaveId")]
        public DetaljDostave DetaljDostave { get; set; }

        [ForeignKey("Korisnik")]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }

        [ForeignKey("Apotekar")]
        public int ApotekarId { get; set; }
        public Apotekar Apotekar { get; set; }

        [ForeignKey("StatusNarudzbeId")]
        public int StatusNarudzbeId { get; set; }
        public StatusNarudzbe StatusNarudzbe { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}
