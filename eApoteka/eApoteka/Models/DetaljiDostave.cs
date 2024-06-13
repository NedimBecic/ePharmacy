using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models
{
    public class DetaljDostave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string AdresaDostave { get; set; }

        [Required]
        [StringLength(50)]
        public string NacinDostave { get; set; }

        [ForeignKey("Dostavljac")]
        public int DostavljacId { get; set; }
        public Dostavljac Dostavljac { get; set; }

    }
}
