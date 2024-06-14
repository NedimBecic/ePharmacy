﻿using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models
{
    public class Dostavljac
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ime { get; set; }

        [Required]
        [StringLength(50)]
        public string Prezime { get; set; }
        [Required]
        [StringLength(15)]
        public string BrojTelefona { get; set; }
    }
}