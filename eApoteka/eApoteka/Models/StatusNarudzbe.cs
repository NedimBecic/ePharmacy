using System.ComponentModel.DataAnnotations;

namespace eApoteka.Models
{
    public class StatusNarudzbe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TrenutniStatus { get; set; }

        public List<string> Historija { get; set; } = new List<string>();
    }
}
