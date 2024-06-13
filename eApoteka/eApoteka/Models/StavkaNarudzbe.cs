using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int Id { get; set; }

        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }

        public int Kolicina { get; set; }
    }
}
