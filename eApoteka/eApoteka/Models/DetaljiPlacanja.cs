using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models
{
    public class DetaljPlacanja
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TipPlacanja { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusPlacanja { get; set; }

        [Required]
        public DateTime DatumPlacanja { get; set; }

    }
}

